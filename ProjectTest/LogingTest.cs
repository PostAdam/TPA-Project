using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project;

namespace ProjectTest
{
    [TestClass]
    class LogingTest
    {
        [TestMethod]
        public void TraceTest()
        {
            string path = $"{ nameof( LogingTest ) }.log";
            FileInfo logFile = new FileInfo( path );
            if (logFile.Exists)
            {
                logFile.Delete();
            }

            Logger logger = new Logger( new TextWriterTraceListener( path, "LogTest" ) );
            logger.Log( "Test" );
            logFile.Refresh();
            Assert.IsTrue( logFile.Exists );
            Assert.IsTrue( logFile.Length > 10 );
        }
    }
}
