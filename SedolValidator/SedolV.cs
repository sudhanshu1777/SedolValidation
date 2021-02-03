using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace SedolValidator
{
    public class SedolV
    {
        private readonly string inputvalue;
        private readonly List<int> lstweights;
        private const int CHECK_DIGIT_IDX = 6;
        private const int SEDOL_LENGTH = 7;
        private const int USER_DEFINED_IDX = 0;
        private const char USER_DEFINED_CHAR = '9';


        public SedolV(string input)
        {
            lstweights = new List<int> { 1, 3, 1, 7, 3, 9 };
            inputvalue = input;
        }

        public static int CharCode(char alphabet)
        {
            if (Char.IsLetter(alphabet))
                return Char.ToUpper(alphabet) - 55;
            return alphabet - 48;
        }

        //check digit
        public char CalculateCheckDigit
        {
            get
            {
                var codes = inputvalue.Take(SEDOL_LENGTH - 1).Select(CharCode).ToList();
                var weightedSum = lstweights.Zip(codes, (w, c) => w * c).Sum(); // The checksum is being calculated by multiplying the first six digits of codes by their weightings
                return Convert.ToChar(((10 - (weightedSum % 10)) % 10).ToString(CultureInfo.InvariantCulture));
            }
        }

        /// <summary>
        /// Null, empty string or string other than 7 characters long 
        /// </summary>
        public bool IsValidLength
        {            
            get {
                return !String.IsNullOrEmpty(inputvalue) && inputvalue.Length.Equals(SEDOL_LENGTH);                    
                }
        }

        /// <summary>
        /// return true if sedol is userdefine else false
        /// </summary>
        public bool IsUserDefined
        {
            get { return inputvalue[USER_DEFINED_IDX] == USER_DEFINED_CHAR; }
        }

        /// <summary>
        /// check if checkdigit is valid
        /// </summary>
        public bool HasValidCheckDigit
        {
            get { return inputvalue[CHECK_DIGIT_IDX] == CalculateCheckDigit; }
        }

        /// <summary>
        /// return true if character is valid else false
        /// </summary>
        public bool IsValidCharcter
        {
            get { return Regex.IsMatch(inputvalue, "^[a-zA-Z0-9]*$"); }
        }
    }

}

