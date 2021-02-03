using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SedolValidator.Constant;
using SedolValidator.Interfaces;
using SedolValidator;


namespace SedolTests
{
    public class SedolValidatorTest
    {
        [TestCase(null)]
        public void SedolsNotSevenCharacters(string sedol)
        {
            var actual = new SedolValidate().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, false, Constants.INPUT_STRING_NOT_VALID_LENGTH);
            AssertValidationResult(expected, actual);
        }

        [TestCase("9123_51")]
        [TestCase("VA.CDE8")]
        public void SedolsContainingInvalidCharacters(string sedol)
        {
            var actual = new SedolValidate().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, false, Constants.SEDOL_CONTAINS_INVALID_CHARACTERS);
            AssertValidationResult(expected, actual);
        }

        [TestCase("9123458")]
        [TestCase("9ABCDE1")]
        public void UserDefinedSedolsWithCorrectChecksum(string sedol)
        {
            var actual = new SedolValidate().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, true, true, null);
            AssertValidationResult(expected, actual);
        }

        [TestCase("9123451")]
        [TestCase("9ABCDE8")]
        public void UserDefinedSedolsWithIncorrectChecksum(string sedol)
        {
            var actual = new SedolValidate().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, true, Constants.CHECKSUM_NOT_VALID);
            AssertValidationResult(expected, actual);
        }

        [TestCase("1234567")]
        public void SedolsWithIncorrectChecksum(string sedol)
        {
            var actual = new SedolValidate().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, false, false, Constants.CHECKSUM_NOT_VALID);
            AssertValidationResult(expected, actual);
        }

        [Test]
        [TestCase("0709954")]
        [TestCase("B0YBKJ7")]
        
        public void ValidSedols(string sedol)
        {
            var actual = new SedolValidate().ValidateSedol(sedol);
            var expected = new SedolValidationResult(sedol, true, false, null);
            AssertValidationResult(expected, actual);
        }

        private static void AssertValidationResult(ISedolValidationResult expected, ISedolValidationResult actual)
        {
            Assert.AreEqual(expected.InputString, actual.InputString, "Input String Failed");
            Assert.AreEqual(expected.IsValidSedol, actual.IsValidSedol, "Is Valid Failed");
            Assert.AreEqual(expected.IsUserDefined, actual.IsUserDefined, "Is User Defined Failed");
            Assert.AreEqual(expected.ValidationDetails, actual.ValidationDetails, "Validation Details Failed");
        }
    }
}
