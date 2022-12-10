// See https://aka.ms/new-console-template for more information

 class Program
    {
        static Dictionary<string, int> ChoiceDict = new Dictionary<string, int>()
        {
            {"X", 1}, //Rock
            {"Y", 2}, //Paper
            {"Z", 3}  //Scissors
        };

        static Dictionary<string, int> PartTwoDict = new Dictionary<string, int>()
        {
            {"X", 0}, //Lose
            {"Y", 3}, //Draw
            {"Z", 6}  //Win
        };

        static Dictionary<(string elfChoice, string myChoice), int> ResultDict = new Dictionary<(string elfChoice, string myChoice), int>()
        {
            {("A","X"), 3}, // Rock vs Rock
            {("A","Y"), 6}, // Rock vs Paper
            {("A","Z"), 0}, // Rock vs Scissors
            {("B","X"), 0}, // Paper vs Rock
            {("B","Y"), 3}, // Paper vs Paper
            {("B","Z"), 6}, // Paper vs Scissors
            {("C","X"), 6}, // Scissors vs Rock
            {("C","Y"), 0}, // Scissors vs Paper
            {("C","Z"), 3}  // Scissors vs Scissors
        };

        static void Main(string[] args)
        {
            // part 1
            Console.WriteLine("--- Part 1 ---");
            var totalScore = 0;

            var filename = "input.txt";
            foreach(var rpsStrat in File.ReadLines(filename))
            {
                var split = rpsStrat.Split(' ');
                totalScore += CalculatePoints(split[0], split[1]);
            }

            Console.WriteLine(totalScore);

            // part 2
            Console.WriteLine("--- Part 2 ---");
            var partTwoScore = 0;
            foreach(var rpsStrat in File.ReadLines(filename))
            {
                var split = rpsStrat.Split(' ');
                partTwoScore += CalculatePartTwoScore(split[0], split[1]);
            }

            Console.WriteLine(partTwoScore);
        }

        public static int CalculatePoints(string elfChoice, string myChoice)
        {
            int result = 0;
            //Console.WriteLine($"\n{elfChoice}  {myChoice}");
            
            // add points from my choice
            result += ChoiceDict[myChoice];
            //Console.WriteLine($"Points from choice: {ChoiceDict[myChoice]}");


            // add points from W-D-L
            result += ResultDict[(elfChoice, myChoice)];
            //Console.WriteLine($"Points from game: {ResultDict[(elfChoice, myChoice)]}\n");

            return result;
        }

        public static int CalculatePartTwoScore(string elfChoice, string desiredResult)
        {
            int result = 0;

            // add points from my choice
            var desiredScore = PartTwoDict[desiredResult];
            string myChoice = String.Empty;
            foreach(var kvp in ResultDict)
            {
                if(kvp.Key.elfChoice == elfChoice && kvp.Value == desiredScore)
                {
                    myChoice = kvp.Key.myChoice;
                    break;
                }
            }            
            //Console.WriteLine($"\n{elfChoice}  {myChoice}");
            
            result += ChoiceDict[myChoice];
            //Console.WriteLine($"Points from choice: {ChoiceDict[myChoice]}");


            // add points from W-D-L
            result += desiredScore;
            //Console.WriteLine($"Points from game: {ResultDict[(elfChoice, myChoice)]}\n");

            return result;
        }
    }
