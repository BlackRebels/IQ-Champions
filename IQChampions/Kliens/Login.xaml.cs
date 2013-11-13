using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
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
using iqchampion_design.ServiceReference;
using IQUtil;

namespace iqchampion_design
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_TextInput_1(object sender, TextCompositionEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (IQServiceClient client = new IQServiceClient())
            {
                // debug, adminra belép
                bool authenticated = false;
                if (TextBoxUser.Text.Equals("admin")) authenticated = client.Login("admin", "admin");
                else authenticated = client.Login(Hash.generate(TextBoxUser.Text), Hash.generate(TextBoxPass.Password));

                if (authenticated)
                {
                    Menu menuWindow = new Menu();
                    menuWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hibás felhasználónév vagy jelszó!");
                    TextBoxUser.Text = null;
                    TextBoxPass.Password = null;
                }
            }
        }

        private void link(object sender, MouseButtonEventArgs e)
        {
            Hyperlink h = new Hyperlink();
            h.NavigateUri = new Uri("http://index.hu");

        }

        private void Hyperlink_RequestNavigate_1(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
