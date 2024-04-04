using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BirthdayProject.Navigations;
using BirthdayProject.Repositories;
using BirthdayProject.Tools;

namespace BirthdayProject.ViewModels
{
    internal class MainViewModel : INavigatable<NavigationTypes>, INotifyPropertyChanged
    {

        private static ObservableCollection<Person> _people;
        private  Person _selected;
        private string _filter = "";
        private Action _gotoAddNew;
        private Action _gotoMain;
        private Action _gotoEdit;

        public event PropertyChangedEventHandler PropertyChanged;
        private static PersonRepository _personRepository;
        private RelayCommand<object>? _removeSelectedCommand;
        private RelayCommand<object>? _goToAddCommand;
        private RelayCommand<object>? _editSelectedCommand;


        public ObservableCollection<Person> People
        {
            get => _people;
            private set
            {
                _people = value;
                OnPropertyChanged();
            }
        }
        public Person SelectedPerson
        {
            get => _selected;
            set {
                if (value == null) {
                    _personRepository.RemoveSelected();
                } else
                {
                    _personRepository.SetNewSelected(value);

                }   
                    _selected = value;

            }
        }

        public NavigationTypes ViewType
        {
            get
            {
                return NavigationTypes.Main;
            }
        }

        private async Task RemovePerson()
        {
            if (SelectedPerson != null)
            {
                await Task.Run(() => _personRepository.Remove(SelectedPerson));
                People.Remove(SelectedPerson);
                OnPropertyChanged(nameof(People));
                SelectedPerson = null;

            }
        }
        public RelayCommand<object> RemoveSelectedCommand
        {
            get
            {
                return _removeSelectedCommand ??= new RelayCommand<object>(o => RemovePerson(), CanExecuteEditOrRemove);
            }
        }
        public RelayCommand<object> AddNewCommand
        {
            get
            {
                return _goToAddCommand ??= new RelayCommand<object>(_ => GoToAdd());
            }
        }
        private void GoToAdd()
        {
            SelectedPerson = null;
            _gotoAddNew.Invoke();
        }
        public RelayCommand<object> EditSelectedCommand
        {
            get
            {
                return _editSelectedCommand ??= new RelayCommand<object>(_ => GoToEditPerson(), CanExecuteEditOrRemove);
            }
        }
        public void GoToEditPerson()
        {
            _gotoEdit.Invoke();
        }
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    OnPropertyChanged();

                    if (string.IsNullOrWhiteSpace(_filter))
                    {
                        People = new ObservableCollection<Person>(_personRepository.GetAll());
                    }
                    else
                    {
                    People = new ObservableCollection<Person>(
                            from person in People
                            where (person.Surname.Contains(_filter, StringComparison.OrdinalIgnoreCase) || 
                            person.Name.Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                            person.Email.Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                            person.ShortBirthDate.Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                            person.Surname.Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                            person.Age.ToString().Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                            person.AdultOrChild.Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                            person.IsBirthday.ToString().Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                            person.WesternSign.Contains(_filter, StringComparison.OrdinalIgnoreCase) ||
                            person.ChineseSign.Contains(_filter, StringComparison.OrdinalIgnoreCase))
                            select person);
                    }
                }
            }
        }
        public static async void AddNewPerson(Person person)
        {
            if (_people != null)
            {
                await _personRepository.AddOrUpdateAsync(person);
                _people = new ObservableCollection<Person>(_personRepository.GetAll());
            }
        }

        public MainViewModel(Action gotoAddNew, Action gotoEdit, Action gotoMain)
        {
            _gotoAddNew = gotoAddNew;
            _gotoEdit = gotoEdit;
            _gotoMain = gotoMain;
            _personRepository = new PersonRepository();
            _people = new ObservableCollection<Person>(_personRepository.GetAll());
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool CanExecuteEditOrRemove(object o)
        {
            return _personRepository.GetSelected() != null;
        }
    }
}
