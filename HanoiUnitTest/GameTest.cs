using HanoiService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiUnitTest
{
    [TestClass]
    public class GameTest : BaseUnitTest
    {
        [TestMethod]
        public void QuantidadeDeMovimentosRealizadosDeveSerIgualAQuantidadeDeMovimentosNecessarios()
        {
            var gameService = BuildGameService();
            var game = gameService.IniciarGame(3);

            gameService.MovimentarDisco(game.Id);

            Assert.AreEqual(game.QuantidadeDeMovimentosNecessarios, game.QuantidadeDeMovimentosExecutados);
        }
    }
}
