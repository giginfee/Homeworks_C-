﻿using System;
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
using BirthdayProject.Tools;
using BirthdayProject.ViewModels;

namespace BirthdayProject.Views
{

    public partial class AddNewView : UserControl
    {
        AddNewViewModel addNewViewModel;
        public AddNewView()
        {
            InitializeComponent();
            /*DataContext = addNewViewModel = new AddNewViewModel(this);*/
           
        }
        
    }
}
