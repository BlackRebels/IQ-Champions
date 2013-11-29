using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IQChampionsServiceLibrary
{
    public class Cell
    {
        public User Owner { get; set; }
        public Boolean isBase { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Cell()
        {
        }

        public Cell(int x, int y)
        {
            Row = x;
            Col = y;
            isBase = false;
        }
        public bool neighbor(Cell cell)
        {
            if (this.Col == cell.Col && this.Row == cell.Row) return false;
            if (Math.Abs(this.Col - cell.Col) <= 1 && Math.Abs(this.Row - cell.Row) <= 1) return true;
            return false;
        }
    }
}
