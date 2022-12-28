﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace EmailManager.UI.Views
{
    /// <summary>
    /// Interaction logic for EmailDetailsView.xaml
    /// </summary>
    public partial class EmailDetailsView : UserControl
    {
        public EmailDetailsView()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)e.OriginalSource;
            var chromeRepo = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\Chrome.exe";
            Process.Start(chromeRepo, link.NavigateUri.AbsoluteUri);
        }
    }
}
