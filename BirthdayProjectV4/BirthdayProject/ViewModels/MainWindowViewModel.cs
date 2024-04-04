using BirthdayProject.Navigations;
using BirthdayProject.Views;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BirthdayProject.ViewModels
{
    class MainWindowViewModel : BaseNavigationViewModel<NavigationTypes> 
    {
        Window _main;
        public MainWindowViewModel(Window main)
        {
            _main = main;
            Navigate(NavigationTypes.Main);
        }

        protected override INavigatable<NavigationTypes> CreateViewModel(NavigationTypes type)
        {
            switch (type)
            {
                case NavigationTypes.Main:
                    return new MainViewModel(() => Navigate(NavigationTypes.New), () => Navigate(NavigationTypes.Edit), () => Navigate(NavigationTypes.Main));
                case NavigationTypes.New:
                    return new AddNewViewModel(_main, () => Navigate(NavigationTypes.Main));
                case NavigationTypes.Edit:
                    return new EditViewModel(_main, () => Navigate(NavigationTypes.Main));
                default:
                    return null;
            }
        }
    }
}
