using iqchampion_design.ServiceReference;
using IQChampionsServiceLibrary;
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
using System.Windows.Shapes;
using System.ComponentModel;

namespace iqchampion_design
{
    public partial class GameTable : Window
    {
        private Menu parent = null;
        private BackgroundWorker refreshworker = null;
        private bool enableMoving = false;

        private string User
        {
            get { return Login.User; }
        }
        private IQServiceClient Client
        {
            get { return Login.Client; }
        }
        private int PingPeriod
        {
            get { return Login.PingPeriod; }
        }

        public GameTable(Menu parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.Title = parent.Title;

            refreshworker = new BackgroundWorker();
            refreshworker.WorkerSupportsCancellation = true;
            refreshworker.WorkerReportsProgress = true;
            refreshworker.DoWork += refresh;
            refreshworker.ProgressChanged += setCellColor;
            refreshworker.RunWorkerCompleted += doActivity;
        }


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            refreshworker.RunWorkerAsync();
        }

        private void refresh(object sender, DoWorkEventArgs e)
        {
            do
            {
                ServiceReference.GameTable table = Client.getGameTable(User);
                foreach (Cell c in table.Table)
                {
                    (sender as BackgroundWorker).ReportProgress(0, c);
                }
                Thread.Sleep(PingPeriod);
            } while ((States)(e.Result = Client.getMyState(User)) == States.IDLE);
        }

        private void setCellColor(object sender, ProgressChangedEventArgs e)
        {
            Cell c = e.UserState as Cell;
            ((Rectangle)GridGameTable.FindName("cell" + c.Row + c.Col)).Fill =
                           new SolidColorBrush(Color.FromRgb(c.Owner.Color[0], c.Owner.Color[1], c.Owner.Color[2]));
        }

        private void doActivity(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((States)e.Result == States.ANSWER)
            {
                //get the question here
            }
            else
            {
                enableMoving = true;
                GridGameTable.Opacity = 100;
                MessageBox.Show("Te jösz!");
            }
        }

        private void cellMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (enableMoving)
            {
                string rname = (sender as Rectangle).Name;
                if (Client.Move(User, int.Parse(rname.Substring(4, 1)), int.Parse(rname.Substring(5, 1))))
                {
                    GridGameTable.Opacity = 60;
                    enableMoving = false;
                    MessageBox.Show(Client.getQuestion(User).Questionn);
                    // megválaszolta...
                    bool good = Client.answerQuestion(User, 0);
                    refreshworker.RunWorkerAsync();
                    if (good)
                    {
                        MessageBox.Show("Helyes válasz");
                    }
                    else
                    {
                        MessageBox.Show("Rossz válasz");
                    }
                }
                else
                {
                    MessageBox.Show("Rossz mező!");
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parent.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextEntered(object sender, KeyEventArgs e)
        {

        }
    }
}
