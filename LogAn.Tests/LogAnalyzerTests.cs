using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogAn.Tests
{
    [TestClass]
    public class LogAnalyzerTests
    {
        [TestMethod]
        public void IsValidFileName_BadExtension_ReturnsFalse()
        {
            // arrange
            var analyzer = new LogAnalyzer();

            // act
            bool result = analyzer.IsValidLogFileName("filewithbadextension.foo");

            // assert
            Assert.IsFalse(result);
        }
    }
}
