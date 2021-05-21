using NeoSmart.Unicode;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Exceptions;

class WriteAllText
{
    public static async Task Main()
    {
        string configfiledir = "config.txt";
        CreateConfig(configfiledir);
        string preferreddir = File.ReadAllText(configfiledir);
        string season = "";
        string occurence = "";
        string filename = "";
        float gamenum = 0f;
        int shootoutround = 1;
        string challengingteam = "";
        string offendingteam = "";
        string challengereason = "";
        bool challengesuccessful = false;
        string challengeoutcome = "";
        //  bool playoffgame = false;
        //  string playoffround = "";
        //  string playofteams = "";
        string dirname = "";
        bool tweetmode = false;
        string[] logtext = { };
        string lastevent = "";
        bool shouldtweet = false;
        string hometeam = "";
        string awayteam = "";
        int homescore = 0;
        int awayscore = 0;
        bool powerplaygoal = false;
        string manualtweet = "";
        Console.WriteLine("Tweet mode?");
        if (Console.ReadLine().ToLower().Contains("yes"))
        {
            tweetmode = true;
            Console.WriteLine("Sending you to authentication!");
            await TwitterAuth();
        }
        Console.WriteLine($"\nWould you like to create a new game? Yes/No? Using directory: {preferreddir}");
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
        Console.WriteLine("What season is this game for?");
        season = Console.ReadLine();
        Console.WriteLine();
        Console.WriteLine("What game number is it?");
        decimal gottengamenum = decimal.Parse(Console.ReadLine());
        gamenum = (float)Math.Round(gottengamenum, 1);
        filename = $"{preferreddir}{season}/Event Logs/Game{gamenum}/Game{gamenum}.txt";
        dirname = $"{preferreddir}{season}/Event Logs/Game{gamenum}";
        if (newgame)
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
                newgame = false;
            }
        }
        else
        {
            /*  Console.WriteLine("What season is the game you're editing?");
              season = Console.ReadLine();
              Console.WriteLine();
              Console.WriteLine("What game number are you editing?");
              gamenum = float.Parse(Console.ReadLine()); */
            Console.WriteLine($"");
            if (!File.Exists($"{filename}"))
            {
                Console.WriteLine($"File not found! Creating a new log file in the following directory: {filename}");
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
        if (newgame)
        {
            Console.WriteLine("Who is the home team?");
            hometeam = Console.ReadLine();
            Console.WriteLine("\n\nWho is the away team?");
            awayteam = Console.ReadLine();
            File.AppendAllText(filename, $"Home Team: {hometeam}\nAway Team: {awayteam}\n\n1ST:");
        }
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
        Console.WriteLine($"What team? \nFor special actions, you can enter the following:\n-Shootout: Enter shootout to enter shootout mode\n-Review: Enter review to log an inconclusive review.\n-Separate/Period: Enter either to generate a period separator in the text file.\n-Stop/Stoppage: Enter either to log a stoppage of play.\nTweet: Tweet something, anything.\n-Finalize: Enter to finalize games or send out final tweets or something idk I havent made it yet.");
        string team = Console.ReadLine();
        if (team.Length > 3)
        {
            team = team.ToLower();
        }
        Thread.Sleep(5);
        if (team.Contains("tweet"))
        {
            Console.WriteLine("\nWhat would you like to send? Enter cancel to cancel.");
            manualtweet = Console.ReadLine();
            if (manualtweet.ToLower().Contains("cancel"))
            {
                goto editorcontinue;
            }
            else
            {
                Console.WriteLine($"Would you like to send out the following?\n{manualtweet}");
                if (Console.ReadLine().ToLower().Contains("yes"))
                {
                    occurence = manualtweet;
                    File.AppendAllText(filename, $"{manualtweet}");
                    goto continuationq;
                }
                else
                {
                    goto editorcontinue;
                }
            }
        }
        if (team.Contains("period") || team.Contains("separate"))
        {
            Console.WriteLine("What period would you like to add a separator for?");
            int separatingperiod = Convert.ToInt32(Console.ReadLine());
            string separatingtext = FormatPeriods(separatingperiod);
            Thread.Sleep(5);
            separatingtext = "\n\n" + separatingtext.ToUpper() + ":";
            File.AppendAllText(filename, separatingtext);
            goto continuationq;
        }
        if (team.Contains("shootout"))
        {
            Console.WriteLine();
            Console.WriteLine("Would you like to enter shootout mode? Yes/no?");
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
        period = FormatPeriods((Convert.ToInt32(period)));
        Console.WriteLine();
        Console.WriteLine($"What minute?");
        int minute = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"What second?");
        int second = Convert.ToInt32(Console.ReadLine());
        string secondscleaned = "";
        secondscleaned = CleanedSeconds(second);
        Console.WriteLine();
        if (team.Contains("stop") || team.Contains("stoppage"))
        {
            Console.WriteLine("\nEnter a reason for the stoppage (optional)");
            string stoppagereason = Console.ReadLine();
            if (stoppagereason.Length <= 1)
            {
                stoppagereason = "Unknown Reason";
            }
            string stoppagetext = $"\nPlay stopped with {minute}:{secondscleaned} remaining in the {period} for {stoppagereason}. ";
            File.AppendAllText(filename, stoppagetext);
            goto continuationq;
        }
        if (team.Contains("review"))
        {
            Console.WriteLine("Inconclusive review?");
            if (Console.ReadLine().ToLower().Contains("yes"))
            {
                string supposedpenalty = "";
                Console.WriteLine("\nWhat was the supposed penalty? Enter cancel to return to start.");
                supposedpenalty = Console.ReadLine();
                if (supposedpenalty.ToLower().Contains("cancel"))
                {
                    Console.WriteLine("\nCancelling inconclusive review logging mode.");
                    goto continuationq;
                }
                else
                {
                    File.AppendAllText(filename, $"\nInconclusive {supposedpenalty} penalty review with {minute}:{secondscleaned} remaining in the {period}.");
                    goto continuationq;
                }
            }
            else
            {
                File.AppendAllText(filename, $"\nPlay is under review with {minute}:{secondscleaned} remaining in the {period}.");
                goto continuationq;
            }

        }
        // skipreviewmode:;
        Console.WriteLine("What happened? Possible options: icing, iced, penalty, offsides, offside, score, goal, challenge, cancel.");
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
            Console.WriteLine("\nPPG?");
            if (Console.ReadLine().ToLower().Contains("yes"))
            {
                occurence = "scored a PPG goal";
            }
            else
            {
                occurence = "scored";
            }
        }
        else if (occurence.Contains("challenge"))
        {
            Console.WriteLine("\nWhat team was challenged?");
            offendingteam = Console.ReadLine();
            Console.WriteLine("\nWhat was the challenge for?");
            challengereason = Console.ReadLine();
            Console.WriteLine("\nWas the challenge successful?");
            if (Console.ReadLine().ToLower().Contains("yes"))
            {
                challengesuccessful = true;
            }
            else
            {
                challengesuccessful = false;
            }
            challengeoutcome = "";
            if (challengesuccessful)
            {
                challengeoutcome = "successfully";
            }
            else
            {
                challengeoutcome = "unsuccessfully";
            }
            File.AppendAllText(filename, $"\n{team} {challengeoutcome} challenged {offendingteam} for {challengereason} with {minute}:{secondscleaned} remaining in the {period}.");
            goto continuationq;
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
        else if (occurence.Contains("scored"))
        {
            File.AppendAllText(filename, $"\n{team} {occurence} with {minute}:{secondscleaned} remaining in the {period}!");
        }
        else
        {
            File.AppendAllText(filename, $"\n{team} {occurence} with {minute}:{secondscleaned} remaining in the {period}.");
        }
    continuationq:
        logtext = File.ReadAllLines(filename);
        lastevent = logtext[logtext.Length - 1];
        powerplaygoal = (lastevent.ToLower().Contains("ppg"));
        if ((occurence.ToLower().Contains("scored") || occurence.ToLower().Contains("challenge") || occurence.ToLower().Contains("review") || occurence.ToLower().Contains("penalty")) && tweetmode)
        {
            shouldtweet = true;
        }
        else if (team.Contains("period") || team.Contains("separate"))
        {
            goto editorcontinue;
        }
        else
        {
            Console.WriteLine($"\nTweet non-important event? Event: {lastevent}");
            if (Console.ReadLine().ToLower().Contains("yes"))
            {
                shouldtweet = true;
            }
            else
            {
                shouldtweet = false;
            }
        }
        if (shouldtweet)
        {
            await StringMaker(lastevent, powerplaygoal);
        }
        /*  Console.WriteLine("Would you like to continue editing?");
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
          } */
        goto editorcontinue;
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
    public static async Task TwitterAuth()
    {
        string apikey = File.ReadAllText("secure/apikey.txt").Trim();
        string apikeysecret = File.ReadAllText("secure/apisecretkey.txt").Trim();
        var appClient = new TwitterClient($"{apikey}", $"{apikeysecret}");
        var authenticationRequest = await appClient.Auth.RequestAuthenticationUrlAsync();
        Process.Start(new ProcessStartInfo(authenticationRequest.AuthorizationURL)
        {
            UseShellExecute = true
        });
        Console.WriteLine("Please enter the PIN from the URL on the target account.");
        File.WriteAllText("secure/pin.txt", Console.ReadLine());
        string pinCode = File.ReadAllText("secure/pin.txt");
        var userCredentials = await appClient.Auth.RequestCredentialsFromVerifierCodeAsync(pinCode, authenticationRequest);
        File.WriteAllText("secure/usercredentials.txt", $"{userCredentials.AccessToken}\n{userCredentials.AccessTokenSecret}");
        Console.Clear();
    }

    public static async Task StringMaker(string lastevent, bool powerplaygoal = false)
    {
        string tweet = lastevent;
        string scoreline = "";
        string leadingteam = "";
        string powerplayteam = "";
        if (lastevent.ToLower().Contains("scored"))
        {
            if (powerplaygoal)
            {
                tweet = Emoji.PoliceCarLight + $"{Emoji.HighVoltage}" + $" {lastevent}";
            }
            else
            {
                tweet = Emoji.PoliceCarLight + $" {lastevent}";
            }
            Console.WriteLine("Append a score update?");
            if (Console.ReadLine().ToLower().Contains("yes"))
            {
                Console.WriteLine("\nWhat's the score? (enter it like 5-4, or whatever it's just a string).");
                scoreline = Console.ReadLine();
                Console.WriteLine("\nWhat team is leading? (don't enter if tied ig)");
                leadingteam = Console.ReadLine();
                tweet = tweet + $"\n\n{scoreline} " + leadingteam;
            }
        }
        else if (lastevent.ToLower().Contains("challenge"))
        {
            if (lastevent.ToLower().Contains("unsuccessfully"))
            {
                tweet = Emoji.CrossMark + $" {lastevent}";
            }
            else if (lastevent.ToLower().Contains("successfully"))
            {
                tweet = Emoji.WhiteHeavyCheckMark + $" {lastevent}";
            }
        }
        else if (lastevent.ToLower().Contains("drew a"))
        {
            tweet = Emoji.HighVoltage + $" {lastevent}";
            Console.WriteLine("\nWho's on the powerplay?");
            powerplayteam = Console.ReadLine();
            tweet = tweet + $" {powerplayteam} is on the powerplay!";
        }
        else if (lastevent.ToLower().Contains("play is under review"))
        {
            tweet = Emoji.Eye + $" {lastevent}";
        }
        else if (lastevent.ToLower().Contains("called offsides"))
        {
            tweet = Emoji.NoEntry + $" {lastevent}";
        }
        else if (lastevent.ToLower().Contains("iced the puck"))
        {
            tweet = Emoji.Snowflake + $" {lastevent}";
        }
        Thread.Sleep(100);
        Console.WriteLine($"Tweet: {tweet}\nAny comments?");
        if (Console.ReadLine().ToLower().Contains("yes"))
        {
            Console.WriteLine("Write your comments and click enter.");
            tweet += Console.ReadLine();
        }
        await Tweeter(tweet);
    }
    static async Task Tweeter(string tweet)
    {
        string apikey = File.ReadAllText("secure/apikey.txt").Trim();
        string apikeysecret = File.ReadAllText("secure/apisecretkey.txt").Trim();
        string token = File.ReadAllText("secure/token.txt").Trim();
        string tokensecret = File.ReadAllText("secure/tokensecret.txt").Trim();
        string pinCode = "";
        token = File.ReadAllLines("secure/usercredentials.txt")[0];
        tokensecret = File.ReadAllLines("secure/usercredentials.txt")[1];

        var appClient = new TwitterClient($"{apikey}", $"{apikeysecret}");
        // Start the authentication process
        var authenticationRequest = await appClient.Auth.RequestAuthenticationUrlAsync();
        //  var authenticatedUser = await appClient.Users.GetAuthenticatedUserAsync(); causes an exception for bad request, not needed atm
        pinCode = File.ReadAllText("secure/pin.txt");
        var userClient = new TwitterClient($"{apikey}", $"{apikeysecret}", $"{token}", $"{tokensecret}");
        Console.WriteLine($"\nSending following tweet: {tweet}");
        /*  Console.WriteLine("\nWanna send the tweet? Debug log moment");
          if (Console.ReadLine().ToLower().Contains("yes"))
          {
              try
              {
                  await userClient.Tweets.PublishTweetAsync($"{tweet}");
              }
              catch (TwitterException e)
              {
                  Console.WriteLine(e.ToString());
                  goto continueanyways;
              }
          } */
        try
        {
            await userClient.Tweets.PublishTweetAsync($"{tweet}");
        }
        catch (TwitterException e)
        {
            Console.WriteLine(e.ToString());
            goto continueanyways;
        }
    continueanyways:;
    }
    public static string FormatPeriods(int separatingperiod)
    {
        string formattedperiod = "";
        if (separatingperiod < 7)
        {
            switch (separatingperiod)
            {
                case 1:
                    formattedperiod = "1st";
                    break;
                case 2:
                    formattedperiod = "2nd";
                    break;
                case 3:
                    formattedperiod = "3rd";
                    break;
                case 4:
                    formattedperiod = "OT period";
                    break;
                case 5:
                    formattedperiod = "2OT period";
                    break;
                case 6:
                    formattedperiod = "3OT period";
                    break;
            }
        }
        else
        {
            formattedperiod = (separatingperiod - 3) + "OT period";
        }
        return formattedperiod;
    }
    public static void CreateConfig(string configdir)
    {
        if (!File.Exists($"{configdir}"))
        {
            File.WriteAllText(configdir, "C:/Users/JD/Documents/IHL/");
            Console.WriteLine("Config file created in exe directory. \nPlease edit 'config.txt' to use your directory and restart the program.");
            Console.WriteLine();
        }
        if (!File.Exists("secure/apikey.txt"))
        {
            Directory.CreateDirectory("secure");
            File.Create("secure/apikey.txt").Close();
            Console.WriteLine();
            Console.WriteLine("Twitter API key config file created. Please enter the API key of the Twitter app before proceeding.");
        }
        if (!File.Exists("secure/apisecretkey.txt"))
        {
            Directory.CreateDirectory("secure");
            File.Create("secure/apisecretkey.txt").Close();
            Console.WriteLine();
            Console.WriteLine("Twitter API Secret key config file created. Please enter the API Secret key of the Twitter app before proceeding.");
        }
        if (!File.Exists("secure/token.txt"))
        {
            Directory.CreateDirectory("secure");
            File.Create("secure/token.txt").Close();
            Console.WriteLine();
            Console.WriteLine("Twitter token config file created. There is no need to edit this file as it'll be overwritten.");
        }
        if (!File.Exists("secure/tokensecret.txt"))
        {
            Directory.CreateDirectory("secure");
            File.Create("secure/tokensecret.txt").Close();
            Console.WriteLine();
            Console.WriteLine("Twitter secret token config file created. There is no need to edit this file as it'll be overwritten.");
        }
        if (!File.Exists("secure/pin.txt"))
        {
            Directory.CreateDirectory("secure");
            File.Create("secure/pin.txt").Close();
            Console.WriteLine();
            Console.WriteLine("OAuth Pin config file created. There is no need to edit this file as it'll be overwritten.");
        }
    }
}