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

namespace iqchampion_design
{
    /// <summary>
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class Lobby : Window
    {
        public Lobby()
        {
            InitializeComponent();
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
            Menu menu = new Menu(null);
            menu.Show();
            this.Close();
        }


        private void onFelhasznaloClicked(object sender, MouseButtonEventArgs e)
        {
            FelhasznaloPage page = new FelhasznaloPage();
            pageContainer.Content = page;
        }

        private void onSzobaClicked(object sender, MouseButtonEventArgs e)
        {
            SzobaPage szoba = new SzobaPage();
            pageContainer.Content = szoba;
        }
    }
}
