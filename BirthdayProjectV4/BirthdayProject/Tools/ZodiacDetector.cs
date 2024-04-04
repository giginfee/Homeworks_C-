using System;


namespace BirthdayProject.Tools
{
    internal class ZodiacDetector
    {
        private readonly DateTime _date;

        public ZodiacDetector(DateTime date)
        {
            _date = date;
        }

        public string GetWesternZodiacSign()
        {
            int day = _date.Day;
            int month = _date.Month;

            if (month == 3 && day >= 21 || month == 4 && day <= 19)
            {
                return "Aries";
            }
            else if (month == 4 && day >= 20 || month == 5 && day <= 20)
            {
                return "Taurus";
            }
            else if (month == 5 && day >= 21 || month == 6 && day <= 20)
            {
                return "Gemini";
            }
            else if (month == 6 && day >= 21 || month == 7 && day <= 22)
            {
                return "Cancer";
            }
            else if (month == 7 && day >= 23 || month == 8 && day <= 22)
            {
                return "Leo";
            }
            else if (month == 8 && day >= 23 || month == 9 && day <= 22)
            {
                return "Virgo";
            }
            else if (month == 9 && day >= 23 || month == 10 && day <= 22)
            {
                return "Libra";
            }
            else if (month == 10 && day >= 23 || month == 11 && day <= 21)
            {
                return "Scorpio";
            }
            else if (month == 11 && day >= 22 || month == 12 && day <= 21)
            {
                return "Sagittarius";
            }
            else if (month == 12 && day >= 22 || month == 1 && day <= 19)
            {
                return "Capricorn";
            }
            else if (month == 1 && day >= 20 || month == 2 && day <= 18)
            {
                return "Aquarius";
            }
            else if (month == 2 && day >= 19 || month == 3 && day <= 20)
            {
                return "Pisces";
            }
            return "unknown";
        }

        public string GetChineseZodiacSign()
        {
            int year = _date.Year;

            switch (year % 12)
            {
                case 0: return "Monkey";
                case 1: return "Rooster";
                case 2: return "Dog";
                case 3: return "Pig";
                case 4: return "Rat";
                case 5: return "Ox";
                case 6: return "Tiger";
                case 7: return "Rabbit";
                case 8: return "Dragon";
                case 9: return "Snake";
                case 10: return "Horse";
                case 11: return "Goat";
                default: return "unknown";
            }
        }

    }
}
