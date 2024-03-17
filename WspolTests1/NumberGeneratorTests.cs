using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wspol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wspol.Tests
{
    [TestClass()]
    public class NumberGeneratorTests
    {
        [TestMethod()]
        public void GenerateRandomNumberTest()
        {
            NumberGenerator numberGenerator = new NumberGenerator();

            int randomNumber = numberGenerator.GenerateRandomNumber();

            Assert.IsTrue(randomNumber >= 1 && randomNumber <= 100);
            Assert.IsFalse(randomNumber < 1 && randomNumber > 100);
        }
    }
}