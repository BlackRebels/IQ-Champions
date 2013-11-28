﻿using iqchampion_design.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class Lobby : Window
    {
        Menu parent = null;

        public string User
        {
            get { return Login.User; }
        }
        public IQServiceClient Client
        {
            get { return Login.Client; }
        }

        public Lobby(Menu parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private void enterMessage(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                chatWindow.Items.Add(chatText.Text);
                chatText.Text = "";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void onFelhasznaloClicked(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(FelhazsnaloList, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                FelhasznaloPage page = new FelhasznaloPage();
                pageContainer.Content = page;
            }
            
        }


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            String[] users = Client.getUserList();
            foreach (string s in users)
            {
                FelhazsnaloList.Items.Add(s);
            }
            /*
            while (true)
            {
                //Client.refreshRoomList();
          * elindították-e a szobámat
                Thread.Sleep(Login.PingPeriod);
            }*/
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onSzobaClicked(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(roomList, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                SzobaPage szoba = new SzobaPage();
                pageContainer.Content = szoba;
            }
        }

    }
}