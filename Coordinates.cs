using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba
{
    internal class Coordinates
    {
        public int Color { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public Coordinates(int Row, int Column)
        {
            this.Column = Column;
            this.Row = Row;
        }
        public Coordinates(int Row, int Column, int Color)
        {
            this.Column = Column;
            this.Row = Row;
            this.Color = Color;
        }
    }
}
