using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using HanoiService;
using HanoiRepository;

namespace HanoiAPI.App_Start
{
    public class AutofacWebApi
    {
        public static void Initialize(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterWebApiFilterProvider(config);

            Initialize(config, RegisterServices(builder));
        }
        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterGeneric(typeof(EntityRepository<>)).
                As(typeof(IEntityRepository<>)).InstancePerDependency();

            builder.RegisterType<GameService>().InstancePerRequest();
            builder.RegisterType<LogService>().InstancePerRequest();
            builder.RegisterType<MovimentacaoService>().InstancePerRequest();
            builder.RegisterType<SlackService>().InstancePerRequest();

            return builder.Build();
        }
    }
}