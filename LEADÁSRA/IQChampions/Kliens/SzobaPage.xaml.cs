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

namespace iqchampion_design
{
    /// <summary>
    /// Interaction logic for SzobaPage.xaml
    /// </summary>
    public partial class SzobaPage : Page
    {
        public SzobaPage()
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(".\\Resources\\keret.png", UriKind.Relative));
            this.Background = myBrush;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Userek bepakolása szobákba

            GameTable game = new GameTable(null);
            game.Show();
        }
    }
}
