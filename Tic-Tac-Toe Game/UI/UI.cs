using System;
using System.Text;
using Ex02.ConsoleUtils;

namespace B21_Ex02
{
    public class UI
    {
        private static StringBuilder s_MatrixSb;

        public static void WelcomeMassage()
        {
            Console.WriteLine(@"Welcome to our X Mix Drix Reverse Game ! 
You can quit at any time by pressing the Q key.

Type Enter to start the game 
");
            string enter = Console.ReadLine();
            quit(enter);
            Screen.Clear();
        }

        public static void DefinedSignsToPlayers(char[] i_PlayersSigns, int i_Computer)
        {
            string playerType = "";
            string enter = "";

            if (i_Computer == 0)
            {
                playerType = "Computer";
            }
            else
            {
                playerType = "Player 2";
            }

            Console.WriteLine(string.Format(@"Player 1 you will be sign : {0}
{1} you will be sign : {2}", i_PlayersSigns[0], playerType, i_PlayersSigns[1]));
            Console.WriteLine(@"

Please press Enter to continue...");
            enter = Console.ReadLine();
            quit(enter);
        }

        public static void BeginTurnMessage(char[] i_PlayersSigns, int i_PlayerTurn)
        {
            Console.WriteLine(string.Format(@"{0} is your turn now 
Remember you need to type location in the game board (for example 13)", i_PlayersSigns[i_PlayerTurn]));
        }

        public static void OccupiedLocationNotify()
        {
            Console.WriteLine(@"This location is occupied.
Please choose another location");
        }

        public static void DisplayScore(int[] i_PlayerScores)
        {
            Console.WriteLine(string.Format(@"THE SCORE IS: 
==================
X points : {0} 
O points : {1}", i_PlayerScores[0], i_PlayerScores[1]));
        }

        public static string AskForAnotherRound()
        {
           string userAnswer = "";

           Console.WriteLine(string.Format(@"
Do you want to play another round ?
Y/N"));
            userAnswer = Console.ReadLine();
            quit(userAnswer);
            while (!checkValidYN(userAnswer))
            {
                Console.WriteLine("Please enter Y/N");
                userAnswer = Console.ReadLine();
                quit(userAnswer);
            }

            return userAnswer; 
        }

        public static void LeaveGameMessage(string i_Winner)
        {
           string getOut = "";

           Console.WriteLine(string.Format(@"Hope you enjoyed ;) 
The winner is -- {0} --
Type Enter to exit", i_Winner));
            getOut = Console.ReadLine();
            quit(getOut);
            Environment.Exit(0);
        }

        private static void quit(string i_UserInput)
        {
            if(i_UserInput == "Q")
            {
                Environment.Exit(0);
            }            
        }

        private static bool checkValidYN(string i_Answer)
        {
            bool valid = true;

            if(i_Answer != "Y" && i_Answer != "N")
            {
                valid = false;
            }

            return valid;
        }

        public static int[] AskLocation(char[] i_PlayersSigns, int i_PlayerTurn, int i_GridSize)
        {
            int numLocation = 0; 
            int[] indexes = new int[2];
            string userLocation = "";

            Console.WriteLine(string.Format(string.Format(@"{0} Enter location", i_PlayersSigns[i_PlayerTurn])));
            userLocation = Console.ReadLine();
            quit(userLocation);
            while (!checkValidLocation(userLocation, i_GridSize))
            {
                Console.WriteLine("Invalid location please try again");
                userLocation = Console.ReadLine();
                quit(userLocation);
            }

            numLocation = Int32.Parse(userLocation);
            indexes[0] = numLocation / 10;
            indexes[1] = numLocation % 10;

            return indexes;
        }

        private static bool checkValidLocation(string i_UserLocation, int i_GridSize)
        {
            bool valid = true;
            int num = 0;
            bool isNum = int.TryParse(i_UserLocation, out num);

            if(i_UserLocation.Length != 2)
            {
                valid = false;
            }

            foreach(char c in i_UserLocation){
                if (c - '0' < 1 || c - '0' > i_GridSize)
                {
                    valid = false; 
                }
            }

            if (!isNum)
            {
                valid = false;
            }

            return valid;
        }

        public static int AskGridSize(int i_Min, int i_Max) 
        {
            int userInputNum = 0;
            string userInput = "";

            Console.WriteLine(string.Format("Please choose the size of the grid, between {0} to {1}", i_Min, i_Max));
            userInput = Console.ReadLine();
            quit(userInput);
            while (!checkValidSize(userInput))
            {
                Console.WriteLine(string.Format("pleace enter a number between {0} to {1}", i_Min, i_Max));
                userInput = Console.ReadLine();
                quit(userInput);
            }

            int.TryParse(userInput, out userInputNum);

            return userInputNum;
        }

        private static bool checkValidSize(string i_UserSize)
        {
            bool valid = true;
            int num = 0;
            bool isNum = int.TryParse(i_UserSize, out num);

            if (isNum)
            {
                if (num < 3 || num > 9)
                {
                    valid = false;
                }
            }

            if (!isNum)
            {
                valid = false;
            }

            return valid;
        }

        public static int AskForCompetitor()
        {
            int userInputNum = 0;
            string userInput = "";

            Console.WriteLine(@"Do you want to play against the computer or a friend ? 
For computer type - 0
For friend type - 1");
            userInput = Console.ReadLine();
            quit(userInput);
            while (!checkValidCompetitor(userInput))
            {
                Console.WriteLine(string.Format("please enter 0 for computer or 1 for friend"));
                userInput = Console.ReadLine();
                quit(userInput);
            }

            int.TryParse(userInput, out userInputNum);

            return userInputNum;
        }

        private static bool checkValidCompetitor(string i_UserInput)
        {
            bool valid = true;
            int num = 0;
            bool isNum = int.TryParse(i_UserInput, out num);

            if (isNum)
            {
                if (num < 0 || num > 1)
                {
                    valid = false;
                }
            }

            if (!isNum)
            {
                valid = false;
            }

            return valid;
        }

        public static void PrintGrid(Grid i_GameGrid)
        {
            Screen.Clear();
            int size = i_GameGrid.Size;

            s_MatrixSb = new StringBuilder("");
            for (int i = 1; i < size + 1; i++)
            {
                s_MatrixSb.Append(string.Format("   {0}", i));
            }

            separateRow(size);
            for (int i = 0; i < size; i++)
            {
                s_MatrixSb.Append(string.Format(@"{0}{1}", Environment.NewLine, i + 1));
                for (int j = 0; j < size; j++)
                {
                    s_MatrixSb.Append(string.Format(@" | {0}", i_GameGrid.Matrix[i, j]));
                }

                s_MatrixSb.Append(" | ");
                separateRow(size);
            }

            s_MatrixSb.Append(Environment.NewLine);
            Console.WriteLine(s_MatrixSb.ToString());
        }

        private static void separateRow(int i_Size)
        {
            s_MatrixSb.Append(String.Format(@"{0}  ", Environment.NewLine));
            for (int i = 0; i < i_Size; i++)
            {
                s_MatrixSb.Append("====");
            }

            s_MatrixSb.Append("=");
        }
    }
}
