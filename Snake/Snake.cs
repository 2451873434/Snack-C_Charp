using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake
    {
        public Point head, tail;
        public List<Point> p_s = new List<Point>();
        public int length;
        public char direction;
    }
}
