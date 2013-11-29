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
        public List<Cell> Table { get; set; }

        public GameTable()
        {
        }

        public GameTable(int x, int y)
        {
            Table = new List<Cell>();
            for (int i = 0; i < x; i++) for (int j = 0; j < y; j++)
                {
                    if ((i == 0 && j == 0) || (i == 0 && j == y-1) || (i == x-1 && j == 0) || (i == x-1 && j == y-1))
                    {
                        Cell cella = new Cell(i, j);
                        cella.isBase = true;
                        Table.Add(cella);
                    }
                    else
                    {
                        Table.Add(new Cell(i, j));

                    }
                }

        }
    }
}
