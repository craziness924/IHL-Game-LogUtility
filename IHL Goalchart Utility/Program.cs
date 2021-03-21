using System;
using System.IO;
using System.Threading.Tasks;

class WriteAllText
{
    public static async Task Main()
    {

        // string newgamecheck = "false";
        //   bool createnewfolder = false;
        string newfolderdirectory = "pog";
        string season = "";
        int gamenum = 0;
        string filename = "";
        int shootoutround = 1;
        // await File.WriteAllTextAsync("C:/Users/JD/Documents/1b/walter.txt", writtentext);
        Console.WriteLine("Would you like to create a new game? True or False?");
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
            if (!File.Exists($"C:/Users/JD/Documents/IHL/{season}/Goal Charts/Game{gamenum}/Game{gamenum}.txt"))
            {
                Console.WriteLine($"Creating a new goal logger file in the following directory: C:/Users/JD/Documents/IHL/{season}/Goal Charts/Game{gamenum}");
                Directory.CreateDirectory($"C:/Users/JD/Documents/IHL/{season}/Goal Charts/Game{gamenum}");
                File.Create($"C:/Users/JD/Documents/IHL/{season}/Goal Charts/Game{gamenum}/Game{gamenum}.txt").Dispose();

                goto editor;
            }
            else
            {
                Console.WriteLine($"File already exists at IHL/{season}/Goal Charts/Game{gamenum}/Game{gamenum}.txt, new folder and text not created.");
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
            if (!File.Exists($"C:/Users/JD/Documents/IHL/{season}/Goal Charts/Game{gamenum}/Game{gamenum}.txt"))
            {
                Console.WriteLine($"File not found! Creating a new log file in the following directory: C:/Users/JD/Documents/IHL/{season}/Goal Charts/Game{gamenum}/Game{gamenum}.txt");
                Directory.CreateDirectory($"C:/Users/JD/Documents/IHL/{season}/Goal Charts/Game{gamenum}");
                File.Create($"C:/Users/JD/Documents/IHL/{season}/Goal Charts/Game{gamenum}/Game{gamenum}.txt").Dispose();
                Console.ReadKey();
                goto editor;
            }
            else
            {
                goto editor;
            }
        }
    editor:
        filename = $"C:/Users/JD/Documents/IHL/{season}/Goal Charts/Game{gamenum}/Game{gamenum}.txt";
        string[][] currenttext = new[] { File.ReadAllLines(filename) }; // gets all current lines of the text doc
        Console.WriteLine($"Season is: {season}\nGame Number is: {gamenum}\nFolder/File Creation?: {newgame}");
        Console.WriteLine();
    editorcontinue:
        Console.WriteLine($"You are editing the goal log file for {gamenum} of the {season} season. Writing current text:");
        Console.WriteLine();
        //  string[] inputtext = new[] { "haha funny text", "barry BEE benson", "walter", "19 Dollar Fortnite Card, who wants it?", "Zdeno Chara", "i am groot" };
        string line = ""; //this next bit displays the current text
        int counter = 0;
        StreamReader file = new StreamReader(@$"{filename}"); // haha not code taken off C# documentation at all definitely not, never!!
        while ((line = file.ReadLine()) != null)
        {
            Console.WriteLine(line);
            counter++;
        }
        file.Close();
        // ok this is the last line that does the displaying of the current text
        //start editing bit
        Console.WriteLine();
        Console.WriteLine("What team?");
        string team = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("What period did the event happen in?");
        int period = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();
        Console.WriteLine($"What minute?");
        int minute = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"What second?");
        int second = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();
        Console.WriteLine("What happened?");
        string occurence = Console.ReadLine();
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
        else if (occurence.Contains("offsides"))
        {
            occurence = "was called offsides";
        }
        else if (occurence.Contains("shootout"))
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to enter shootout mode? Case sensitive.");
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
        if (occurence.Contains("shootout") || occurence.Contains("missed"))
        {
            File.AppendAllText(filename, $"\n{team} {occurence} in Round {shootoutround} of the shootout!");
        }
        else
        {
            File.AppendAllText(filename, $"\n{team} {occurence} with {minute}:{second} remaining in period {period}");
        }
    continuationq:
        Console.WriteLine("");
        Console.WriteLine("Would you like to continue editing?");
        string continuationQ = Console.ReadLine();
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





        /* Console.WriteLine("What team scored?");
         string scoringteam = Console.ReadLine();
         Console.WriteLine();
         Console.WriteLine("What period did the team score in?");
         int scoringperiod = Convert.ToInt32(Console.ReadLine());
         Console.WriteLine();
         Console.WriteLine($"{scoringteam} scored at what minute");
         int scoringminute = Convert.ToInt32(Console.ReadLine());
         Console.WriteLine($"What second did they score at?");
         int scoringsecond = Convert.ToInt32(Console.ReadLine());
         Console.WriteLine(); */
    }
}