using BirthdayProject.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayProject.Navigations
{
    internal abstract class BaseNavigationViewModel<TObject> : INotifyPropertyChanged where TObject : Enum
    {
        private INavigatable<TObject> _viewModel;
        List<INavigatable<TObject>> _viewModels = new List<INavigatable<TObject>>();

        public event PropertyChangedEventHandler PropertyChanged;
        public INavigatable<TObject> ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                if (_viewModel == value)
                    return;
                _viewModel = value;
                OnPropertyChanged(nameof(ViewModel));
            }
        }
       

        internal void Navigate(TObject type)
        {

            if (ViewModel != null && ViewModel.ViewType.Equals(type))
                return;

            INavigatable<TObject> viewModel = GetViewModel(type);

            if (viewModel == null)
                return;

            _viewModels.Add(viewModel);
            ViewModel = viewModel;


        }

        private INavigatable<TObject> GetViewModel(TObject type)
        {
            INavigatable<TObject> viewModel = _viewModels.FirstOrDefault(viewModel => viewModel.ViewType.Equals(type));

            if (viewModel != null)
                return viewModel;

            return CreateViewModel(type);
        }

        protected abstract INavigatable<TObject> CreateViewModel(TObject type);

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
   
}
