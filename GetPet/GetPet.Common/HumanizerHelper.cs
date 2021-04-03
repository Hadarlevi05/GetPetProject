using System;

namespace GetPet.Common
{
    public static class HumanizerHelper
    {
        public static string DateHumanize(this DateTime date)
        {
            var now = DateTime.Now;

            var age = date.Subtract(now);

            var dateSpan = DateTimeSpan.CompareDates(date, now);
            var yearsString = GetYears(dateSpan.Years);
            var monthsString = GetMonths(dateSpan.Months);

            if (string.IsNullOrWhiteSpace(monthsString))
                return yearsString;

            return $"{yearsString} ו{monthsString}";

        }

        public static string GetYears(int age)
        {
            switch (age)
            {
                case 1: return "שנה";
                case 2: return "שנתיים";
                case 3: return "שלוש שנים";
                case 4: return "ארבע שנים";
                case 5: return "חמש שנים";
            }
            return string.Empty;
        }

        public static string GetMonths(int month)
        {
            switch (month)
            {
                case 1: return "חודש";
                case 2: return "חודשיים";
                case 3: return "שלושה חודשים";
                case 4: return "ארבעה חודשים";
                case 5: return "חמישה חודשים";
                case 6: return "שישה חודשים";
                case 7: return "שבעה חודשים";
                case 8: return "שמונה חודשים";
                case 9: return "תשעה חודשים";
                case 10: return "עשרה חודשים";
                case 11: return "11 חודשים";
            }
            return string.Empty;
        }

        public static string GenderHumanize(this int gender) => gender switch
        {
            0 => "בן",
            1 => "בת",
            _ => string.Empty,
        };
    }
}