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
            var result = analyzer.IsValidLogFileName("filewithbadextension.foo");

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidLogFileName_GoodExtensionLovercase_ReturnsTrue()
        {
            var analyzer = new LogAnalyzer();

            var result = analyzer.IsValidLogFileName("filewithgoodextention.slf");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            var analyzer = new LogAnalyzer();

            var result = analyzer.IsValidLogFileName("filewithgoodextention.SLF");

            Assert.IsTrue(result);
        }
    }
}
