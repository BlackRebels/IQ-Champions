using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IQChampionsServiceLibrary
{
    public class GameTable
    {
        public Cell[] Table { get; set; }

        public GameTable()
        {
        }

        public GameTable(int x, int y)
        {
            Table = new Cell[x * y];
            int k = 0;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Table[k++] = new Cell(i, j);
                }
            }
        }
    }
}
