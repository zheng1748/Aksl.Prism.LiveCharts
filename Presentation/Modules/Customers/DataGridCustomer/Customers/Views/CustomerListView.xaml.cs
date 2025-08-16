﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aksl.Modules.DataGridCustomer.Views
{
    /// <summary>
    /// CustomerListView.xaml 的交互逻辑
    /// </summary>
    public partial class CustomerListView : UserControl
    {
        public CustomerListView()
        {
            InitializeComponent();

            customerDataGrid.LoadingRow += (sender, e) =>
            {
                e.Row.Header = e.Row.GetIndex() + 1;
            };
        }
    }
}
