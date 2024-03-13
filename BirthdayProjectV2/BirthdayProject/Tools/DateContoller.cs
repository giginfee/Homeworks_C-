using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayProject.Tools
{
    internal class DateContoller
    {

        private readonly DateTime _date;

        public DateContoller(DateTime date)
        {
            _date = date;
        }

        public int GetFullYears()
        {
            DateTime currentDate = DateTime.Now;
            int years = currentDate.Year - _date.Year;

            if (currentDate.Month < _date.Month || currentDate.Month == _date.Month && currentDate.Day < _date.Day)
            {
                years--;
            }

            return years;
        }
        public bool IsToday()
        {
            return _date.Date == DateTime.Now.Date;
        }
        public bool IsValidBirthDate()
        {
            int years = GetFullYears();
            return years >= 0 && years <= 135;
        }
    }
}
