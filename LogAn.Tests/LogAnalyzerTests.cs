using LogAn.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LogAn.Tests
{
    [TestClass]
    public class LogAnalyzerTests
    {
        [DataTestMethod]
        [TestCategory("Chapter 2")]
        [DataRow("filewithbadextension.foo")]
        public void IsValidLogFileName_BadExtension_ReturnsFalse(string fileName)
        {
            // arrange
            var analyzer = MakeAnalyzer();

            // act
            var result = analyzer.IsValidLogFileName(fileName);

            // assert
            Assert.IsFalse(result);
        }

        [DataTestMethod]
        [TestCategory("Chapter 2")]
        [DataRow("filewithgoodextention.slf")]
        [DataRow("filewithgoodextention.SLF")]
        public void IsValidLogFileName_ValidExtension_ReturnsTrue(string fileName)
        {
            var analyzer = MakeAnalyzer();

            var result = analyzer.IsValidLogFileName(fileName);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("Chapter 2")]
        [ExpectedException(typeof(ArgumentException))]
        public void IsValidLogFileName_EmptyFileName_ThrowsException()
        {
            var analyzer = MakeAnalyzer();

            analyzer.IsValidLogFileName(string.Empty);
        }

        [DataTestMethod]
        [TestCategory("Chapter 2")]
        [DataRow("badfile.foo", false)]
        [DataRow("goodfile.slf", true)]
        public void IsValidFileName_WhenCalled_WasLastFileNameValid(string fileName, bool excpected)
        {
            var analyzer = MakeAnalyzer();

            analyzer.IsValidLogFileName(fileName);
            var actual = analyzer.WasLastFileNameValid;

            Assert.AreEqual(excpected, actual);
        }


        #region Microsoft recommendation for testing exception handling

        // Link to article: https://msdn.microsoft.com/library/jj159340.aspx

        [DataTestMethod]
        [TestCategory("Chapter 2")]
        [DataRow("")]
        [DataRow(null)]
        public void IsValidLogFileName_EmptyFileName_Throws(string fileName)
        {
            var analyzer = MakeAnalyzer();

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
        [TestCategory("Chapter 2")]
        [DataRow("", "filename has to be provided")]
        [DataRow(null, "filename has to be provided")]
        public void IsValidLogFileName_EmptyFileName_Throws_HelperAssert(string fileName, string exMessage)
        {
            var analyzer = MakeAnalyzer();

            AssertException.Throws<ArgumentException>(() => analyzer.IsValidLogFileName(fileName), exMessage);
        }

        #endregion

        #region Constructor Injection / Stubs

        [TestMethod]
        [TestCategory("Chapter 3")]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            var fakeExtensionManager = new FakeExtensionManager {WillBeValid = true};
            var analyzer = new LogAnalyzer(fakeExtensionManager);

            bool result = analyzer.IsValidLogFileName("shorslf");

            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("Chapter 3")]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            var fakeExtensionManager = new FakeExtensionManager
            {
                WillBeValid = false,
                WillThrow = new Exception("this is fake")
            };
            var analyzer = new LogAnalyzer(fakeExtensionManager);

            AssertException.Throws<Exception>(() => analyzer.IsValidLogFileName("anything.anyextension"), "this is fake");
        }

        #endregion

        private static LogAnalyzer MakeAnalyzer()
        {
            IExtenstionManager manager = new FileExtensionManager();
            return new LogAnalyzer(manager);
        }
    }
        

    internal class FakeExtensionManager : IExtenstionManager
    {
        public bool WillBeValid = false;
        public Exception WillThrow = null;

        public bool IsValid(string fileName)
        {
            if(WillThrow != null)
                throw WillThrow;

            return WillBeValid;
        }
    }
}
