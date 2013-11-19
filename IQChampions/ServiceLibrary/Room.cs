using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IQChampionsServiceLibrary
{
    public class Room
    {
        public const int MAXPLAYERS = 4;

        public GameTable Table { get; set; }
        public string Name { get; set; }
        public List<User> Players { get; set; }
        public Question Question { get { return question; } }
        public bool Finished { get { return finished; } }

        private const int turnTimeout = 30000;
        private const int maxturns = 3;
        private static readonly byte[] defaultcolor = new byte[] { 225, 225, 225 };
        private static readonly byte[][] playercolors = new byte[][] { 
            new byte[] { 0, 0, 255 },   // Blue
            new byte[] { 0, 255, 0 },   // Green
            new byte[] { 255, 0, 0 },   // Red
            new byte[] { 255, 255, 0 }  // Yellow
        };

        private BackgroundWorker turnworker = null;
        private List<User> rollable;
        private User actualPlayer = new User();
        private Cell actualCell = null;
        private Question question = null;
        private bool finished = false;


        public Room()
        {
            init();
            Name = Guid.NewGuid().ToString();
        }

        public Room(string name)
        {
            init();
            Name = name;

        }

        private void init()
        {
            Players = new List<User>();
            Table = new GameTable(6, 4);
            turnworker = new BackgroundWorker();
            turnworker.DoWork += turn;
        }

        public void addUser(User user)
        {
            if (Players.Count <= Room.MAXPLAYERS)
            {
                user.Color = playercolors[Players.Count];
                Players.Add(user);
            }
            else throw new OverflowException("Too much users in " + Name + " room!");
        }

        public void start()
        {
            foreach (Cell c in Table.Table)
            {
                if (c.Col == 0 && c.Row == 0) c.Owner = Players[0];
                else if (c.Col == 0 && c.Row == 1) c.Owner = Players[0];
                else if (c.Col == 0 && c.Row == 4) c.Owner = Players[1];
                else if (c.Col == 0 && c.Row == 5) c.Owner = Players[1];
                else if (c.Col == 3 && c.Row == 0) c.Owner = Players[2];
                else if (c.Col == 3 && c.Row == 1) c.Owner = Players[2];

                else if (c.Col == 3 && c.Row == 4) c.Owner = Players[3];
                else if (c.Col == 3 && c.Row == 5) c.Owner = Players[3];
                else c.Owner = new User() { Color = defaultcolor, State = States.IDLE };
            }
            turnworker.RunWorkerAsync();
        }

        private void turn(object sender, DoWorkEventArgs e)
        {
            int turn = 0;
            do
            {
                actualPlayer = null;
                actualCell = null;
                question = new Question()
                     {
                         Questionn = "Példakérdés",
                         GoodAnswer = "Jóválasz",
                         BadAnswer1 = "Rosszválasz 1",
                         BadAnswer2 = "Rosszválasz 2",
                         BadAnswer3 = "Rosszválasz 3"
                     };
                selectNextMove();

                Stopwatch stopper = new Stopwatch();
                stopper.Start();
                // Lépésre vár
                while (stopper.ElapsedMilliseconds < turnTimeout && actualCell == null) Thread.Sleep(IQService.pingperiod);

                if (actualCell != null)
                {

                    actualCell.Owner.State = States.ANSWER;

                    stopper.Restart();
                    while (stopper.ElapsedMilliseconds < turnTimeout &&
                        (actualPlayer.State == States.ANSWER ||
                        (actualCell.Owner.State == States.ANSWER && actualCell.Owner.Name != null)))
                    {
                        Thread.Sleep(IQService.pingperiod);
                    }

                    // Lejárt az idő: a válasz rossz
                    if (actualPlayer.AnswerResult == null)
                        actualPlayer.AnswerResult = new AnswerResult() { Answer = false, Time = DateTime.Now };
                    if (actualCell.Owner.AnswerResult == null)
                        actualCell.Owner.AnswerResult = new AnswerResult() { Answer = false, Time = DateTime.Now };

                    if (actualPlayer.AnswerResult.Answer == false || actualPlayer.AnswerResult.Time > actualCell.Owner.AnswerResult.Time)
                    {
                        // Védő nyert
                        actualCell.Owner.Point += 10;
                        actualPlayer.Point -= 10;
                    }
                    else
                    {
                        // Támadó nyert
                        actualCell.Owner.Point -= 10;
                        actualPlayer.Point += 10;
                        actualCell.Owner = actualPlayer;
                    }

                    actualCell.Owner.State = States.IDLE;
                    actualCell.Owner.AnswerResult = null;
                }
                actualPlayer.State = States.IDLE;
                actualPlayer.AnswerResult = null;
                turn++;
            } while (turn < maxturns);

            // Játék vége
            foreach (User u in Players)
            {
                u.State = States.FINISHED;
            }
            finished = true;
        }

        public void selectNextMove()
        {
            foreach (User u in Players) u.State = States.IDLE;
            if (rollable == null || rollable.Count == 0)
            {
                rollable = new List<User>();
                rollable.AddRange(Players);
            }

            int r = IQService.rand.Next(rollable.Count);
            actualPlayer = rollable[r];
            rollable.RemoveAt(r);

            actualPlayer.State = States.MOVE;
        }

        public bool Move(int x, int y)
        {
            actualCell = Table.Table.Find(c => c.Row == x && c.Col == y);
            if (actualCell.Owner == actualPlayer)
            {
                // Saját mező, hiba
                actualCell = null;
                return false;
            }
            else
            {
                // Támad
                actualPlayer.State = States.ANSWER;
                actualCell.Owner.State = States.ANSWER;
                return true;
            }
        }

        public bool Answer(User user, int id)
        {
            user.State = States.IDLE;
            if (id == 0)
            {
                user.AnswerResult = new AnswerResult() { Answer = true, Time = DateTime.Now };
                return true;
            }
            else
            {
                user.AnswerResult = new AnswerResult() { Answer = false, Time = DateTime.Now };
                return false;
            }

        }

    }
}
