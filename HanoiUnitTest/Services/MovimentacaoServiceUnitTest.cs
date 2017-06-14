using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HanoiUnitTest
{
    [TestClass()]
    public class MovimentacaoServiceUnitTest : BaseUnitTest
    {
        public MovimentacaoServiceUnitTest() : base() { }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalcularMovimentoComParametroNull()
        {
            movimentoService.CalcularMovimento(null, 0, null, null, null);
            Assert.Fail();
        }
    }
}
