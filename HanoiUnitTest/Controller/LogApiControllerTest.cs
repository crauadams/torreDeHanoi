using HanoiAPI.Controllers;
using HanoiEntity;
using HanoiService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace HanoiUnitTest.Controller
{
    [TestClass]
    public class LogApiControllerTest : BaseUnitTest
    {
        [TestMethod]
        public void GetLogMovimentoComIdInexistente()
        {
            MovimentoLog movimentoLog = null;

            var controller = new LogApiController(logService);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.GetLogMovimento(-1);
            response.TryGetContentValue(out movimentoLog);

            Assert.IsNull(movimentoLog);
        }

        [TestMethod]
        public void GetLogMovimentoComIdExistente()
        {
            List<MovimentoLog> movimentoLog = null;

            var gameService = BuildGameService();
            var gameId = gameService.IniciarGame(3).Id;
            
            var controller = new LogApiController(logService);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.GetLogMovimento(gameId);
            response.TryGetContentValue(out movimentoLog);

            Assert.IsNotNull(movimentoLog);
        }

        [TestMethod]
        public void GetLogHistoricoSemRegistros()
        {
            List<HistoricoDTO> historicoLog = null;

            var controller = new LogApiController(logService);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.GetLogHistorico();
            response.TryGetContentValue(out historicoLog);

            Assert.IsNotNull(historicoLog);
            Assert.AreEqual(0, historicoLog.Count);
        }

        [TestMethod]
        public void GetLogHistoricoComRegistros()
        {
            List<HistoricoDTO> historicoLog = null;

            var gameService = BuildGameService();
            var gameId1 = gameService.IniciarGame(3).Id;
            var gameId2 = gameService.IniciarGame(4).Id;

            gameService.MovimentarDisco(gameId1);
            gameService.MovimentarDisco(gameId2);

            var controller = new LogApiController(logService);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.GetLogHistorico();
            response.TryGetContentValue(out historicoLog);

            Assert.IsNotNull(historicoLog);
            Assert.AreEqual(2, historicoLog.Count);
        }
    }
}
