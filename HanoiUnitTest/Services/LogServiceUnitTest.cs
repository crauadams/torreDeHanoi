using HanoiService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HanoiUnitTest
{
    [TestClass()]
    public class LogServiceUnitTest : BaseUnitTest
    {
        public LogServiceUnitTest() : base() { }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstanciarLogServicePassandoNullComoParametro()
        {
            var gameService = new LogService(null, null);
            Assert.Fail();
        }
    }
}
