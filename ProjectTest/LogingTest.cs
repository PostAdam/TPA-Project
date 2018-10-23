using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using MEFDefinitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;


namespace ProjectTest
{
    [TestClass]
    class LogingTest
    {
        [Import(typeof(ITrace))]
        private ITrace _logger;

        [TestMethod]
        public void TraceTest()
        {
            string path = $"{ nameof( LogingTest ) }.log";
            FileInfo logFile = new FileInfo( path );
            if (logFile.Exists)
            {
                logFile.Delete();
            }
            _logger.Log( "Test" );
            logFile.Refresh();
            Assert.IsTrue( logFile.Exists );
            Assert.IsTrue( logFile.Length > 10 );
        }
    }
}
