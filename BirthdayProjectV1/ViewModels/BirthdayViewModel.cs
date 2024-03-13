using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BirthdayProjectV1.Tools;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BirthdayProjectV1.ViewModels
{
    internal class BirthdayViewModel : INotifyPropertyChanged
    {

        private String _westernZodiacPrimeText = "Your zodiac sign is ";
        private String _westernZodiacSign = "";

        private String _chineseZodiacPrimeText = "Your chinese zodiac is ";
        private String _chineseZodiacSign = "";

        private String _agePrimeText = "Your age is ";
        private String _age = "";

        private DateTime? _selectedDate;
        private DateContoller _dateContoller;
        private ZodiacDetector _zodiacDetector;

        private RelayCommand<object> _calculateCommand;


        public event PropertyChangedEventHandler? PropertyChanged;
        public DateTime? Date { get => _selectedDate; set {

                _selectedDate = value;
                if (value.HasValue)
                {
                    _dateContoller = new DateContoller((DateTime)_selectedDate);
                    _zodiacDetector = new ZodiacDetector((DateTime)_selectedDate);

                }

            }
        }
        public String WesternZodiac { get => _westernZodiacPrimeText + _westernZodiacSign; set { _westernZodiacSign = _zodiacDetector.GetWesternZodiacSign(); OnPropertyChanged();} }
        public String ChineseZodiac { get => _chineseZodiacPrimeText + _chineseZodiacSign; set { _chineseZodiacSign = _zodiacDetector.GetChineseZodiacSign(); OnPropertyChanged(); } }

        public String Age { get => _agePrimeText + _age; set { _age = _dateContoller.GetFullYears().ToString(); OnPropertyChanged(); } }

        public RelayCommand<object> CalculateCommand
        {
            get
            {
                return _calculateCommand ??= new RelayCommand<object>(_ => LoadResults());
            }
        }


        private void LoadResults()
        { 
            if (!_selectedDate.HasValue)
            {
                MessageBox.Show("Date was not selected!");
                return ;
            }  
            if (_dateContoller is null || !_dateContoller.IsValidBirthDate())
            {
                MessageBox.Show("Date is not valid!");
                return ;
            }
            WesternZodiac = "";
            ChineseZodiac = "";
            Age = "";
            if (_dateContoller.IsToday())
            {
                MessageBox.Show("OMG, today is your birthday?! May all of your wishes come true!");
            }
            

        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



    }
}
