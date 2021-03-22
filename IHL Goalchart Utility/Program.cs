using System;
using System.IO;

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
        int gamenum = 0;

        int shootoutround = 1;


        Console.WriteLine($"Would you like to create a new game? True or False? Yes/no? Using directory: {preferreddir}");
        bool newgame = false;
        string newgameq = Console.ReadLine();
        if (newgameq.Contains("yes"))
        {
            newgame = true;
        }
        else if (newgameq.Contains("YES"))
        {
            newgame = true;
        }
        else if (newgameq.Contains("Yes"))
        {
            newgame = true;
        }
        else
        {
            newgame = false;
        }
        /*  if (newgame == true) 
          {
              createnewfolder = true;
          }
          else
          {
              createnewfolder = false;
          } */
        Console.WriteLine($"Creating new folder? {newgame}");
        Console.WriteLine();
        if (newgame == true)
        {
            Console.WriteLine("What season is this game for?");
            season = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("What game number is it?");
            gamenum = int.Parse(Console.ReadLine());
            if (!File.Exists($"{preferreddir}{season}/Event Logs/Game{gamenum}/Game{gamenum}.txt"))
            {
                Console.WriteLine($"Creating a new event logger file in the following directory: {preferreddir}{season}/Event Logs/Game{gamenum}");
                Directory.CreateDirectory($"{preferreddir}{season}/Event Logs/Game{gamenum}");
                File.Create($"{preferreddir}{season}/Event Logs/Game{gamenum}/Game{gamenum}.txt").Dispose();

                goto editor;
            }
            else
            {
                Console.WriteLine($"File already exists at IHL/{season}/Event Logs/Game{gamenum}/Game{gamenum}.txt, new folder and text not created.");
            }
        }
        else
        {
            Console.WriteLine("What season is the game you're editing?");
            season = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("What game number are you editing?");
            gamenum = int.Parse(Console.ReadLine());
            Console.WriteLine($"");
            if (!File.Exists($"{preferreddir}{season}/Event Logs/Game{gamenum}/Game{gamenum}.txt"))
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
        filename = $"{preferreddir}{season}/Event Logs/Game{gamenum}/Game{gamenum}.txt";
        string[][] currenttext = new[] { File.ReadAllLines(filename) }; // gets all current lines of the text doc
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
        Console.WriteLine("What team? \nIf you'd like to log a shootout, enter whatever you'd like into the Team and time fields and then type 'shootout' (case sensitive) in the event question.");
        string team = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("What period did the event happen in?");
        int period = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();
        Console.WriteLine($"What minute?");
        int minute = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"What second?");
        int second = Convert.ToInt32(Console.ReadLine());
        string secondscleaned = "";
        secondscleaned = CleanedSeconds(second);
        Console.WriteLine();
        Console.WriteLine("What happened?");
        occurence = Console.ReadLine();
        if (occurence.Contains("icing") || occurence.Contains("iced"))
        {
            occurence = $"iced the puck";
        }
        else if (occurence.Contains("penalty") || occurence.Contains("Penalty"))
        {
            Console.WriteLine();
            Console.WriteLine("What type of penalty?");
            string penaltytype = Console.ReadLine();
            occurence = $"drew a {penaltytype} penalty";
        }
        else if (occurence.Contains("offsides"))
        {
            occurence = "was called offsides";
        }
        else if (occurence.Contains("score") || occurence.Contains("goal"))
        {
            occurence = "scored";
        }
        else if (occurence.Contains("shootout"))
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to enter shootout mode? Case sensitive. yes/no?");
            string shootout = (Console.ReadLine());
            if (shootout.Contains("yes") || shootout.Contains("Yes") || shootout.Contains("YES"))
            {
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
                string shootoutcontinue = Console.ReadLine();
                if (shootoutcontinue.Contains("Yes") || shootoutcontinue.Contains("YES") || shootoutcontinue.Contains("yes"))
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
        string continuationQ = Console.ReadLine();
        if (continuationQ.Contains("yes") || continuationQ.Contains("Yes") || continuationQ.Contains("yes"))
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
            Console.WriteLine("Config file created in exe directory. \nIf this is your first time, please edit 'config.txt' to use your directory and restart the program.");
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