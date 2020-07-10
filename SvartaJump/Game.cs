using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SvartaJump
{
    class Game
    {
        private float GRAVITY = 0.05F;
        private int WINDOW_HEIGHT;
        private int WINDOW_WIDTH;
        private int SCALE_PLAYER = 3;
        private int SCALE_BLOCK = 7;
        private int SCALE_MONSTER = 1;
        public int score = 0;
        private List<MoveableObject> moveable_objects = new List<MoveableObject>();
        private Player svarta;
        private Bitmap background_normal_mode;
        private Bitmap background_fast_mode;
        private Bitmap background_little_mode;

        private int TRAMPOLINE_PROBABILITY = 30;
        private int SMALL_TRAMPOLINE_PROBABILITY = 130;
        private int SMALL_BLOCK_PROBABILITY = 180;
        private int FAST_BLOCK_PROBABILITY = 230;
        private int FALL_THROUGH_BLOCK_PROBABILITY = 280;
        private int BLOCKS_IN_NORMAL_MODE_CONST = 20;
        private int BLOCKS_IN_DIFFERENT_MODE_CONST = 10;
        private int blocks_until_mode_change;
        private int NUMBER_OF_DIFFERENT_MODES = 2;
        private int MONSTER_PROBABILITY = 100;
        private GameMode game_mode = GameMode.Normal;
        Random r;

        public enum GameSate
        {
            NotStarted,
            Started,
            Paused,
            End
        }

        public enum GameMode
        {
            Normal,
            Little_blocks,
            Fast_blocks
        }

        public GameSate state = GameSate.NotStarted;

        public Game(int window_height, int window_width)
        {
            WINDOW_HEIGHT = window_height;
            WINDOW_WIDTH = window_width;
            state = GameSate.NotStarted;
            background_normal_mode = new Bitmap("background_normal.png");
            background_little_mode = new Bitmap("background_little.png");

            background_fast_mode = new Bitmap("background_fast.png");

            svarta = new Player("svarta.png", 600, 900, SCALE_PLAYER, 270, 400);
            moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 100, 100, 30));
            moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 100, 350, 30));
            moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 250, 600, 30));
            //moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 300, 600, 30));
            //moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 150, 600, 30));
            //moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 0, 600, 30));
            r = new Random();
            blocks_until_mode_change = BLOCKS_IN_NORMAL_MODE_CONST;
        }

        public void draw(PaintEventArgs e)
        {
            
            //if there is some block from different mode
            bool different_game_mode = false;
            foreach (MoveableObject m in moveable_objects)
            {
                if (m.block_mode != GameMode.Normal && m.y >= 0)
                {
                    different_game_mode = true;
                    switch (m.block_mode)
                    {
                        case GameMode.Little_blocks:
                            e.Graphics.DrawImage(background_little_mode, 0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
                            break;
                        case GameMode.Fast_blocks:
                            e.Graphics.DrawImage(background_fast_mode, 0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
                            break;
                    }
                }
            }
            if(different_game_mode == false)
            {
                e.Graphics.DrawImage(background_normal_mode, 0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

            }
            svarta.draw(e);
            foreach (MoveableObject m in moveable_objects)
            {
                m.draw(e);
            }
        }

        private bool delete_old_moveable_objects()
        {
            bool all_blocks_displayed = true;

            List<int> to_be_deleted = new List<int>();
            for (int i = 0; i < moveable_objects.Count; i++)
            {
                if (moveable_objects[i].y + moveable_objects[i].HEIGHT < 0)
                {
                    all_blocks_displayed = false;
                }

                if (moveable_objects[i].y > WINDOW_HEIGHT)
                {
                    to_be_deleted.Add(i);
                }

            }
            for (int i = to_be_deleted.Count - 1; i >= 0; i--)
            {
                moveable_objects.RemoveAt(to_be_deleted[i]);
            }

            return all_blocks_displayed;
        }

        private void generate_new_block()
        {
            switch (game_mode)
            {
                case GameMode.Normal:
                    int different_block = r.Next(0, 1000); // negative so that it is invisible (above screen)
                    if (different_block <= TRAMPOLINE_PROBABILITY)
                    {
                        moveable_objects.Add(new Block("trampoline.png", 600, 900, SCALE_BLOCK, score, 60, game_mode));

                    }
                    else if (different_block <= SMALL_TRAMPOLINE_PROBABILITY)
                    {
                        moveable_objects.Add(new Block("small_trampoline.png", 600, 900, SCALE_BLOCK, score, 37, game_mode));

                    }
                    else if (different_block <= SMALL_BLOCK_PROBABILITY)
                    {
                        moveable_objects.Add(new Block("small_block.png", 600, 900, SCALE_BLOCK * 2, score, 30, game_mode));

                    }
                    else
                    {
                        moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, score, 30, game_mode));

                    }
                    break;
                case GameMode.Fast_blocks:
                    moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, score, 30, game_mode));

                    break;
                case GameMode.Little_blocks:
                    moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, score, 30, game_mode));
                    break;
            }
        }

        public void generate_monster()
        {
            if (svarta.time_from_last_jump <= 30) // we don't want to fly too fast (impossible for player)
            {
                int monster = r.Next(1000);
                if(monster <= MONSTER_PROBABILITY)
                {
                    moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 100, -100, 30));
                    moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 400, -100, 30));
                    moveable_objects.Add(new Block("block.png", 600, 900, SCALE_BLOCK, 0, 250, -100, 30));
                    moveable_objects.Add(new Monster("monster1.png", 600, 900, SCALE_MONSTER, score));
                }
            }
        }

        private void change_mode()
        {
            if (game_mode == GameMode.Normal)
            {
                int new_mode = r.Next(NUMBER_OF_DIFFERENT_MODES);
                switch (new_mode)
                {
                    case 0:
                        game_mode = GameMode.Little_blocks;
                        break;
                    case 1:
                        game_mode = GameMode.Fast_blocks;
                        break;
                }
                blocks_until_mode_change = BLOCKS_IN_DIFFERENT_MODE_CONST;
            }
            else
            {
                game_mode = GameMode.Normal;
                blocks_until_mode_change = BLOCKS_IN_NORMAL_MODE_CONST;
            }
        }

        public void move(bool right_pressed, bool left_pressed)
        {
            

            svarta.move(left_pressed, right_pressed, GRAVITY, moveable_objects);
            foreach (MoveableObject m in moveable_objects)
            {
                m.move(left_pressed, right_pressed, GRAVITY, moveable_objects);
            }

            if (svarta.dead == true)
            {
                HighscoreHandler.add_high_score(score);
                state = GameSate.End;
                return;
            }

            bool all_blocks_displayed = delete_old_moveable_objects();

            if (all_blocks_displayed)
            {
                generate_new_block();

                generate_monster();   

                blocks_until_mode_change--;

                if (blocks_until_mode_change == 0)
                {
                    change_mode();
                }
            }

            score = svarta.score;
            
            
        }

    }
}
