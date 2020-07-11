using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SvartaJump
{
    internal class Player
    {
        protected int WINDOW_WIDTH;
        protected int WINDOW_HEIGHT;

        public int WIDTH;
        public int HEIGHT;
        public int x;
        public int y;
        public int speed = 0;
        public int time_from_last_jump = 0;
        private int last_x;
        private int last_y;
        protected int X_CHANGE_PER_TICK;

        //when player reaches this height, he stops and everything around moves
        private int MAX_HEIGHT_OF_PLAYER = 400;

        public int score = 0;
        public bool dead = false;

        public Bitmap img;
        protected int scale_img;
        
        private Direction direction_of_move = Direction.Up;
        private enum Direction
        {
            Down = 1,
            Up = -1
        }

        public Player(string img_name, int _window_width, int _window_height, int _scale_img, int _x, int _y)
        {
            img = new Bitmap(img_name);
            scale_img = _scale_img;
            WINDOW_WIDTH = _window_width;
            WINDOW_HEIGHT = _window_height;
            WIDTH = img.Width/scale_img;
            HEIGHT = img.Height/scale_img;
            X_CHANGE_PER_TICK = 20;
            x = _x;
            y = _y;
            last_x = _x;
            last_y = _y;
        }

        public void draw(PaintEventArgs e)
        {
            e.Graphics.DrawImage(img, x, y, WIDTH, HEIGHT);
        }

        public int X { 
            get
            {
                return x; 
            }
            set {
                if (value + WIDTH <= WINDOW_WIDTH && value >= 0)
                {
                    x = value;
                }
                else if (value + WIDTH > WINDOW_WIDTH)
                    x = WINDOW_WIDTH - WIDTH;
                else if (value < 0)
                    x = 0;
            }
        }
        public int Y { get => y; set => y = value; }
        public int Speed { get => speed;
            set
            {
                speed = value;
            }
        }

        public void move(bool left_pressed, bool right_pressed, float gravity, List<MoveableObject> all_objects)
        {
            /* moves player, checks for collisions, moves all objects down if needed */
            int collided = 0;
            last_x = x;
            last_y = y;
            if (left_pressed) this.X -= X_CHANGE_PER_TICK;
            if (right_pressed) this.X += X_CHANGE_PER_TICK;

            if(y + HEIGHT > WINDOW_HEIGHT)
            {
                dead = true;
                return;
            }

            this.Speed = (int)(gravity * time_from_last_jump * time_from_last_jump);

            //speed == 0 means we ar at the top of the jump trajectory
            if (this.Speed <= 0)
            {
                direction_of_move = Direction.Down;
            }
            if (this.y >= MAX_HEIGHT_OF_PLAYER || direction_of_move == Direction.Down)
            {
                if(direction_of_move == Direction.Up)
                    this.Y -= this.Speed;
                else
                    this.Y += this.Speed;
            }

            collided = collision_with_monster(all_objects);
            if (collided < 0)
            {
                dead = true;
                return;
            }

            if (direction_of_move == Direction.Down)
            {
                collided = collision_with_block(all_objects);
            }
            else
            {
                collided = 0;
            }

            // moving all objects down
            if(this.y < MAX_HEIGHT_OF_PLAYER && direction_of_move == Direction.Up)
            {
                score += this.Speed;
                foreach (MoveableObject m in all_objects)
                {
                    if(m.GetType() != typeof(Player))
                    {
                        //all move down (always - look at the first condition)
                        m.y += this.Speed;

                    }                      
                }
            }
            
            if (collided > 0)
            {
                direction_of_move = Direction.Up;
                time_from_last_jump = collided;
            }
            if(direction_of_move == Direction.Down)
                time_from_last_jump++;
            if (direction_of_move == Direction.Up)
                time_from_last_jump--;
        }

        private int collision_with_monster(List<MoveableObject> all_objects)
        {
            foreach (MoveableObject m in all_objects)
            {
                if (m.GetType() == typeof(Monster))
                {
                    if (player_overlaps_with_monster(this.x, this.y, m))
                    {
                        return m.energy_on_collision();
                    }
                }
            }
            return 0;
        }
        private int collision_with_block(List<MoveableObject> all_objects)
        {
            //returns 0 when no collision
            //returns energy of collision otherwise

            foreach (MoveableObject m in all_objects)
            {
                if (m.GetType() == typeof(Block))
                {
                    //last time it didn't collide, now it does AND player was above the platform = COLLISION
                    if(!player_overlaps_with_moveable_object(last_x, last_y, m) && 
                        player_overlaps_with_moveable_object(this.x, this.y, m) &&
                        last_y + this.HEIGHT < m.y)
                    {
                        return m.energy_on_collision();
                    }
                }
                
            }
            return 0;
        }

        private bool player_overlaps_with_moveable_object(int player_x, int player_y, MoveableObject m)
        {
            //one rectangle is on the right one on the left - they don't overlap
            if (player_x + this.WIDTH < m.x || m.x + m.WIDTH < player_x)
            {
                return false;
            }

            //one rectangle is above the other one
            if (player_y + this.HEIGHT < m.y || m.y + m.HEIGHT < player_y)
            {
                return false;
            }
                
            return true;
        }

        private bool player_overlaps_with_monster(int player_x, int player_y, MoveableObject m)
        {
            //one rectangle is on the right one on the left - they don't overlap
            if (player_x + this.WIDTH < m.x || m.x + m.WIDTH < player_x)
            {
                return false;
            }

            //one rectangle is above the other one
            if (player_y + this.HEIGHT < m.y || m.y + m.HEIGHT < player_y)
            {
                return false;
            }


            return true;
        }
    }
}
