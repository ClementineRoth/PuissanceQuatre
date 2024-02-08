using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuissanceQuatre.Model
{
    internal class Player
    {
        public int color;
        public string name;

        public Player(int color, string name)
        {
            this.color = color;
            this.name = name;
        }
    }
}
