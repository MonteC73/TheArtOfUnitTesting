using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogAn.Tests
{
    [TestClass]
    public class MemCalculatorTests
    {
        [TestMethod]
        public void Sum_ByDefault_ReturnsZero()
        {
            MemCalculator calc = new MemCalculator();

            int sum = calc.Sum();

            Assert.AreEqual(0, sum);
        }

        [TestMethod]
        public void Add_WhenCalled_ChangesSum()
        {
            MemCalculator calc = new MemCalculator();

            calc.Add(1);
            int sum = calc.Sum();

            Assert.AreEqual(1, sum);
        }
    }
}
