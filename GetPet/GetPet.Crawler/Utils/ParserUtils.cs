using System;
using System.Collections.Generic;
using System.Text;

namespace GetPet.Crawler.Utils
{
    public static class ParserUtils
    {
        public static string ConvertGender(string input)
        {
            // TODO: enum
            if (input.Equals("female", StringComparison.OrdinalIgnoreCase))
            {
                return "נקבה";
            }
            else if (input.Equals("male", StringComparison.OrdinalIgnoreCase))
            {
                return "זכר";
            }
            else
            {
                return ""; // Handle missing case
            }
        }

        public static int ConvertYear(string input)
        {
            switch (input)
            {
                case "שנה":
                    {
                        return 1;
                    }
                case "שנתיים":
                    {
                        return 2;
                    }
                case "שלוש שנים":
                    {
                        return 3;
                    }
                default:
                    {
                        return -1;
                    }
            }
        }
    }
}
