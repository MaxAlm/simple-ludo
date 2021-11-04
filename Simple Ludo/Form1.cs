using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Ludo
{
    public partial class Form1 : Form
    {
        /////////////
        // METHODS //
        /////////////

        private void NewGame()
        {
            // Move all back to spawn
            spawn_b1.Visible = true;
            spawn_b2.Visible = true;
            spawn_b3.Visible = true;
            spawn_b4.Visible = true;

            spawn_r1.Visible = true;
            spawn_r2.Visible = true;
            spawn_r3.Visible = true;
            spawn_r4.Visible = true;

            spawn_g1.Visible = true;
            spawn_g2.Visible = true;
            spawn_g3.Visible = true;
            spawn_g4.Visible = true;

            spawn_y1.Visible = true;
            spawn_y2.Visible = true;
            spawn_y3.Visible = true;
            spawn_y4.Visible = true;

            // Reset game board
            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i].Name.StartsWith("tile_"))
                {
                    tiles[i].BackColor = Color.White;
                }
            }

            // Set up dice
            dice_anim = 0;
            dice_timer.Interval = 20;
            dice_label.Text = "--";

            // Set up bot
            bot_anim = 0;
            bot_timer.Interval = 1500;

            // Set up move timer
            move_anim = 0;
            move_timer.Interval = 1;

            turn = rnd.Next(0, 4);

            ChangePlayer();

            if (turn == human)
            {
                button_dice.Visible = true;
                MessageBox.Show("You go first!", "Simple Ludo");
            }
            else
            {
                button_dice.Visible = false;
                bot_timer.Start();
            }
        }
        
        private void ChangePlayer()
        {
            // Change turn
            if (turn == (int)COLOR.YELLOW) { turn = (int)COLOR.BLUE; }
            else { turn++; }

            // Set color
            switch (turn)
            {
                case (int)COLOR.BLUE:
                    color = set_color[turn];
                    player_label.Text = "BLUE";
                    break;

                case (int)COLOR.RED:
                    color = set_color[turn];
                    player_label.Text = "RED";
                    break;

                case (int)COLOR.GREEN:
                    color = set_color[turn];
                    player_label.Text = "GREEN";
                    break;

                case (int)COLOR.YELLOW:
                    color = set_color[turn];
                    player_label.Text = "YELLOW";
                    break;
            }

            // Reset dice text
            dice_label.Text = "--";

            // Check if player or bot turn
            if (turn != human)
            {
                button_dice.Visible = false;
                
                if (human != -1)
                {
                    bot_timer.Start();
                }
            }
            else
            {
                button_dice.Visible = true;
            }
        }

        private void FillLists()
        {
            int tile_num;

            // Fill blue_tiles list
            tile_num = 1;

            for (int i = 0; i < 48; i++)
            {
                blue_tiles.Add("" + tile_num);
                tile_num++;
            }

            blue_tiles.Add("b1");
            blue_tiles.Add("b2");
            blue_tiles.Add("b3");
            blue_tiles.Add("b4");
            blue_tiles.Add("b5");
            blue_tiles.Add("goal");

            // Fill red_tiles list
            tile_num = 13;

            for (int i = 0; i < 48; i++)
            {
                red_tiles.Add("" + tile_num);
                
                if (tile_num == 48)
                {
                    tile_num = 1;
                }
                else
                {
                    tile_num++;
                }
            }

            red_tiles.Add("r1");
            red_tiles.Add("r2");
            red_tiles.Add("r3");
            red_tiles.Add("r4");
            red_tiles.Add("r5");
            red_tiles.Add("goal");

            // Fill green_tiles list
            tile_num = 25;

            for (int i = 0; i < 48; i++)
            {
                green_tiles.Add("" + tile_num);

                if (tile_num == 48)
                {
                    tile_num = 1;
                }
                else
                {
                    tile_num++;
                }
            }

            green_tiles.Add("g1");
            green_tiles.Add("g2");
            green_tiles.Add("g3");
            green_tiles.Add("g4");
            green_tiles.Add("g5");
            green_tiles.Add("goal");

            // Fill yellow_tiles list
            tile_num = 37;

            for (int i = 0; i < 48; i++)
            {
                yellow_tiles.Add("" + tile_num);

                if (tile_num == 48)
                {
                    tile_num = 1;
                }
                else
                {
                    tile_num++;
                }
            }

            yellow_tiles.Add("y1");
            yellow_tiles.Add("y2");
            yellow_tiles.Add("y3");
            yellow_tiles.Add("y4");
            yellow_tiles.Add("y5");
            yellow_tiles.Add("goal");
        }

        private void UpdateGameBoard()
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].BorderStyle = BorderStyle.FixedSingle;
                tiles[i].Image = null;
            }
        }

        private bool CheckForMove()
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i].BackColor == color)
                {
                    if (tiles[i].Name.StartsWith("tile_"))
                    {
                        int temp_pos = 0;
                        
                        // Get tile position
                        switch (turn)
                        {
                            case (int)COLOR.BLUE:
                                for (int j = 0; j < blue_tiles.Count; j++)
                                {
                                    if (blue_tiles[j] == (string)tiles[i].Tag)
                                    {
                                        temp_pos = j;
                                        break;
                                    }
                                }
                                break;

                            case (int)COLOR.RED:
                                for (int j = 0; j < red_tiles.Count; j++)
                                {
                                    if (red_tiles[j] == (string)tiles[i].Tag)
                                    {
                                        temp_pos = j;
                                        break;
                                    }
                                }
                                break;

                            case (int)COLOR.GREEN:
                                for (int j = 0; j < green_tiles.Count; j++)
                                {
                                    if (green_tiles[j] == (string)tiles[i].Tag)
                                    {
                                        temp_pos = j;
                                        break;
                                    }
                                }
                                break;

                            case (int)COLOR.YELLOW:
                                for (int j = 0; j < yellow_tiles.Count; j++)
                                {
                                    if (yellow_tiles[j] == (string)tiles[i].Tag)
                                    {
                                        temp_pos = j;
                                        break;
                                    }
                                }
                                break;
                        }

                        // Check if it can move to dice position
                        for (int p = 0; p < tiles.Count; p++)
                        {
                            try
                            {
                                switch (turn)
                                {
                                    case (int)COLOR.BLUE:
                                        if ((string)tiles[p].Tag == blue_tiles[temp_pos + dice])
                                        {
                                            if (tiles[p].BackColor != color)
                                            {
                                                return true;
                                            }
                                        }
                                        break;

                                    case (int)COLOR.RED:
                                        if ((string)tiles[p].Tag == red_tiles[temp_pos + dice])
                                        {
                                            if (tiles[p].BackColor != color)
                                            {
                                                return true;
                                            }
                                        }
                                        break;

                                    case (int)COLOR.GREEN:
                                        if ((string)tiles[p].Tag == green_tiles[temp_pos + dice])
                                        {
                                            if (tiles[p].BackColor != color)
                                            {
                                                return true;
                                            }
                                        }
                                        break;

                                    case (int)COLOR.YELLOW:
                                        if ((string)tiles[p].Tag == yellow_tiles[temp_pos + dice])
                                        {
                                            if (tiles[p].BackColor != color)
                                            {
                                                return true;
                                            }
                                        }
                                        break;
                                }
                            } catch { }
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckForWinner()
        {
            bool check = true;
            
            switch (turn)
            {
                case (int)COLOR.BLUE:
                    if (!spawn_b1.Visible && !spawn_b2.Visible && !spawn_b3.Visible && !spawn_b4.Visible)
                    {
                        for (int i = 0; i < tiles.Count; i++)
                        {
                            if (tiles[i].Name.StartsWith("tile_"))
                            {
                                if (tiles[i].BackColor == color)
                                {
                                    check = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        check = false;
                    }
                    break;

                case (int)COLOR.RED:
                    if (!spawn_r1.Visible && !spawn_r2.Visible && !spawn_r3.Visible && !spawn_r4.Visible)
                    {
                        for (int i = 0; i < tiles.Count; i++)
                        {
                            if (tiles[i].Name.StartsWith("tile_"))
                            {
                                if (tiles[i].BackColor == color)
                                {
                                    check = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        check = false;
                    }
                    break;

                case (int)COLOR.GREEN:
                    if (!spawn_g1.Visible && !spawn_g2.Visible && !spawn_g3.Visible && !spawn_g4.Visible)
                    {
                        for (int i = 0; i < tiles.Count; i++)
                        {
                            if (tiles[i].Name.StartsWith("tile_"))
                            {
                                if (tiles[i].BackColor == color)
                                {
                                    check = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        check = false;
                    }
                    break;

                case (int)COLOR.YELLOW:
                    if (!spawn_y1.Visible && !spawn_y2.Visible && !spawn_y3.Visible && !spawn_y4.Visible)
                    {
                        for (int i = 0; i < tiles.Count; i++)
                        {
                            if (tiles[i].Name.StartsWith("tile_"))
                            {
                                if (tiles[i].BackColor == color)
                                {
                                    check = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        check = false;
                    }
                    break;
            }

            if (check)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void KickTile(PictureBox pb)
        {
            // If blue
            if (pb.BackColor == Color.FromArgb(255, 0, 162, 232))
            {
                if (!spawn_b1.Visible) { spawn_b1.Visible = true; }
                else if (!spawn_b2.Visible) { spawn_b2.Visible = true; }
                else if (!spawn_b3.Visible) { spawn_b3.Visible = true; }
                else if (!spawn_b4.Visible) { spawn_b4.Visible = true; }
            }
            // If red
            else if (pb.BackColor == Color.FromArgb(255, 225, 0, 34))
            {
                if (!spawn_r1.Visible) { spawn_r1.Visible = true; }
                else if (!spawn_r2.Visible) { spawn_r2.Visible = true; }
                else if (!spawn_r3.Visible) { spawn_r3.Visible = true; }
                else if (!spawn_r4.Visible) { spawn_r4.Visible = true; }
            }
            // If red
            else if (pb.BackColor == Color.FromArgb(255, 34, 177, 76))
            {
                if (!spawn_g1.Visible) { spawn_g1.Visible = true; }
                else if (!spawn_g2.Visible) { spawn_g2.Visible = true; }
                else if (!spawn_g3.Visible) { spawn_g3.Visible = true; }
                else if (!spawn_g4.Visible) { spawn_g4.Visible = true; }
            }
            // If red
            else if (pb.BackColor == Color.FromArgb(255, 255, 201, 14))
            {
                if (!spawn_y1.Visible) { spawn_y1.Visible = true; }
                else if (!spawn_y2.Visible) { spawn_y2.Visible = true; }
                else if (!spawn_y3.Visible) { spawn_y3.Visible = true; }
                else if (!spawn_y4.Visible) { spawn_y4.Visible = true; }
            }
        }

        private void BOT_PlayFormSpawn()
        {
            if (dice == 1 || dice == 6)
            {
                // Set current_tile_1 & current_tile_2
                switch (turn)
                {
                    case (int)COLOR.BLUE:
                        if (tile_1.BackColor != color)
                        {
                            if (spawn_b1.Visible) { current_tile_1 = 8; }
                            else if (spawn_b2.Visible) { current_tile_1 = 10; }
                            else if (spawn_b3.Visible) { current_tile_1 = 9; }
                            else if (spawn_b4.Visible) { current_tile_1 = 11; }

                            current_tile_2 = 82;

                            // Set up anim_pb
                            anim_pb.Left = tiles[current_tile_1].Left;
                            anim_pb.Top = tiles[current_tile_1].Top;
                            anim_pb.Width = tiles[current_tile_1].Width;
                            anim_pb.Height = tiles[current_tile_1].Height;

                            anim_pb.BackColor = color;
                            anim_pb.Visible = true;
                            anim_pb.BringToFront();

                            if (tiles[current_tile_1].Name.StartsWith("spawn_"))
                            {
                                tiles[current_tile_1].Visible = false;
                            }
                            else
                            {
                                tiles[current_tile_1].BackColor = Color.White;
                            }

                            move_x = (tiles[current_tile_1].Left - tiles[current_tile_2].Left) / 30;
                            move_y = (tiles[current_tile_1].Top - tiles[current_tile_2].Top) / 30;
                            move_size = (tiles[current_tile_1].Width - tiles[current_tile_2].Width) / 30;
                            move_timer.Start();
                        }
                        else
                        {
                            ChangePlayer();
                        }
                        break;

                    case (int)COLOR.RED:
                        if (tile_13.BackColor != color)
                        {
                            if (spawn_r1.Visible) { current_tile_1 = 2; }
                            else if (spawn_r2.Visible) { current_tile_1 = 3; }
                            else if (spawn_r3.Visible) { current_tile_1 = 0; }
                            else if (spawn_r4.Visible) { current_tile_1 = 1; }

                            current_tile_2 = 43;

                            // Set up anim_pb
                            anim_pb.Left = tiles[current_tile_1].Left;
                            anim_pb.Top = tiles[current_tile_1].Top;
                            anim_pb.Width = tiles[current_tile_1].Width;
                            anim_pb.Height = tiles[current_tile_1].Height;

                            anim_pb.BackColor = color;
                            anim_pb.Visible = true;
                            anim_pb.BringToFront();

                            if (tiles[current_tile_1].Name.StartsWith("spawn_"))
                            {
                                tiles[current_tile_1].Visible = false;
                            }
                            else
                            {
                                tiles[current_tile_1].BackColor = Color.White;
                            }

                            move_x = (tiles[current_tile_1].Left - tiles[current_tile_2].Left) / 30;
                            move_y = (tiles[current_tile_1].Top - tiles[current_tile_2].Top) / 30;
                            move_size = (tiles[current_tile_1].Width - tiles[current_tile_2].Width) / 30;
                            move_timer.Start();
                        }
                        else
                        {
                            ChangePlayer();
                        }
                        break;

                    case (int)COLOR.GREEN:
                        if (tile_25.BackColor != color)
                        {
                            if (spawn_g1.Visible) { current_tile_1 = 7; }
                            else if (spawn_g2.Visible) { current_tile_1 = 5; }
                            else if (spawn_g3.Visible) { current_tile_1 = 6; }
                            else if (spawn_g4.Visible) { current_tile_1 = 4; }

                            current_tile_2 = 17;

                            // Set up anim_pb
                            anim_pb.Left = tiles[current_tile_1].Left;
                            anim_pb.Top = tiles[current_tile_1].Top;
                            anim_pb.Width = tiles[current_tile_1].Width;
                            anim_pb.Height = tiles[current_tile_1].Height;

                            anim_pb.BackColor = color;
                            anim_pb.Visible = true;
                            anim_pb.BringToFront();

                            if (tiles[current_tile_1].Name.StartsWith("spawn_"))
                            {
                                tiles[current_tile_1].Visible = false;
                            }
                            else
                            {
                                tiles[current_tile_1].BackColor = Color.White;
                            }

                            move_x = (tiles[current_tile_1].Left - tiles[current_tile_2].Left) / 30;
                            move_y = (tiles[current_tile_1].Top - tiles[current_tile_2].Top) / 30;
                            move_size = (tiles[current_tile_1].Width - tiles[current_tile_2].Width) / 30;
                            move_timer.Start();
                        }
                        else
                        {
                            ChangePlayer();
                        }
                        break;

                    case (int)COLOR.YELLOW:
                        if (tile_37.BackColor != color)
                        {
                            if (spawn_y1.Visible) { current_tile_1 = 13; }
                            else if (spawn_y2.Visible) { current_tile_1 = 12; }
                            else if (spawn_y3.Visible) { current_tile_1 = 15; }
                            else if (spawn_y4.Visible) { current_tile_1 = 14; }

                            current_tile_2 = 48;

                            // Set up anim_pb
                            anim_pb.Left = tiles[current_tile_1].Left;
                            anim_pb.Top = tiles[current_tile_1].Top;
                            anim_pb.Width = tiles[current_tile_1].Width;
                            anim_pb.Height = tiles[current_tile_1].Height;

                            anim_pb.BackColor = color;
                            anim_pb.Visible = true;
                            anim_pb.BringToFront();

                            if (tiles[current_tile_1].Name.StartsWith("spawn_"))
                            {
                                tiles[current_tile_1].Visible = false;
                            }
                            else
                            {
                                tiles[current_tile_1].BackColor = Color.White;
                            }

                            move_x = (tiles[current_tile_1].Left - tiles[current_tile_2].Left) / 30;
                            move_y = (tiles[current_tile_1].Top - tiles[current_tile_2].Top) / 30;
                            move_size = (tiles[current_tile_1].Width - tiles[current_tile_2].Width) / 30;
                            move_timer.Start();
                        }
                        else
                        {
                            ChangePlayer();
                        }
                        break;
                }
            }
            else
            {
                ChangePlayer();
            }
        }

        private bool BOT_CheckSpawn()
        {
            bool check = false;
            
            switch (turn)
            {
                case (int)COLOR.BLUE:
                    if (spawn_b1.Visible && spawn_b2.Visible && spawn_b3.Visible && spawn_b4.Visible)
                    {
                        check = true;
                    }
                    break;

                case (int)COLOR.RED:
                    if (spawn_r1.Visible && spawn_r2.Visible && spawn_r3.Visible && spawn_r4.Visible)
                    {
                        check = true;
                    }
                    break;

                case (int)COLOR.GREEN:
                    if (spawn_g1.Visible && spawn_g2.Visible && spawn_g3.Visible && spawn_g4.Visible)
                    {
                        check = true;
                    }
                    break;

                case (int)COLOR.YELLOW:
                    if (spawn_y1.Visible && spawn_y2.Visible && spawn_y3.Visible && spawn_y4.Visible)
                    {
                        check = true;
                    }
                    break;
            }

            if (check)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void BOT_MoveTile()
        {
            int temp_2;
            int temp_pos;
            
            for (int loop_count = 0; loop_count < 4; loop_count++)
            {
                for (int i = 0; i < tiles.Count; i++)
                {
                    if (tiles[i].BackColor == color)
                    {
                        if (tiles[i].Name.StartsWith("tile_"))
                        {
                            current_tile_1 = -1;
                            current_tile_2 = -1;
                            current_position = -1;

                            temp_2 = -1;
                            temp_pos = -1;

                            // Get temp_pos & temp_2
                            switch (turn)
                            {
                                case (int)COLOR.BLUE:
                                    // Get temp_pos
                                    for (int p = 0; p < blue_tiles.Count; p++)
                                    {
                                        if (blue_tiles[p] == (string)tiles[i].Tag)
                                        {
                                            temp_pos = p;
                                        }
                                    }

                                    // Get temp_2
                                    for (int k = 0; k < tiles.Count; k++)
                                    {
                                        try
                                        {
                                            if ((string)tiles[k].Tag == blue_tiles[temp_pos + dice])
                                            {
                                                temp_2 = k;
                                            }
                                        }
                                        catch { }
                                    }
                                    break;

                                case (int)COLOR.RED:
                                    // Get temp_pos
                                    for (int p = 0; p < red_tiles.Count; p++)
                                    {
                                        if (red_tiles[p] == (string)tiles[i].Tag)
                                        {
                                            temp_pos = p;
                                        }
                                    }

                                    // Get temp_2
                                    for (int k = 0; k < tiles.Count; k++)
                                    {
                                        try
                                        {
                                            if ((string)tiles[k].Tag == red_tiles[temp_pos + dice])
                                            {
                                                temp_2 = k;
                                            }
                                        }
                                        catch { }
                                    }
                                    break;

                                case (int)COLOR.GREEN:
                                    // Get temp_pos
                                    for (int p = 0; p < green_tiles.Count; p++)
                                    {
                                        if (green_tiles[p] == (string)tiles[i].Tag)
                                        {
                                            temp_pos = p;
                                        }
                                    }

                                    // Get temp_2
                                    for (int k = 0; k < tiles.Count; k++)
                                    {
                                        try
                                        {
                                            if ((string)tiles[k].Tag == green_tiles[temp_pos + dice])
                                            {
                                                temp_2 = k;
                                            }
                                        }
                                        catch { }
                                    }
                                    break;

                                case (int)COLOR.YELLOW:
                                    // Get temp_pos
                                    for (int p = 0; p < yellow_tiles.Count; p++)
                                    {
                                        if (yellow_tiles[p] == (string)tiles[i].Tag)
                                        {
                                            temp_pos = p;
                                        }
                                    }

                                    // Get temp_2
                                    for (int k = 0; k < tiles.Count; k++)
                                    {
                                        try
                                        {
                                            if ((string)tiles[k].Tag == yellow_tiles[temp_pos + dice])
                                            {
                                                temp_2 = k;
                                            }
                                        }
                                        catch { }
                                    }
                                    break;
                            }

                            try
                            {
                                // Check if kicking player is possible
                                if (loop_count == 0)
                                {
                                    if (tiles[temp_2].BackColor != color && tiles[temp_2].BackColor != Color.White)
                                    {
                                        current_tile_1 = i;
                                        current_tile_2 = temp_2;
                                        current_position = temp_pos;

                                        // Position and resize anim_pb
                                        anim_pb.Left = tiles[current_tile_1].Left;
                                        anim_pb.Top = tiles[current_tile_1].Top;
                                        anim_pb.Width = tiles[current_tile_1].Width;
                                        anim_pb.Height = tiles[current_tile_1].Height;

                                        anim_pb.BackColor = color;
                                        anim_pb.Visible = true;
                                        anim_pb.BringToFront();

                                        if (tiles[current_tile_1].Name.StartsWith("spawn_"))
                                        {
                                            tiles[current_tile_1].Visible = false;
                                        }
                                        else
                                        {
                                            tiles[current_tile_1].BackColor = Color.White;
                                        }

                                        move_x = (tiles[current_tile_1].Left - tiles[current_tile_2].Left) / 30;
                                        move_y = (tiles[current_tile_1].Top - tiles[current_tile_2].Top) / 30;
                                        move_size = (tiles[current_tile_1].Width - tiles[current_tile_2].Width) / 30;
                                        move_timer.Start();
                                        return;
                                    }
                                }
                                // Check if reaching safe zone is possible
                                else if (loop_count == 1)
                                {
                                    string tile_tag = (string)tiles[temp_2].Tag;
                                    
                                    if (tile_tag.Contains("b") || tile_tag.Contains("r") || tile_tag.Contains("g") || tile_tag.Contains("y"))
                                    {
                                        if (tiles[temp_2].BackColor != color)
                                        {
                                            current_tile_1 = i;
                                            current_tile_2 = temp_2;
                                            current_position = temp_pos;

                                            // Position and resize anim_pb
                                            anim_pb.Left = tiles[current_tile_1].Left;
                                            anim_pb.Top = tiles[current_tile_1].Top;
                                            anim_pb.Width = tiles[current_tile_1].Width;
                                            anim_pb.Height = tiles[current_tile_1].Height;

                                            anim_pb.BackColor = color;
                                            anim_pb.Visible = true;
                                            anim_pb.BringToFront();

                                            if (tiles[current_tile_1].Name.StartsWith("spawn_"))
                                            {
                                                tiles[current_tile_1].Visible = false;
                                            }
                                            else
                                            {
                                                tiles[current_tile_1].BackColor = Color.White;
                                            }

                                            move_x = (tiles[current_tile_1].Left - tiles[current_tile_2].Left) / 30;
                                            move_y = (tiles[current_tile_1].Top - tiles[current_tile_2].Top) / 30;
                                            move_size = (tiles[current_tile_1].Width - tiles[current_tile_2].Width) / 30;
                                            move_timer.Start();
                                            return;
                                        }
                                    }
                                }
                                // Check if reaching goal is possible
                                else if (loop_count == 2)
                                {
                                    if ((string)tiles[temp_2].Tag == "goal")
                                    {
                                        current_tile_1 = i;
                                        current_tile_2 = temp_2;
                                        current_position = temp_pos;

                                        // Position and resize anim_pb
                                        anim_pb.Left = tiles[current_tile_1].Left;
                                        anim_pb.Top = tiles[current_tile_1].Top;
                                        anim_pb.Width = tiles[current_tile_1].Width;
                                        anim_pb.Height = tiles[current_tile_1].Height;

                                        anim_pb.BackColor = color;
                                        anim_pb.Visible = true;
                                        anim_pb.BringToFront();

                                        if (tiles[current_tile_1].Name.StartsWith("spawn_"))
                                        {
                                            tiles[current_tile_1].Visible = false;
                                        }
                                        else
                                        {
                                            tiles[current_tile_1].BackColor = Color.White;
                                        }

                                        move_x = (tiles[current_tile_1].Left - tiles[current_tile_2].Left) / 30;
                                        move_y = (tiles[current_tile_1].Top - tiles[current_tile_2].Top) / 30;
                                        move_size = (tiles[current_tile_1].Width - tiles[current_tile_2].Width) / 30;
                                        move_timer.Start();
                                        return;
                                    }
                                }
                                // Check for any possible move
                                else if (loop_count == 3)
                                {
                                    if (tiles[temp_2].BackColor != color)
                                    {
                                        current_tile_1 = i;
                                        current_tile_2 = temp_2;
                                        current_position = temp_pos;

                                        // Position and resize anim_pb
                                        anim_pb.Left = tiles[current_tile_1].Left;
                                        anim_pb.Top = tiles[current_tile_1].Top;
                                        anim_pb.Width = tiles[current_tile_1].Width;
                                        anim_pb.Height = tiles[current_tile_1].Height;

                                        anim_pb.BackColor = color;
                                        anim_pb.Visible = true;
                                        anim_pb.BringToFront();

                                        if (tiles[current_tile_1].Name.StartsWith("spawn_"))
                                        {
                                            tiles[current_tile_1].Visible = false;
                                        }
                                        else
                                        {
                                            tiles[current_tile_1].BackColor = Color.White;
                                        }

                                        move_x = (tiles[current_tile_1].Left - tiles[current_tile_2].Left) / 30;
                                        move_y = (tiles[current_tile_1].Top - tiles[current_tile_2].Top) / 30;
                                        move_size = (tiles[current_tile_1].Width - tiles[current_tile_2].Width) / 30;
                                        move_timer.Start();
                                        return;
                                    }
                                }
                            } catch { }
                        }
                    }
                }
            }

            if (dice == 6)
            {
                bool check = false; // Turns to true if spawn is empty

                switch (turn)
                {
                    case (int)COLOR.BLUE:
                        if (!spawn_b1.Visible && !spawn_b2.Visible && !spawn_b3.Visible && !spawn_b4.Visible)
                        {
                            check = true;
                        }
                        break;

                    case (int)COLOR.RED:
                        if (!spawn_r1.Visible && !spawn_r2.Visible && !spawn_r3.Visible && !spawn_r4.Visible)
                        {
                            check = true;
                        }
                        break;

                    case (int)COLOR.GREEN:
                        if (!spawn_g1.Visible && !spawn_g2.Visible && !spawn_g3.Visible && !spawn_g4.Visible)
                        {
                            check = true;
                        }
                        break;

                    case (int)COLOR.YELLOW:
                        if (!spawn_y1.Visible && !spawn_y2.Visible && !spawn_y3.Visible && !spawn_y4.Visible)
                        {
                            check = true;
                        }
                        break;
                }

                if (check)
                {
                    bot_timer.Start();
                }
                else
                {
                    BOT_PlayFormSpawn();
                }
            }
            else
            {
                ChangePlayer();
            }
        }

        ///////////////
        // VARIABLES //
        ///////////////

        int human = -1;                 // Keeps track of the human player's color
        int turn;                       // Controls who's turn it is
        Color color;                    // Stores current players color
        Color[] set_color;              // Contains all player colors
        Random rnd = new Random();      // Random number generator

        int current_tile_1 = -1;        // Stores the current tile's list index
        int current_tile_2 = -1;        // Stores the list index of the current tile's accepted move position
        int current_position = -1;      // Stores the current tiles position in the <current color>_tiles list

        List<string> blue_tiles = new List<string>();       // Stores the blue path to victory
        List<string> red_tiles = new List<string>();        // Stores the red path to victory
        List<string> green_tiles = new List<string>();      // Stores the green path to victory
        List<string> yellow_tiles = new List<string>();     // Stores the yellow path to victory
        List<PictureBox> tiles = new List<PictureBox>();    // Stores all tiles

        int dice;                       // Contains dice result
        int dice_anim;                  // Dice animation controller
        Timer dice_timer;               // Dice animation timer

        int bot_anim;                   // Bot sequence controller
        Timer bot_timer;                // Bot sequence timer

        int move_anim;                  // Move sequence controller
        int move_x;                     // Controls how far the tile moves on the x-axis every tick
        int move_y;                     // Controls how far the tile moves on the y-axis every tick
        int move_size;                  // Controls the tile size change with every tick
        Timer move_timer;               // Move animation timer

        Timer change_player_timer;      // Adds a delay before changing player

        enum COLOR { BLUE = 0, RED = 1, GREEN = 2, YELLOW = 3 }

        ////////////////
        // CODE START //
        ////////////////
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set up color select panel
            Panel color_panel_back = new Panel { Name = "color_panel_back", Dock = DockStyle.Fill };
            this.Controls.Add(color_panel_back);

            color_panel_back.BringToFront();
            color_select_panel.Dock = DockStyle.None;
            color_select_panel.Left = this.Width / 2 - color_select_panel.Width / 2;
            color_select_panel.Top = this.Height / 2 - color_select_panel.Height / 2;
            color_select_panel.BringToFront();
            this.Text = "Color Select";

            // Set up set_color
            set_color = new Color[4];
            for (int i = 0; i < set_color.Length; i++)
            {
                switch (i)
                {
                    case (int)COLOR.BLUE:
                        set_color[i] = Color.FromArgb(255, 0, 162, 232);
                        break;

                    case (int)COLOR.RED:
                        set_color[i] = Color.FromArgb(255, 225, 0, 34);
                        break;

                    case (int)COLOR.GREEN:
                        set_color[i] = Color.FromArgb(255, 34, 177, 76);
                        break;

                    case (int)COLOR.YELLOW:
                        set_color[i] = Color.FromArgb(255, 255, 201, 14);
                        break;
                }
            }

            // Set up dice
            dice_anim = 0;
            dice_timer = new Timer();
            dice_timer.Interval = 20;
            dice_timer.Tick += dice_tick;

            // Set up bot
            bot_anim = 0;
            bot_timer = new Timer();
            bot_timer.Interval = 1500;
            bot_timer.Tick += bot_tick;

            // Set up move timer
            move_anim = 0;
            move_timer = new Timer();
            move_timer.Interval = 1;
            move_timer.Tick += move_tick;

            // Set up change player delay
            change_player_timer = new Timer();
            change_player_timer.Interval = 1000;
            change_player_timer.Tick += change_player_tick;

            // Fill tiles list
            foreach (PictureBox pb in this.Controls.OfType<PictureBox>())
            {
                if (pb.Name.StartsWith("tile_") || pb.Name.StartsWith("spawn_"))
                {
                    tiles.Add(pb);
                }
            }

            // Set up variables
            turn = rnd.Next(0, 4);
            FillLists();
            ChangePlayer();
        }

        // Buttons
        private void roll_dice_Click(object sender, EventArgs e)
        {
            button_dice.Visible = false;
            dice_timer.Start();
        }

        // Game EventHandlers
        private void spawn_click(object sender, EventArgs e)
        {
            var pb = (sender as PictureBox);
            
            if (turn == human)
            {
                if (!button_dice.Visible)
                {
                    if (!change_player_timer.Enabled)
                    {
                        if (!dice_timer.Enabled)
                        {
                            if (!move_timer.Enabled)
                            {
                                if (pb.BackColor == color)
                                {
                                    if (pb.BorderStyle != BorderStyle.Fixed3D)
                                    {
                                        UpdateGameBoard();

                                        current_tile_1 = -1;
                                        current_tile_2 = -1;
                                        current_position = -1;

                                        pb.BorderStyle = BorderStyle.Fixed3D;
                                        pb.Image = Image.FromFile(@"select_frame.png");

                                        // Set current_tile_1 value
                                        for (int i = 0; i < tiles.Count; i++)
                                        {
                                            if ((string)tiles[i].Tag == (string)pb.Tag)
                                            {
                                                current_tile_1 = i;
                                                break;
                                            }
                                        }

                                        // Set current_tile_2 value
                                        if (dice == 1 || dice == 6)
                                        {
                                            switch (turn)
                                            {
                                                case (int)COLOR.BLUE:
                                                    if (tile_1.BackColor != color)
                                                    {
                                                        current_tile_2 = 82;
                                                        tiles[current_tile_2].Image = Image.FromFile(@"whitelist_frame.png");
                                                    }
                                                    break;

                                                case (int)COLOR.RED:
                                                    if (tile_13.BackColor != color)
                                                    {
                                                        current_tile_2 = 43;
                                                        tiles[current_tile_2].Image = Image.FromFile(@"whitelist_frame.png");
                                                    }
                                                    break;

                                                case (int)COLOR.GREEN:
                                                    if (tile_25.BackColor != color)
                                                    {
                                                        current_tile_2 = 17;
                                                        tiles[current_tile_2].Image = Image.FromFile(@"whitelist_frame.png");
                                                    }
                                                    break;

                                                case (int)COLOR.YELLOW:
                                                    if (tile_37.BackColor != color)
                                                    {
                                                        current_tile_2 = 48;
                                                        tiles[current_tile_2].Image = Image.FromFile(@"whitelist_frame.png");
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        UpdateGameBoard();

                                        current_tile_1 = -1;
                                        current_tile_2 = -1;
                                        current_position = -1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void tile_click(object sender, EventArgs e)
        {
            var pb = (sender as PictureBox);

            if (turn == human && !button_dice.Visible && !change_player_timer.Enabled && !dice_timer.Enabled && !move_timer.Enabled)
            {
                if (pb.BackColor == color)
                {
                    if (pb.BorderStyle != BorderStyle.Fixed3D)
                    {
                        UpdateGameBoard();

                        current_tile_1 = -1;
                        current_tile_2 = -1;
                        current_position = -1;

                        pb.BorderStyle = BorderStyle.Fixed3D;
                        pb.Image = Image.FromFile(@"select_frame.png");

                        // Set current_tile_1 value
                        for (int i = 0; i < tiles.Count; i++)
                        {
                            if ((string)tiles[i].Tag == (string)pb.Tag)
                            {
                                current_tile_1 = i;
                                break;
                            }
                        }

                        // Set current_position value
                        switch (turn)
                        {
                            case (int)COLOR.BLUE:
                                for (int i = 0; i < blue_tiles.Count; i++)
                                {
                                    if (blue_tiles[i] == (string)tiles[current_tile_1].Tag)
                                    {
                                        current_position = i;
                                        break;
                                    }
                                }
                                break;

                            case (int)COLOR.RED:
                                for (int i = 0; i < red_tiles.Count; i++)
                                {
                                    if (red_tiles[i] == (string)tiles[current_tile_1].Tag)
                                    {
                                        current_position = i;
                                        break;
                                    }
                                }
                                break;

                            case (int)COLOR.GREEN:
                                for (int i = 0; i < green_tiles.Count; i++)
                                {
                                    if (green_tiles[i] == (string)tiles[current_tile_1].Tag)
                                    {
                                        current_position = i;
                                        break;
                                    }
                                }
                                break;

                            case (int)COLOR.YELLOW:
                                for (int i = 0; i < yellow_tiles.Count; i++)
                                {
                                    if (yellow_tiles[i] == (string)tiles[current_tile_1].Tag)
                                    {
                                        current_position = i;
                                        break;
                                    }
                                }
                                break;
                        }

                        // Set current_tile_2 value
                        try
                        {
                            switch (turn)
                            {
                                case (int)COLOR.BLUE:
                                    for (int i = 0; i < tiles.Count; i++)
                                    {
                                        if ((string)tiles[i].Tag == blue_tiles[current_position + dice])
                                        {
                                            if (tiles[i].BackColor != color)
                                            {
                                                current_tile_2 = i;
                                                tiles[current_tile_2].Image = Image.FromFile(@"whitelist_frame.png");
                                                break;
                                            }
                                        }
                                    }
                                    break;

                                case (int)COLOR.RED:
                                    for (int i = 0; i < tiles.Count; i++)
                                    {
                                        if ((string)tiles[i].Tag == red_tiles[current_position + dice])
                                        {
                                            if (tiles[i].BackColor != color)
                                            {
                                                current_tile_2 = i;
                                                tiles[current_tile_2].Image = Image.FromFile(@"whitelist_frame.png");
                                                break;
                                            }
                                        }
                                    }
                                    break;

                                case (int)COLOR.GREEN:
                                    for (int i = 0; i < tiles.Count; i++)
                                    {
                                        if ((string)tiles[i].Tag == green_tiles[current_position + dice])
                                        {
                                            if (tiles[i].BackColor != color)
                                            {
                                                current_tile_2 = i;
                                                tiles[current_tile_2].Image = Image.FromFile(@"whitelist_frame.png");
                                                break;
                                            }
                                        }
                                    }
                                    break;

                                case (int)COLOR.YELLOW:
                                    for (int i = 0; i < tiles.Count; i++)
                                    {
                                        if ((string)tiles[i].Tag == yellow_tiles[current_position + dice])
                                        {
                                            if (tiles[i].BackColor != color)
                                            {
                                                current_tile_2 = i;
                                                tiles[current_tile_2].Image = Image.FromFile(@"whitelist_frame.png");
                                                break;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        catch { }
                    }
                    else // Unselect tile
                    {
                        UpdateGameBoard();

                        current_tile_1 = -1;
                        current_tile_2 = -1;
                        current_position = -1;
                    }
                }
                else
                {
                    try
                    {
                        // Check if clicked tile is same as current_tile_2
                        if (pb.Name == tiles[current_tile_2].Name)
                        {
                            UpdateGameBoard();

                            // Position and resize anim_pb
                            anim_pb.Left = tiles[current_tile_1].Left;
                            anim_pb.Top = tiles[current_tile_1].Top;
                            anim_pb.Width = tiles[current_tile_1].Width;
                            anim_pb.Height = tiles[current_tile_1].Height;

                            anim_pb.BackColor = color;
                            anim_pb.Visible = true;
                            anim_pb.BringToFront();

                            if (tiles[current_tile_1].Name.StartsWith("spawn_"))
                            {
                                tiles[current_tile_1].Visible = false;
                            }
                            else
                            {
                                tiles[current_tile_1].BackColor = Color.White;
                            }

                            move_x = (tiles[current_tile_1].Left - pb.Left) / 30;
                            move_y = (tiles[current_tile_1].Top - pb.Top) / 30;
                            move_size = (tiles[current_tile_1].Width - pb.Width) / 30;
                            move_timer.Start();
                        }
                    }
                    catch { }
                }
            }
        }

        // Timer ticks
        private void dice_tick(object sender, EventArgs e)
        {
            if (dice_anim < 27)
            {
                int last_num = dice;

                while (dice == last_num)
                {
                    dice = rnd.Next(1, 7);
                }
                dice_label.Text = "" + dice;
                dice_anim++;
            }
            else
            {
                dice_timer.Stop();
                dice_anim = 0;

                // Check if human, if so check if move is possible
                if (turn == human)
                {
                    switch (turn)
                    {
                        case (int)COLOR.BLUE:
                            // Check if all blue are in spawn
                            if (spawn_b1.Visible && spawn_b2.Visible && spawn_b3.Visible && spawn_b4.Visible)
                            {
                                if (dice == 1 || dice == 6)
                                {
                                    // Okay to play
                                }
                                else
                                {
                                    change_player_timer.Start();
                                }
                            }
                            else
                            {
                                if (tile_1.BackColor != color)
                                {
                                    if (dice == 1 || dice == 6)
                                    {
                                        if (!spawn_b1.Visible && !spawn_b2.Visible && !spawn_b3.Visible && !spawn_b4.Visible)
                                        {
                                            if (CheckForMove())
                                            {
                                                // Okay to play
                                            }
                                            else
                                            {
                                                if (dice == 6)
                                                {
                                                    button_dice.Visible = true;
                                                }
                                                else
                                                {
                                                    change_player_timer.Start();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Okay to play
                                        }
                                    }
                                    else
                                    {
                                        if (CheckForMove())
                                        {
                                            // Okay to play
                                        }
                                        else
                                        {
                                            change_player_timer.Start();
                                        }
                                    }
                                }
                                else
                                {
                                    // Okay to play
                                }
                            }
                            break;

                        case (int)COLOR.RED:
                            // Check if all red are in spawn
                            if (spawn_r1.Visible && spawn_r2.Visible && spawn_r3.Visible && spawn_r4.Visible)
                            {
                                if (dice == 1 || dice == 6)
                                {
                                    // Okay to play
                                }
                                else
                                {
                                    change_player_timer.Start();
                                }
                            }
                            else
                            {
                                if (tile_13.BackColor != color)
                                {
                                    if (dice == 1 || dice == 6)
                                    {
                                        if (!spawn_r1.Visible && !spawn_r2.Visible && !spawn_r3.Visible && !spawn_r4.Visible)
                                        {
                                            if (CheckForMove())
                                            {
                                                // Okay to play
                                            }
                                            else
                                            {
                                                if (dice == 6)
                                                {
                                                    button_dice.Visible = true;
                                                }
                                                else
                                                {
                                                    change_player_timer.Start();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Okay to play
                                        }
                                    }
                                    else
                                    {
                                        if (CheckForMove())
                                        {
                                            // Okay to play
                                        }
                                        else
                                        {
                                            change_player_timer.Start();
                                        }
                                    }
                                }
                                else
                                {
                                    // Okay to play
                                }
                            }
                            break;

                        case (int)COLOR.GREEN:
                            // Check if all green are in spawn
                            if (spawn_g1.Visible && spawn_g2.Visible && spawn_g3.Visible && spawn_g4.Visible)
                            {
                                if (dice == 1 || dice == 6)
                                {
                                    // Okay to play
                                }
                                else
                                {
                                    change_player_timer.Start();
                                }
                            }
                            else
                            {
                                if (tile_25.BackColor != color)
                                {
                                    if (dice == 1 || dice == 6)
                                    {
                                        if (!spawn_g1.Visible && !spawn_g2.Visible && !spawn_g3.Visible && !spawn_g4.Visible)
                                        {
                                            if (CheckForMove())
                                            {
                                                // Okay to play
                                            }
                                            else
                                            {
                                                if (dice == 6)
                                                {
                                                    button_dice.Visible = true;
                                                }
                                                else
                                                {
                                                    change_player_timer.Start();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Okay to play
                                        }
                                    }
                                    else
                                    {
                                        if (CheckForMove())
                                        {
                                            // Okay to play
                                        }
                                        else
                                        {
                                            change_player_timer.Start();
                                        }
                                    }
                                }
                                else
                                {
                                    // Okay to play
                                }
                            }
                            break;

                        case (int)COLOR.YELLOW:
                            // Check if all yellow are in spawn
                            if (spawn_y1.Visible && spawn_y2.Visible && spawn_y3.Visible && spawn_y4.Visible)
                            {
                                if (dice == 1 || dice == 6)
                                {

                                }
                                else
                                {
                                    change_player_timer.Start();
                                }
                            }
                            else
                            {
                                if (tile_37.BackColor != color)
                                {
                                    if (dice == 1 || dice == 6)
                                    {
                                        if (!spawn_y1.Visible && !spawn_y2.Visible && !spawn_y3.Visible && !spawn_y4.Visible)
                                        {
                                            if (CheckForMove())
                                            {
                                                // Okay to play
                                            }
                                            else
                                            {
                                                if (dice == 6)
                                                {
                                                    button_dice.Visible = true;
                                                }
                                                else
                                                {
                                                    change_player_timer.Start();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Okay to play
                                        }
                                    }
                                    else
                                    {
                                        if (CheckForMove())
                                        {
                                            // Okay to play
                                        }
                                        else
                                        {
                                            change_player_timer.Start();
                                        }
                                    }
                                }
                                else
                                {
                                    // Okay to play
                                }
                            }
                            break;
                    }
                    
                }
            }
        }

        private void change_player_tick(object sender, EventArgs e)
        {
            change_player_timer.Stop();
            ChangePlayer();
        }

        private void bot_tick(object sender, EventArgs e)
        {
            if (bot_anim == 0)
            {
                dice_timer.Start();
                bot_timer.Interval = 2000;
                bot_anim++;
            }
            else if (bot_anim == 1)
            {
                bot_timer.Stop();
                bot_timer.Interval = 1500;
                bot_anim = 0;

                if (BOT_CheckSpawn())
                {
                    BOT_PlayFormSpawn();
                }
                else
                {
                    if (dice == 1 || dice == 6)
                    {
                        bool empty = true;

                        // Check if whole game board is missing current color
                        for (int i = 0; i < tiles.Count; i++)
                        {
                            if (tiles[i].BackColor == color)
                            {
                                if (tiles[i].Name.StartsWith("tile_"))
                                {
                                    empty = false;
                                }
                            }
                        }
                        
                        if (!empty) // If game board NOT empty
                        {
                            bool check = false;

                            // Check if current player's spawn is empty
                            switch (turn)
                            {
                                case (int)COLOR.BLUE:
                                    if (!spawn_b1.Visible && !spawn_b2.Visible && !spawn_b3.Visible && !spawn_b4.Visible)
                                    {
                                        check = true;
                                    }
                                    break;

                                case (int)COLOR.RED:
                                    if (!spawn_r1.Visible && !spawn_r2.Visible && !spawn_r3.Visible && !spawn_r4.Visible)
                                    {
                                        check = true;
                                    }
                                    break;

                                case (int)COLOR.GREEN:
                                    if (!spawn_g1.Visible && !spawn_g2.Visible && !spawn_g3.Visible && !spawn_g4.Visible)
                                    {
                                        check = true;
                                    }
                                    break;

                                case (int)COLOR.YELLOW:
                                    if (!spawn_y1.Visible && !spawn_y2.Visible && !spawn_y3.Visible && !spawn_y4.Visible)
                                    {
                                        check = true;
                                    }
                                    break;
                            }

                            if (check) // If spawn IS empty
                            {
                                BOT_MoveTile();
                            }
                            else
                            {
                                // Check if opponent is on first tile
                                switch (turn)
                                {
                                    case (int)COLOR.BLUE:
                                        if (tile_1.BackColor != Color.White && tile_1.BackColor != color)
                                        {
                                            BOT_PlayFormSpawn();
                                            return;
                                        }
                                        break;

                                    case (int)COLOR.RED:
                                        if (tile_13.BackColor != Color.White && tile_13.BackColor != color)
                                        {
                                            BOT_PlayFormSpawn();
                                            return;
                                        }
                                        break;

                                    case (int)COLOR.GREEN:
                                        if (tile_25.BackColor != Color.White && tile_25.BackColor != color)
                                        {
                                            BOT_PlayFormSpawn();
                                            return;
                                        }
                                        break;

                                    case (int)COLOR.YELLOW:
                                        if (tile_37.BackColor != Color.White && tile_37.BackColor != color)
                                        {
                                            BOT_PlayFormSpawn();
                                            return;
                                        }
                                        break;
                                }

                                // If opponent is not on first tile
                                int percentage = rnd.Next(0, 100);

                                if (percentage < 20)
                                {
                                    switch (turn)
                                    {
                                        case (int)COLOR.BLUE:
                                            if (tile_1.BackColor != color)
                                            {
                                                BOT_PlayFormSpawn();
                                            }
                                            else
                                            {
                                                BOT_MoveTile();
                                            }
                                            break;

                                        case (int)COLOR.RED:
                                            if (tile_13.BackColor != color)
                                            {
                                                BOT_PlayFormSpawn();
                                            }
                                            else
                                            {
                                                BOT_MoveTile();
                                            }
                                            break;

                                        case (int)COLOR.GREEN:
                                            if (tile_25.BackColor != color)
                                            {
                                                BOT_PlayFormSpawn();
                                            }
                                            else
                                            {
                                                BOT_MoveTile();
                                            }
                                            break;

                                        case (int)COLOR.YELLOW:
                                            if (tile_37.BackColor != color)
                                            {
                                                BOT_PlayFormSpawn();
                                            }
                                            else
                                            {
                                                BOT_MoveTile();
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    BOT_MoveTile();
                                }
                            }
                        }
                        else
                        {
                            BOT_PlayFormSpawn();
                        }
                    }
                    else
                    {
                        BOT_MoveTile();
                    }
                }
            }
        }

        private void move_tick(object sender, EventArgs e)
        {
            if (move_anim < 30)
            {
                anim_pb.Left -= move_x;
                anim_pb.Top -= move_y;
                anim_pb.Width -= move_size;
                anim_pb.Height -= move_size;
                move_anim++;
            }
            else
            {
                move_timer.Stop();
                move_anim = 0;
                anim_pb.Visible = false;

                // Check if player reached goal or kicked player
                if ((string)tiles[current_tile_2].Tag == "goal")
                {
                    tiles[current_tile_2].BackColor = Color.White;
                }
                else
                {
                    if (tiles[current_tile_2].BackColor != Color.White)
                    {
                        KickTile(tiles[current_tile_2]);
                        tiles[current_tile_2].BackColor = color;
                    }
                    else
                    {
                        tiles[current_tile_2].BackColor = color;
                    }
                }

                current_tile_1 = -1;
                current_tile_2 = -1;
                current_position = -1;

                if (CheckForWinner())
                {
                    DialogResult dr;
                    string winner = string.Empty;
                    switch (turn)
                    {
                        case (int)COLOR.BLUE:
                            winner = "Blue";
                            break;

                        case (int)COLOR.RED:
                            winner = "Red";
                            break;

                        case (int)COLOR.GREEN:
                            winner = "Green";
                            break;

                        case (int)COLOR.YELLOW:
                            winner = "Yellow";
                            break;
                    }

                    dr = MessageBox.Show("" + winner + " wins!", "Simple Ludo", MessageBoxButtons.OK);

                    if (dr == DialogResult.OK)
                    {
                        NewGame();
                    }
                }
                else
                {
                    if (dice == 6)
                    {
                        if (turn == human)
                        {
                            button_dice.Visible = true;
                        }
                        else
                        {
                            bot_timer.Start();
                        }
                    }
                    else
                    {
                        ChangePlayer();
                    }
                }
            }
        }

        // Color Select EvenHandlers
        private void pb_cs_click(object sender, EventArgs e)
        {
            var pb = (sender as PictureBox);

            foreach (PictureBox pic in color_select_panel.Controls.OfType<PictureBox>())
            {
                pic.BorderStyle = BorderStyle.None;
            }

            pb.BorderStyle = BorderStyle.FixedSingle;

            switch (pb.Tag)
            {
                case "blue":
                    human = (int)COLOR.BLUE;
                    break;

                case "red":
                    human = (int)COLOR.RED;
                    break;

                case "green":
                    human = (int)COLOR.GREEN;
                    break;

                case "yellow":
                    human = (int)COLOR.YELLOW;
                    break;
            }
            
        }

        private void select_Click(object sender, EventArgs e)
        {
            if (human != -1)
            {
                // Look for color_panel_back
                foreach (Panel p in this.Controls.OfType<Panel>())
                {
                    if (p.Name == "color_panel_back")
                    {
                        p.Dispose();
                        break;
                    }
                }
                color_select_panel.Dispose();
                
                this.Text = "Simple Ludo";

                if (turn == human)
                {
                    button_dice.Visible = true;
                    MessageBox.Show("You go first!", "Simple Ludo");
                }
                else
                {
                    button_dice.Visible = false;
                    bot_timer.Start();
                }
            }
        }

    }
}
