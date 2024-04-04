using BirthdayProject.Exceptions;
using BirthdayProject.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BirthdayProject.ViewModels
{
    class Person
    {
        private readonly string _name;
        private readonly string _surname;
        private readonly string _email;
        private readonly DateTime _birthDate;
        private int _age;
        private bool _isAdult;
        private string _westernSign;
        private string _chineseSign;
        private bool _isBirthday;
        private DateContoller _dateContoller;
        private ZodiacDetector _zodiacDetector;
        public string Name => _name;
        public string Surname => _surname;
        public string Email => _email;
        public DateTime BirthDate => _birthDate;
        public string ShortBirthDate => _birthDate.ToShortDateString();

        public string AdultOrChild => _isAdult? "Adult":"Child";
        public int Age => _age;
        public bool IsAdult => _isAdult;
        public string WesternSign => _westernSign;
        public string ChineseSign => _chineseSign;
        public bool IsBirthday => _isBirthday;


        [JsonConstructor]
        public Person(string Name, string Surname, string Email, DateTime BirthDate)
        {
           
            if (!IsValidEmail(Email)) {
                throw new InvalidEmailException(Email);
            };
            if (!(new DateContoller((DateTime)BirthDate)).IsNotTooDistantDate())
            {
                throw new DistantDateOfBirthException(BirthDate);
            }
            if (!(new DateContoller((DateTime)BirthDate)).IsNotFutureDate())
            {
                throw new FutureDateOfBirthException(BirthDate);
            }

            _name = Name;
            _surname = Surname;
            _email = Email;
            _birthDate = BirthDate;

            _dateContoller = new DateContoller((DateTime)BirthDate);
            _zodiacDetector = new ZodiacDetector((DateTime)BirthDate);
            _westernSign = _zodiacDetector.GetWesternZodiacSign();
            _chineseSign = _zodiacDetector.GetChineseZodiacSign();
             _age=_dateContoller.GetFullYears();
            _isAdult = _age >= 18;
            _isBirthday = _dateContoller.IsToday();
           
        }

        public Person(string name, string surname, string email) : this( name,  surname,  email, DateTime.Now.Date)
        {}

        public Person(string name, string surname, DateTime birthDate) : this(name, surname, "A@abc.com", birthDate)
        {}
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch 
            {
                return false; 
            }
        }
    }
}
