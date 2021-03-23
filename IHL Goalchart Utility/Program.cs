using System;
using System.IO;
using System.Threading;

class WriteAllText
{
    public static void Main()
    {
        string configfiledir = "config.txt";
        CreateConfig(configfiledir);
        string preferreddir = File.ReadAllText(configfiledir);
        string season = "";
        string occurence = "";
        string filename = "";
        float gamenum = 0f;
        int shootoutround = 1;
      //  bool playoffgame = false;
      //  string playoffround = "";
      //  string playofteams = "";
        string dirname = "";
        Console.WriteLine($"Would you like to create a new game? Yes/No? Using directory: {preferreddir}");
        bool newgame = false;
        string newgameq = Console.ReadLine().ToLower();
        if (newgameq.Contains("yes"))
        {
            newgame = true;
        }
        else
        {
            newgame = false;
        }
        /*  Console.WriteLine("Is it a playoff game?");
          string playoffq = Console.ReadLine().ToLower();
          if (playoffq.Contains("yes"))
          {
              playoffgame = true;
              Console.WriteLine("What round is it for?");
              playoffround = Console.ReadLine();
              Console.WriteLine();
              Console.WriteLine("What series is it? Team1vTeam2?");
              playofteams = Console.ReadLine();
          }
          else
          {
              goto noplayoff;
          }
          noplayoff:
          Console.WriteLine($"Creating new folder? {newgame}");
          Console.WriteLine();
          if (playoffgame)
          {
              Console.WriteLine("What season is this game for?");
              season = Console.ReadLine();
              Console.WriteLine();
              Console.WriteLine("What round number is it?");
              gamenum = int.Parse(Console.ReadLine());
              dirname = $"{preferreddir}{season}/Playoffs/Event Logs/{playoffround}/{playofteams}/{gamenum}";
              filename = $"{preferreddir}{season}/Playoffs/Event Logs/{playoffround}/{playofteams}/{gamenum}/{gamenum}.txt";
          }
          else
          {
              Console.WriteLine("What season is this game for?");
              season = Console.ReadLine();
              Console.WriteLine();
              Console.WriteLine("What game number is it?");
              decimal gottengamenum = decimal.Parse(Console.ReadLine());
              gamenum = (float)Math.Round(gottengamenum, 1);
              dirname = $"{preferreddir}{season}/Event Logs/{gamenum}/{gamenum}.txt";
              filename = $"{preferreddir}{season}/Event Logs/{gamenum}/{gamenum}.txt";
          } */
        Console.WriteLine("What season is this game for?");
        season = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("What game number is it?");
        decimal gottengamenum = decimal.Parse(Console.ReadLine());
        gamenum = (float)Math.Round(gottengamenum, 1);
        filename = $"{preferreddir}{season}/Event Logs/Game {gamenum}/Game{gamenum}.txt";
        dirname = $"{preferreddir}{season}/Event Logs/Game {gamenum}";
        if (newgame == true)
        {
            if (!File.Exists($"{filename}"))
            {
                Console.WriteLine($"Creating a new event logger file in the following directory: {filename}");
                Directory.CreateDirectory($"{dirname}");
                File.Create($"{filename}").Dispose();
                goto editor;
            }
            else
            {
                Console.WriteLine($"File already exists at {filename}, new folder and text not created.");
            }
        }
        else
        {
            Console.WriteLine("What season is the game you're editing?");
            season = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("What game number are you editing?");
            gamenum = float.Parse(Console.ReadLine());
            Console.WriteLine($"");
            if (!File.Exists($"{filename}"))
            {
                Console.WriteLine($"File not found! Creating a new log file in the following directory: C:/Users/JD/Documents/IHL/{season}/Event Logs/Game{gamenum}/Game{gamenum}.txt");
                Directory.CreateDirectory($"{preferreddir}{season}/Event Logs/Game{gamenum}");
                File.Create($"{preferreddir}{season}/Event Logs/Game{gamenum}/Game{gamenum}.txt").Dispose();
                Console.ReadKey();
                goto editor;
            }
            else
            {
                goto editor;
            }
        }
    editor:
        //   filename = $"{preferreddir}{season}/Event Logs/Game{gamenum}/Game{gamenum}.txt";
        string[] currenttext = File.ReadAllLines(filename);  // gets all current lines of the text doc
        Console.WriteLine($"Season is: {season}\nGame Number is: {gamenum}\nFolder/File Creation?: {newgame}");
        Console.WriteLine();
    editorcontinue:
        Console.WriteLine($"You are editing the event log file for game {gamenum} of the {season} season. Writing current text:");
        Console.WriteLine();
        //  string[] inputtext = new[] { "haha funny text", "barry BEE benson", "walter", "19 Dollar Fortnite Card, who wants it?", "Zdeno Chara", "i am groot" };
        string line = ""; //this next bit displays the current text
        int counter = 0;
        StreamReader file = new StreamReader(@$"{filename}");
        while ((line = file.ReadLine()) != null)
        {
            Console.WriteLine(line);
            counter++;
        }
        file.Close();
        // ok this is the last line that does the displaying of the current text
        //start editing bit
        Console.WriteLine();
        Console.WriteLine("What team? If you'd like to log a shootout, enter 'Shootout'");
        string team = Console.ReadLine();
        if (team.Length > 3)
        {
            team = team.ToLower();
        }
        Thread.Sleep(5);
        if (team.Contains("shootout"))
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to enter shootout mode? Case sensitive. yes/no?");
            string shootout = (Console.ReadLine()).ToLower();
            if (shootout.Contains("yes"))
            {
                Console.WriteLine("What team?");
                team = Console.ReadLine();
                Console.WriteLine();
            shootoutanotherone:
                Console.WriteLine("Round number?");
                shootoutround = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Was a shootout goal scored?");
                string shootoutscore = Console.ReadLine();
                bool shootoutscorebool = (shootoutscore.Contains("yes"));
                if (shootoutscorebool)
                {
                    occurence = $"scores a shootout goal";
                }
                else
                {
                    occurence = $"missed";
                }
                File.AppendAllText(filename, $"\n{team} {occurence} in Round {shootoutround} of the shootout!");
                Console.WriteLine();
                Console.WriteLine("Is the shootout still ongoing?");
                string shootoutcontinue = Console.ReadLine().ToLower();
                if (shootoutcontinue.Contains("yes"))
                {
                    goto shootoutcontinue;
                }
                else
                {
                    goto continuationq;
                }
            shootoutcontinue:
                Console.WriteLine("Team name?");
                team = Console.ReadLine();
                goto shootoutanotherone;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Returning to the editor...");
                goto editor;
            }
        }
        Console.WriteLine();
        Console.WriteLine("What period did the event happen in?");
        string period = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine($"What minute?");
        int minute = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"What second?");
        int second = Convert.ToInt32(Console.ReadLine());
        string secondscleaned = "";
        secondscleaned = CleanedSeconds(second);
        Console.WriteLine();
        Console.WriteLine("What happened? Possible options: icing, iced, penalty, offsides, offside, score, goal, cancel.");
        occurence = Console.ReadLine().ToLower();
        if (occurence.Contains("icing") || occurence.Contains("iced"))
        {
            occurence = $"iced the puck";
        }
        else if (occurence.Contains("penalty"))
        {
            Console.WriteLine();
            Console.WriteLine("What type of penalty?");
            string penaltytype = Console.ReadLine();
            occurence = $"drew a {penaltytype} penalty";
        }
        else if (occurence.Contains("offsides") || occurence.Contains("offside"))
        {
            occurence = "was called offsides";
        }
        else if (occurence.Contains("score") || occurence.Contains("goal"))
        {
            occurence = "scored";
        }
        else if (occurence.Contains("cancel"))
        {
            Console.WriteLine("Cancelling current edit.");
            goto editorcontinue;
        }
        else
        {
            Console.WriteLine("Event not recognized, returning to editor...");
            goto editorcontinue;
        }
        if (occurence.Contains("shootout") || occurence.Contains("missed"))
        {
            File.AppendAllText(filename, $"\n{team} {occurence} in Round {shootoutround} of the shootout!");
        }
        else
        {
            File.AppendAllText(filename, $"\n{team} {occurence} with {minute}:{secondscleaned} remaining in period {period}.");
        }
    continuationq:
        Console.WriteLine("");
        Console.WriteLine("Would you like to continue editing?");
        string continuationQ = Console.ReadLine().ToLower();
        if (continuationQ.Contains("yes"))
        {
            goto editorcontinue;
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
    public static void CreateConfig(string configdir)
    {
        if (!File.Exists($"{configdir}"))
        {
            File.WriteAllText(configdir, "C:/Users/JD/Documents/IHL/");
            Console.WriteLine("Config file created in exe directory. \nPlease edit 'config.txt' to use your directory and restart the program.");
            Console.WriteLine();
        }
    }
    public static string CleanedSeconds(int seconds)
    {
        string secondsclean = "";
        if (seconds < 10)
        {
            secondsclean = $"0{seconds}";
        }
        else secondsclean = $"{seconds}";
        return secondsclean;
    }
}