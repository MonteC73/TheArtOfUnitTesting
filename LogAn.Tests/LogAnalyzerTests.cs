using LogAn.Tests.Helpers;
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

        [DataTestMethod]
        [DataRow("badfile.foo", false)]
        [DataRow("goodfile.slf", true)]
        public void IsValidFileName_WhenCalled_WasLastFileNameValid(string fileName, bool excpected)
        {
            var analyzer = new LogAnalyzer();

            analyzer.IsValidLogFileName(fileName);
            var actual = analyzer.WasLastFileNameValid;

            Assert.AreEqual(excpected, actual);
        }


        #region Microsoft recommendation for testing exception handling

        // Link to article: https://msdn.microsoft.com/library/jj159340.aspx

        [DataTestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void IsValidLogFileName_EmptyFileName_Throws(string fileName)
        {
            var analyzer = new LogAnalyzer();

            AssertThrows<ArgumentException>( delegate 
            {
                analyzer.IsValidLogFileName(fileName);
            });
        }

        internal static void AssertThrows<TException>(Action method)
            where TException : Exception
        {
            try
            {
                method.Invoke();
            }
            catch (TException)
            {
                return;
            }
            catch (Exception ex)
            {
                Assert.Fail("Wrong exception thrown: " + ex.Message);
            }
            Assert.Fail("No exception thrown");
        }

        #endregion

        #region Helper class for testing exception and exception message

        [DataTestMethod]
        [DataRow("", "filename has to be provided")]
        [DataRow(null, "filename has to be provided")]
        public void IsValidLogFileName_EmptyFileName_Throws_HelperAssert(string fileName, string exMessage)
        {
            var analyzer = new LogAnalyzer();

            AssertException.Throws<ArgumentException>(() => analyzer.IsValidLogFileName(fileName), exMessage);
        }

        #endregion
    }
}
