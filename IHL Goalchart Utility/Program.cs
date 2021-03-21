using System;
using System.IO;
using System.Threading.Tasks;

class WriteAllText
{
    public static async Task Main()
    {

        // string newgamecheck = "false";
        bool createnewfolder = false;
        string newfolderdirectory = "pog";
        string season = "";
        int gamenum = 0;
        string filename = "";
    walter:
        // await File.WriteAllTextAsync("C:/Users/JD/Documents/1b/walter.txt", writtentext);
        Console.WriteLine("Would you like to create a new game? True or False?");
        bool newgame = bool.Parse(Console.ReadLine());

        if (newgame == true)
        {
            createnewfolder = true;
        }
        else
        {
            createnewfolder = false;
        }
        Console.WriteLine($"Creating new folder? {createnewfolder}");
        Console.WriteLine();
        if (createnewfolder == true)
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
            goto editor;
        }
    editor:
        filename = $"C:/Users/JD/Documents/IHL/{season}/Goal Charts/Game{gamenum}/Game{gamenum}.txt";
        string[][] currenttext = new[] { File.ReadAllLines(filename) }; // gets all current lines of the text doc
        Console.WriteLine($"Season is: {season}\nGame Number is: {gamenum}\nFolder/File Creation?: {createnewfolder}");
        Console.WriteLine();
        Console.WriteLine($"You are editing the goal log file for {gamenum} of the {season} season. Writing current text:");
        Console.WriteLine();
        //  string[] inputtext = new[] { "haha funny text", "barry BEE benson", "walter", "19 Dollar Fortnite Card, who wants it?", "Zdeno Chara", "i am groot" };
        string line = ""; //this next bit displays the current text
        int counter = 0;
        StreamReader file =
    new StreamReader(@$"{filename}"); // haha not code taken off C# documentation at all definitely not, never!!
        while ((line = file.ReadLine()) != null)
        {
            Console.WriteLine(line);
            counter++;
        } // ok this is the last line that does the displaying of the current text
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
        else if (occurence.Contains("scored"))
        {
            occurence = $"scored";
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
        File.AppendAllText(filename, $"{team} {occurence} with {minute}:{second} remaining in the {period}");
        goto editor;





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