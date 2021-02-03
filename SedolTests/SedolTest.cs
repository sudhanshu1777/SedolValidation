using NUnit.Framework;
using SedolValidator;
using System.Globalization;

namespace SedolTests
{
    public class SedolTest
    {
        [Test]
        public void ValueForBIs11()
        {
            var actual = SedolV.CharCode('B');
            Assert.AreEqual(11, actual);
        }

        [Test]
        public void ValueForZIs35()
        {
            var actual = SedolV.CharCode('Z');
            Assert.AreEqual(35, actual);
        }

        [TestCase("0709954")]
        [TestCase("B0YBKJ7|")]
        public void CheckValidNonUserDefinedSedolTest(string input)
        {
            SedolV sedol = new SedolV(input);
            var actual = sedol.CalculateCheckDigit;
            Assert.AreEqual(input[6].ToString(CultureInfo.InvariantCulture), actual.ToString(CultureInfo.InvariantCulture));
        }

        
        [TestCase(null)]
        [TestCase("")]
        [TestCase("123456789")]
        [TestCase("12")]
        public void ValidLengthChecks(string input)
        {
            SedolV sedol = new SedolV(input);
            Assert.IsFalse(sedol.IsValidLength);
        }


        [TestCase("9123458")]
        [TestCase("9ABCDE1")]
        public void UserDefinedSedols(string input)
        {
            SedolV sedol = new SedolV(input);
            Assert.IsTrue(sedol.IsUserDefined);
        }


        [TestCase("9123451")]
        [TestCase("9ABCDE8")]
        public void UserDefinedSedolsWithIncorrectChecksum(string input)
        {
            SedolV sedol = new SedolV(input);
            Assert.IsFalse(sedol.HasValidCheckDigit);
        }

        [TestCase("9123_51")]
        [TestCase("VA.CDE8")]
        public void SedolsContainingInvalidCharacters(string input)
        {
            SedolV sedol = new SedolV(input);
            Assert.IsFalse(sedol.IsValidCharcter);
        }


    }
}