using HanoiService;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HanoiUnitTest.Tests
{
    [TestClass()]
    public class GameServiceUnitTest : BaseUnitTest
    {
        public GameServiceUnitTest() : base() { }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstanciarGamePassandoNullComoParametro()
        {
            var gameService = new GameService(null, null, null);
            Assert.Fail();
        }

        [TestMethod]
        public void InstanciarGamePassandoParametrosValidos()
        {
            var gameService = new GameService(gameRep, logService, movimentoService);
            Assert.IsNotNull(gameService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IniciarGameComQuantidadeDeDiscosMenorOuIgualAZero()
        {
            var gameService = BuildGameService();
            gameService.IniciarGame(0);
            Assert.Fail();
        }

        [TestMethod]
        public void IniciarGameComQuantidadeDeDiscosMaiorQueZero()
        {
            var gameService = BuildGameService();
            var game = gameService.IniciarGame(1);
            Assert.IsNotNull(game);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MovimentarDiscoComIdInexistente()
        {
            var gameService = BuildGameService();
            gameService.MovimentarDisco(0);
            Assert.Fail();
        }

        [TestMethod]
        public void MovimentarDiscoComIdExistente()
        {
            var gameService = BuildGameService();
            var gameId = gameService.IniciarGame(3).Id;
            gameService.MovimentarDisco(gameId);
        }
    }
}