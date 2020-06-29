using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvartaJump
{
    class Player
    {
        private int window_width;
        private int window_height;

        private int width = 50;
        private int height = 50;

        private int speed = 0;
        public Player(int _window_width, int _window_height)
        {
            window_width = _window_width;
            window_height = _window_height;
        }
        private int x;
        private int y;
        public int X { 
            get
            {
                return x; 
            }
            set {
                if(value + width < window_width && value >= 0)
                {
                    x = value;
                }
            }
        }
        public int Y { get => y; set => y = value; }
        public int Speed { get => speed;
            set
            {
                speed = value;
            }
        }
    }
}
