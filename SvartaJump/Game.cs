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
        private int WINDOW_HEIGHT;
        private int WINDOW_WIDTH;
        private int SCALE_PLAYER = 3;
        private int SCALE_BLOCK = 7;
        private int SCALE_MONSTER = 1;

        private float GRAVITY = 0.05F;
        public int score = 0;
        private List<MoveableObject> moveable_objects = new List<MoveableObject>();
        private Player svarta;
        public GameSate state = GameSate.NotStarted;
        private GameMode game_mode = GameMode.Normal;


        private Bitmap background_normal_mode;
        private Bitmap background_fast_mode;
        private Bitmap background_little_mode;
        private string svarta_img_name = "Graphics/svarta.png";
        private string block_img_name = "Graphics/block.png";
        private string monster_img_name = "Graphics/monster1.png";
        private string trampoline_img_name = "Graphics/trampoline.png";
        private string small_trampolinea_img_name = "Graphics/small_trampoline.png";
        private string small_block_img_name = "Graphics/small_block.png";


        // PROBABILITY: X/1000
        private int TRAMPOLINE_PROBABILITY = 30;
        private int SMALL_TRAMPOLINE_PROBABILITY = 130;
        private int SMALL_BLOCK_PROBABILITY = 180;
        private int MONSTER_PROBABILITY = 50;

        //how many blocks are in normal mode
        private int BLOCKS_IN_NORMAL_MODE_CONST = 25;
        private int BLOCKS_IN_DIFFERENT_MODE_CONST = 10;
        private int blocks_remaining_until_mode_change;
        
        Random r;

        public bool warning = false;
        private int warning_started = 0;
        private int WARNING_TIME = 300; //how long will warning be displayed
        
        public enum GameSate
        {
            NotStarted,
            Started,
            End
        }

        private int NUMBER_OF_DIFFERENT_MODES = 2;
        public enum GameMode
        {
            Normal,
            Little_blocks,
            Fast_blocks
        }

        public Game(int window_height, int window_width)
        {
            WINDOW_HEIGHT = window_height;
            WINDOW_WIDTH = window_width;

            background_normal_mode = new Bitmap("Graphics/background_normal.png");
            background_little_mode = new Bitmap("Graphics/background_little.png");
            background_fast_mode = new Bitmap("Graphics/background_fast.png");

            state = GameSate.NotStarted;
            init_objects();

            blocks_remaining_until_mode_change = BLOCKS_IN_NORMAL_MODE_CONST;
            r = new Random();
        }

        private void init_objects()
        {
            /* inits starting objects visible on screen */
            svarta = new Player(svarta_img_name, 600, 900, SCALE_PLAYER, 270, 400);
            moveable_objects.Add(new Block(block_img_name, 600, 900, SCALE_BLOCK, 0, 100, 100, 30));
            moveable_objects.Add(new Block(block_img_name, 600, 900, SCALE_BLOCK, 0, 100, 350, 30));
            moveable_objects.Add(new Block(block_img_name, 600, 900, SCALE_BLOCK, 0, 250, 600, 30));
        }

        public void draw(PaintEventArgs e)
        {
            /* draws background, svarta and all moveable objects
             * determines which background to choose based on current game_mode */

            //if there is some block from different mode - we have to keep background of the game_mode
            bool different_game_mode = false;
            foreach (MoveableObject m in moveable_objects)
            {
                // if block in different mode and is visible
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
            /* deletes moveable_objects that are no longer visible
             * returns true if all moveable objects are displayed
             * (no moveable objects have y coordinate < 0) */

            bool all_blocks_displayed = true;
            List<int> to_be_deleted = new List<int>();

            //finds indexes to delete
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

            // safe removing (from end)
            for (int i = to_be_deleted.Count - 1; i >= 0; i--)
            {
                moveable_objects.RemoveAt(to_be_deleted[i]);
            }

            return all_blocks_displayed;
        }

        private void generate_new_block()
        {
            /* generates new block based on game_mode and takes into account probability
             * of different types of blocks*/

            switch (game_mode)
            {
                case GameMode.Normal:
                    int different_block = r.Next(0, 1000); // negative so that it is invisible (above screen)
                    if (different_block <= TRAMPOLINE_PROBABILITY)
                    {
                        moveable_objects.Add(new Block(trampoline_img_name, 600, 900, SCALE_BLOCK, score, 60, game_mode));
                    }
                    else if (different_block <= SMALL_TRAMPOLINE_PROBABILITY)
                    {
                        moveable_objects.Add(new Block(small_trampolinea_img_name, 600, 900, SCALE_BLOCK, score, 37, game_mode));
                    }
                    else if (different_block <= SMALL_BLOCK_PROBABILITY)
                    {
                        moveable_objects.Add(new Block(small_block_img_name, 600, 900, SCALE_BLOCK * 2, score, 30, game_mode));
                    }
                    else
                    {
                        moveable_objects.Add(new Block(block_img_name, 600, 900, SCALE_BLOCK, score, 30, game_mode));
                    }
                    break;

                case GameMode.Fast_blocks:
                    moveable_objects.Add(new Block(block_img_name, 600, 900, SCALE_BLOCK, score, 30, game_mode));
                    break;

                case GameMode.Little_blocks:
                    moveable_objects.Add(new Block(block_img_name, 600, 900, SCALE_BLOCK, score, 30, game_mode));
                    break;
            }
        }

        public void generate_monster()
        {
            /* generates monster if svarta doesn't move too quickly (it would be
             * impossible for the player to dodge the monster */

            if (svarta.time_from_last_jump <= 30) // we don't want to fly too fast (impossible for player)
            {
                int monster = r.Next(1000);
                if(monster <= MONSTER_PROBABILITY)
                {
                    warning = true;
                    warning_started = score;
                    moveable_objects.Add(new Block(block_img_name, 600, 900, SCALE_BLOCK, 0, 100, -100, 30));
                    moveable_objects.Add(new Block(block_img_name, 600, 900, SCALE_BLOCK, 0, 400, -100, 30));
                    moveable_objects.Add(new Block(block_img_name, 600, 900, SCALE_BLOCK, 0, 250, -100, 30));
                    moveable_objects.Add(new Monster(monster_img_name, 600, 900, SCALE_MONSTER, score));
                }
            }
        }

        private void change_mode()
        {
            /* changes mode of the game */

            if (game_mode == GameMode.Normal)
            {
                warning = true;
                warning_started = score;
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
                blocks_remaining_until_mode_change = BLOCKS_IN_DIFFERENT_MODE_CONST;
            }
            else
            {
                game_mode = GameMode.Normal;
                blocks_remaining_until_mode_change = BLOCKS_IN_NORMAL_MODE_CONST;
            }
        }

        public void move(bool right_pressed, bool left_pressed)
        {
            /* moves with svarta and all moveable objects, detects svarta's death
             * and if needed generates new blocks, monsters and changes modes*/

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

                blocks_remaining_until_mode_change--;

                if (blocks_remaining_until_mode_change == 0)
                {
                    change_mode();
                }
            }

            score = svarta.score;
            
            if(score - warning_started > WARNING_TIME)
            {
                warning = false;
            }
            
        }

    }
}
