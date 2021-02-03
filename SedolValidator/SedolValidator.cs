using System;
using System.Collections.Generic;
using System.Text;
using SedolValidator.Interfaces;
using SedolValidator.Constant;

namespace SedolValidator
{
    public class SedolValidate: ISedolValidator
    {
        public ISedolValidationResult ValidateSedol(string input)
        {
            // should/could be injected to separate functionality of validator with sedol class
            // sedol class knows about sedols, validator class knows how to combine the properties of a sedol
            // to generate the expected validation result.
            var sedol = new SedolV(input);

            var result = new SedolValidationResult
            {
                InputString = input,
                IsUserDefined = false,
                IsValidSedol = false,
                ValidationDetails = null
            };

            if (!sedol.IsValidLength)
            {
                result.ValidationDetails = Constants.INPUT_STRING_NOT_VALID_LENGTH;
                return result;
            }
            if (!sedol.IsValidCharcter)
            {
                result.ValidationDetails = Constants.SEDOL_CONTAINS_INVALID_CHARACTERS;
                return result;
            }
            if (sedol.IsUserDefined)
            {
                result.IsUserDefined = true;
                if (sedol.HasValidCheckDigit)
                {
                    result.IsValidSedol = true;
                    return result;
                }
                result.ValidationDetails = Constants.CHECKSUM_NOT_VALID;
                return result;
            }

            if (sedol.HasValidCheckDigit)
                result.IsValidSedol = true;
            else
                result.ValidationDetails = Constants.CHECKSUM_NOT_VALID;

            return result;
        }

    }
}
