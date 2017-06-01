using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogAn.Tests
{
    [TestClass]
    public class LogAnalyzerTests
    {
        [DataTestMethod]
        [DataRow("filewithbadextension.foo")]
        public void IsValidLogFileName_BadExtension_ReturnsFalse(string fileName)
        {
            // arrange
            var analyzer = new LogAnalyzer();

            // act
            var result = analyzer.IsValidLogFileName(fileName);

            // assert
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [DataRow("filewithgoodextention.slf")]
        [DataRow("filewithgoodextention.SLF")]
        public void IsValidLogFileName_ValidExtension_ReturnsTrue(string fileName)
        {
            var analyzer = new LogAnalyzer();

            var result = analyzer.IsValidLogFileName(fileName);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsValidLogFileName_EmptyFileName_ThrowsException()
        {
            var analyzer = new LogAnalyzer();

            analyzer.IsValidLogFileName(string.Empty);
        }
    }
}
