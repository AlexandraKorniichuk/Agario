using System.IO;

namespace AgarioSFML
{
    public class Settings
    {
        public static void LoadSettings(Game game)
        {
            StreamReader sr = new StreamReader("Settings.ini");
            while (!sr.EndOfStream)
            {
                string[] input = sr.ReadLine().Split('=');
                if (input.Length < 2)
                    continue;

                string name = input[0];
                if (int.TryParse(input[1], out int value))
                {
                    typeof(Game).GetField(name)?.SetValue(game, value);
                }
            }
        }
    }
}