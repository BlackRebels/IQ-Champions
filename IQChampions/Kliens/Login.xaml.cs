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
    public partial class Login : Window
    {
        private static int pingPeriod = -1;
        private static IQServiceClient client = new IQServiceClient();
        private string user = null;

        public static int PingPeriod
        {
            get { return Login.pingPeriod; }
        }
        public static IQServiceClient Client
        {
            get { return Login.client; }
        }
        public string User
        {
            get { return this.user; }
        }

        public Login()
        {
            InitializeComponent();
        }

        public void ClearFields()
        {
            TextBoxUser.Text = null;
            TextBoxPass.Password = null;
        }

        private void ButtonClickLogin(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            try
            {
                pingPeriod = client.PingPeriod();
                bool authenticated = client.Login(TextBoxUser.Text, Hash.generate(TextBoxPass.Password));
                if (authenticated)
                {
                    user = TextBoxUser.Text;
                    Menu menuWindow = new Menu(this);
                    menuWindow.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hibás felhasználónév vagy jelszó!");
                    TextBoxUser.Text = null;
                    TextBoxPass.Password = null;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("A szerver jelenleg nem elérhető!");
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void RegistrationRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }

        private void LoginVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                user = null;
            }
        }

        private void EnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) ButtonClickLogin(sender, null);
        }
    }
}
