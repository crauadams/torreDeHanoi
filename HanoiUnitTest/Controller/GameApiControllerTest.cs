using HanoiAPI.Controllers;
using HanoiEntity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HanoiUnitTest.Controller
{
    [TestClass]
    public class GameApiControllerTest : BaseUnitTest
    {
        [TestMethod]
        public void GetStartGameComDiscoMenorOuIgualAZero()
        {
            Game game = null;

            var controller = new GameApiController(gameService);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.StartGame(0);
            response.TryGetContentValue(out game);

            Assert.IsNull(game);
        }

        [TestMethod]
        public void GetStartGameComDiscoMaiorQueZero()
        {
            Game game = null;
            var quantidadeDeDiscos = 3;

            var controller = new GameApiController(gameService);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.StartGame(quantidadeDeDiscos);
            response.TryGetContentValue(out game);

            Assert.IsNotNull(game);
            Assert.AreEqual(quantidadeDeDiscos, game.Torres.ElementAt(0).Discos.Count);
        }
    }
}
