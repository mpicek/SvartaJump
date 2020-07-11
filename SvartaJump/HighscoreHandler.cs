using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SvartaJump
{
    

    static class HighscoreHandler
    {
        static string HIGHSCORE_FILE = "highscore.txt";
        static public List<int> read_high_scores()
        {
            /* returns list of highscores */

            List<int> highscores = new List<int>();
            if (File.Exists(HIGHSCORE_FILE))
            {
                using (StreamReader file = new StreamReader(HIGHSCORE_FILE))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        highscores.Add(int.Parse(line));
                    }

                }
            }
            else
            {
                File.Create(HIGHSCORE_FILE).Close();
            }

            return highscores;
        }

        static public string return_high_scores()
        {
            /* returns printable string of highscores */
            List<int> highscores = read_high_scores();
            string highscore_string = "";
            int i = 1;
            foreach (int h in highscores)
            {
                highscore_string += i.ToString() + " ................... ";
                highscore_string += h.ToString();
                highscore_string += "\n";
                i++;
            }
            return highscore_string;
            
        }

        static public void add_high_score(int new_score)
        {
            /* adds highscore to the list if the new score is in top 10 */

            List<int> highscores = read_high_scores();

            highscores.Add(new_score);
            highscores.Sort((a, b) => b.CompareTo(a));

            if (highscores.Count > 10)
                highscores.RemoveAt(10);

            string numbers_to_file = "";

            foreach (int number in highscores)
            {
                numbers_to_file += number.ToString() + "\n";
            }

            using (FileStream file = File.Create(HIGHSCORE_FILE))
            {
                byte[] data = new UTF8Encoding(true).GetBytes(numbers_to_file);
                file.Write(data, 0, data.Length);
            }
        }
    }
}
