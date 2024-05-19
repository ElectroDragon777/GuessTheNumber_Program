using System;
using System.Text;

namespace GuessTheNumber
{
    class GuessTheNumber
    {
        static void Main(string[] args)
        {
            // ====== Game count + Guessing Attempts: ====== //
            int game_Counter = 1;
            int guess_Counter = 0; // Integers count only in the said range later on.
            // ====== Encodings (not necessary) ====== //
            Console.OutputEncoding = Encoding.UTF8;
            // ====== Bool for Cycle ====== //
            bool endCycle = false;
            // ====== Random Number Generated ====== //
            Random randomNum = new Random();
            int chosenNumber = randomNum.Next(1, 101); // [1; 100] is chosen. RRandom (1,101) in Clickteam Fusion also does this.
            // ====== Random Number Cycle Guess ====== //
            Gameplay(endCycle, chosenNumber, randomNum, game_Counter, guess_Counter);
        }
        private static void CenterText(String text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        }
        private static void Gameplay(bool endCycle, int chosenNumber, Random randomNum, int game_Counter, int guess_Counter)
        {
            CenterText("==== Welcome to \"Guess the number\"! ====");
            CenterText("Game: " + game_Counter);
            while (!endCycle) // If false, go on.
            {
                Console.Write("Guess a number in [1; 100]: "); //'\u2208' is "belongs"
                string playerGuess = Console.ReadLine();
                bool IsValid = int.TryParse(playerGuess, out int NumberGuess); // Checks if a number is integer (INT)
                if (!IsValid)
                {
                    Console.WriteLine("Input is not an integer! Resetting...");
                }
                else
                {
                    int numberGuess = Int32.Parse(playerGuess);
                    bool numberInInterval = ((Int32.Parse(playerGuess) >= 1) && (Int32.Parse(playerGuess) <= 100)); // Checks if passed INT is in range 1 <=> 100 (100 included) 
                    // ====== Validation for Range ====== //
                    if (!numberInInterval)
                    {
                        Console.WriteLine("Input {0} is NOT in [1; 100]! Resetting...", numberGuess); //'\u2209' is "doesn't belong to"
                    }
                    else
                    {
                        guess_Counter += 1;
                        // ====== Return the guess' difference to the Random Generated Number ====== //
                        switch (numberGuess, chosenNumber)// Condition in switch-case exists. Can be set up with WHEN. You also need a var to use WHEN. 
                        {
                            case var output when ((numberGuess < chosenNumber) && (Math.Abs(numberGuess - chosenNumber) <= 10)):
                                {
                                    Console.WriteLine("Your number ({0}) is close to the chosen random number, but it is still a bit lower! >~< Try again!", numberGuess);
                                    break;
                                }
                            case var output when ((numberGuess < chosenNumber) && (Math.Abs(numberGuess - chosenNumber) > 10)):
                                {
                                    Console.WriteLine("Your number ({0}) is too low compared to the chosen random number! >~< Try again!", numberGuess);
                                    break;
                                }
                            case var output when ((numberGuess > chosenNumber) && (Math.Abs(numberGuess - chosenNumber) <= 10)):
                                {
                                    Console.WriteLine("Your number ({0}) is close to the chosen random number, but it is still a bit higher! >~< Try again!", numberGuess);
                                    break;
                                }
                            case var output when ((numberGuess > chosenNumber) && (Math.Abs(numberGuess - chosenNumber) > 10)):
                                {
                                    Console.WriteLine("Your number ({0}) is too high compared to the chosen random number! >~< Try again!", numberGuess);
                                    break;
                                }
                            case var output when (numberGuess == chosenNumber):
                                {
                                    Console.WriteLine("Congratulations! :D You guessed it! [Number being {0}]", numberGuess);
                                    endCycle = !endCycle; //Make false to true to break the input.
                                    break;
                                }
                        }
                    }
                }
            }
            Retry(endCycle, chosenNumber, randomNum, game_Counter, guess_Counter);
        }
        private static void Retry(bool endCycle, int chosenNumber, Random randomNum, int game_Counter, int guess_Counter)
        {
            Console.Write("Would you like to play again? [Y(es)/N(o)]: "); // Y, N, Yes, No with lowercased inputs are accepted.
            string choice = Console.ReadLine();
            switch (choice.ToLower()) // Here I showcase a switch-case with one event for multiple cases. (Same event for different cases)
            {
                case "n":
                case "no":
                    {
                        Console.WriteLine("============\nGames played: {0}\nTotal guesses: {1}\n== Average guesses per game: {2} ==\n============", game_Counter, guess_Counter, Math.Round((double)guess_Counter/game_Counter,2));
                        CenterText("Thank you for playing!");
                        break;
                    }
                case "y":
                case "yes":
                    {
                        // == Next game settings: == //
                        chosenNumber = randomNum.Next(1, 101);
                        endCycle = !endCycle; // Setting it to false.
                        game_Counter += 1; // Increasing the game count. Also counter++ works.
                        // Free the console. //
                        Console.Clear();
                        // Here we begin again. //
                        CenterText("Let's do this!");
                        Gameplay(endCycle, chosenNumber, randomNum, game_Counter, guess_Counter);
                        break;
                    }
            }
        }
    }
}
