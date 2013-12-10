using iqchampion_design.ServiceReference;
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
using System.ComponentModel;
using IQUtil;

namespace iqchampion_design
{
    public partial class Lobby : Window
    {
        Menu parent = null;
        private BackgroundWorker refreshworker = null;
        private BackgroundWorker activityworker = null;

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

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri(".\\Resources\\icon_backarrow.png", UriKind.Relative));
            bekk.Background = myBrush;

            ImageBrush myBrush2 = new ImageBrush();
            myBrush2.ImageSource = new BitmapImage(new Uri(".\\Resources\\icon_closebutton.png", UriKind.Relative));
            klóz.Background = myBrush2;

            this.parent = parent;



            chatbox.Items.Clear();

            refreshworker = new BackgroundWorker();
            refreshworker.WorkerSupportsCancellation = true;
            refreshworker.WorkerReportsProgress = true;
            refreshworker.DoWork += refresh;
            refreshworker.ProgressChanged += refreshUI;

            activityworker = new BackgroundWorker();
            activityworker.WorkerSupportsCancellation = true;
            activityworker.DoWork += wait;
            activityworker.RunWorkerCompleted += doActivity;
        }


        private void doActivity(object sender, RunWorkerCompletedEventArgs e)
        { }
        private int PingPeriod
        {
            get { return Login.PingPeriod; }
        }




        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            refreshworker.RunWorkerAsync();
            activityworker.RunWorkerAsync();

            String[] users = Client.getUserList();
            foreach (string s in users)
            {
                FelhazsnaloList.Items.Add(s);
            }
        }

        private void refresh(object sender, DoWorkEventArgs e)
        {
            do
            {
                //(sender as BackgroundWorker).ReportProgress(0, Client.getMesages(User));
                Thread.Sleep(PingPeriod);
            } while (!(sender as BackgroundWorker).CancellationPending);
        }

        public void refreshUI(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is Message[])
            {
                Message[] chat = (e.UserState as Message[]);
                chatbox.Items.Clear();
                for (int i = chat.Length - 1; i >= 0; i--)
                {
                    User u = chat[i].Sender;
                    TextBlock tb = new TextBlock();
                    tb.TextWrapping = TextWrapping.Wrap;
                    tb.Foreground = new SolidColorBrush(Color.FromRgb(u.Color[0], u.Color[1], u.Color[2]));
                    string s = chat[i].Time.ToString("HH:mm") + " [" + u.Name + "] " + Environment.NewLine + chat[i].Msg;
                    tb.Text = s;
                    chatbox.Items.Add(tb);
                }
            }
        }

        private void wait(object sender, DoWorkEventArgs e)
        {
            try
            {
                while ((States)(e.Result = Client.getMyState(User)) == States.IDLE)
                {
                    Thread.Sleep(PingPeriod);
                }
            }
            catch (Exception exc)
            {
                //!!!!!!!!! Wait miatt kihal a progi
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private void enterMessage(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Client.Send(User, chatText.Text);
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
                FelhasznaloPage page = new FelhasznaloPage(item.Content.ToString());
                pageContainer.Content = page;
            }

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

        private void chatText_GotFocus(object sender, RoutedEventArgs e)
        {
            chatText.Text = "";
        }

    }
}
