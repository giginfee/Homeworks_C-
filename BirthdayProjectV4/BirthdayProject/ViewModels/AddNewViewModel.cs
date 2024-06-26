﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BirthdayProject.Tools;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.DirectoryServices.ActiveDirectory;
using BirthdayProject.Navigations;

namespace BirthdayProject.ViewModels
{
    internal class AddNewViewModel : INavigatable<NavigationTypes>, INotifyPropertyChanged
    {
        private bool proceeded=false;
        private Action _gotoMain;
        private RelayCommand<object>? _gotoMainCommand;

        private String _firstName = "";
        private String _surname = "";
        private String _email = "";

        private String _isBirthday = "";
        private String _isAdult = "";

        private String _westernZodiacPrimeText = "Your zodiac sign is ";

        private String _chineseZodiacPrimeText = "Your chinese zodiac is ";

        private String _agePrimeText = "Your age is ";

        private DateTime? _selectedDate;
        private Person _person=new Person("п","п", DateTime.Now);

        private Window _main;

        public NavigationTypes ViewType
        {
            get
            {
                return NavigationTypes.New;
            }
        }
        public AddNewViewModel(Window main, Action gotoMain)
        {
            _main = main;
            _gotoMain = gotoMain;
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
            
            _main.IsEnabled = false;
            await Task.Delay(500);

            bool canProceed = await Task.Run(() => {
                try
                {
                    _person = new Person(_firstName, _surname, _email, (DateTime)_selectedDate);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
            });
            _main.IsEnabled = true;
            if (!canProceed)
            {
                return;
            }

            MainViewModel.AddNewPerson(_person);
            
            if (_person.IsBirthday)
            {
                MessageBox.Show("New person added to the list. By the way, today is his/her birthday.");
            }
            else
            {
                MessageBox.Show("New person added to the list.");
            }
            _gotoMain.Invoke();



        }


        public RelayCommand<object> GotoMainCommand
        {
            get
            {
                return _gotoMainCommand ??= new RelayCommand<object>(_ => _gotoMain.Invoke());
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
