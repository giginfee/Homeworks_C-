using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BirthdayProjectV1.Tools;
using BirthdayProjectV1.ViewModels;

namespace BirthdayProjectV1.Views
{

    public partial class BirthdayView : UserControl
    {
        BirthdayViewModel birthdayViewModel;
        public BirthdayView()
        {
            InitializeComponent();
            DataContext = birthdayViewModel = new BirthdayViewModel();  
        }
        
    /*    private void BCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (!DpBirthday.SelectedDate.HasValue)
            {
                MessageBox.Show("Date was not selected!");
                return;
            }

            DateTime selectedDate = (DateTime)DpBirthday.SelectedDate;

            DateContoller dateContoller = new DateContoller(selectedDate);
            ZodiacDetector zodiacDetector = new ZodiacDetector(selectedDate);

            if (!dateContoller.IsValidBirthDate())
            {
                MessageBox.Show("Date is not valid!");
                return;
            }

            TBAge.Text = $"Your age is {dateContoller.GetFullYears()}";
            TBWesternZodiac.Text = $"Your zodiac sign is {zodiacDetector.GetWesternZodiacSign()}";
            TBChineseZodiac.Text = $"Your chinese sign is {zodiacDetector.GetChineseZodiacSign()}";


        }*/
    }
}
