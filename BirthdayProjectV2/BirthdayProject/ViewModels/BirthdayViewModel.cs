using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BirthdayProject.Tools;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.DirectoryServices.ActiveDirectory;

namespace BirthdayProject.ViewModels
{
    internal class BirthdayViewModel : INotifyPropertyChanged
    {
        private bool proceeded=false;
        private String _firstName = "";
        private String _surname = "";
        private String _email = "";

        private String _isBirthday = "";
        private String _isAdult = "";

        private String _westernZodiacPrimeText = "Your zodiac sign is ";

        private String _chineseZodiacPrimeText = "Your chinese zodiac is ";

        private String _agePrimeText = "Your age is ";

        private DateTime? _selectedDate;
        private Person _person=new Person("п","п","п");

        private UserControl _main;

        public BirthdayViewModel(UserControl main)
        {
            _main = main;
        }
        public string FirstName
        {
            get => nullOrValue(_person.Name);
             set
            {
                _firstName = value;
            }
        }
        public string Email
        {
            get => nullOrValue(_person.Email);
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get => _person.Surname; 
            set
            {
                _surname = value;
            }
        }
        public string FullName
        {
            get => nullOrValue(_surname + " " + _firstName);
            set { OnPropertyChanged(); }
        }
        public string IsBirthday
        {
            get => nullOrValue(_isBirthday);
           
            set
            {
                if (_person.IsBirthday)
                    _isBirthday = "Today is your birthday. Congrats!";
                OnPropertyChanged();
            }
        }
         public string IsAdult
        {
            get => nullOrValue(_isAdult);
                     
            set
            {
                if (_person.IsAdult)
                    _isAdult = "You are an adult already";
                else
                    _isAdult = "You are not an adult yet";
                OnPropertyChanged();

            }
        } 
        public string BirthDate
        {
            get => nullOrValue(_person.BirthDate.ToShortDateString());
            
            set { OnPropertyChanged(); }
        }


        private RelayCommand<object> _proceedCommand;


        public event PropertyChangedEventHandler? PropertyChanged;
        public DateTime? Date { get => _person.BirthDate; set {

                _selectedDate = value;             

            }
        }
        public String WesternZodiac { get => nullOrValue(_westernZodiacPrimeText + _person.WesternSign) ; set {  OnPropertyChanged();} }
        public String ChineseZodiac { get => nullOrValue(_chineseZodiacPrimeText + _person.ChineseSign); set { OnPropertyChanged(); } }

        public String Age { get => nullOrValue(_agePrimeText + _person.Age.ToString()); set { OnPropertyChanged(); } }

        public RelayCommand<object> ProceedCommand
        {
            get
            {
                return _proceedCommand ??= new RelayCommand<object>(_ => LoadResults(), CanExecute);
            }
        }


        private async void LoadResults()
        { 
            if (!_selectedDate.HasValue)
            {
                MessageBox.Show("Date was not selected!");
                return ;
            }  
            if (!(new DateContoller((DateTime)_selectedDate)).IsValidBirthDate())
            {
                MessageBox.Show("Date is not valid!");
                return ;
            }
            _main.IsEnabled = false;
            await Task.Delay(1000);
            _person = await Task.Run(()=>{  return new Person(_firstName, _surname, _email, (DateTime)_selectedDate); });
            _main.IsEnabled = true;

           
            proceeded = true;
            WesternZodiac = "";
            ChineseZodiac = "";
            Age = "";
            IsAdult = "";
            IsBirthday = "";
            FullName = "";
            Email = _email;
            BirthDate = "";
            if (_person.IsBirthday)
            {
                MessageBox.Show("OMG, today is your birthday?! May all of your wishes come true!");
            }
            

        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool CanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_surname) && !String.IsNullOrWhiteSpace(_email) && !String.IsNullOrWhiteSpace(_firstName);
        }

        private string nullOrValue(string value)
        {
            {
                if (proceeded)
                    return value;
                else
                    return "";
            }
        }


    }
}
