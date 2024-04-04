using BirthdayProject.Navigations;
using BirthdayProject.Repositories;
using BirthdayProject.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BirthdayProject.ViewModels
{
    internal class EditViewModel:INavigatable<NavigationTypes>, INotifyPropertyChanged
    {
        private bool proceeded = false;
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
    private Person _person = new Person("п", "п", DateTime.Now);

    private Window _main;
    private static PersonRepository _personRepository;


        public NavigationTypes ViewType
    {
        get
        {
            return NavigationTypes.New;
        }
    }
    public EditViewModel(Window main, Action gotoMain)
    {
            _main = main;
            _gotoMain = gotoMain;
            proceeded = true;
            _personRepository = new PersonRepository();
            _person = _personRepository.GetSelected();
            FirstName = _person.Name;
            Surname = _person.Surname;
            Email = _person.Email;
            Date = _person.BirthDate;
            _personRepository.RemoveSelected();

        }
    public string FirstName
    {
        get => nullOrValue(_firstName);
        set
        {
            _firstName = value;
        }
    }
    public string Email
    {
        get => nullOrValue(_email);
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }
    public string Surname
    {
        get => _surname;
        set
        {
            _surname = value;
        }
    }
   
    

    private RelayCommand<object> _proceedCommand;


    public event PropertyChangedEventHandler? PropertyChanged;
    public DateTime? Date
    {
        get => _selectedDate; 
        set
        {

            _selectedDate = value;

        }
    }
   
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
            return;
        }

       

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
        if (!canProceed)
        {
            return;
        }

        MainViewModel.AddNewPerson(_person);

     
        MessageBox.Show("Person's info has changed.");
        
        
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
