using System;
using System.Collections.Generic;

namespace B21_Ex02
{
    public class GameLogic
    {
        private static Grid s_GameGrid;
        private const int k_Min = 3;
        private const int k_Max = 9;
        private static int s_PlayerTurn;
        private static int[] s_PlayerScores;
        private static char[] s_PlayersSigns = new char[] { 'X', 'O' };
        private static int s_Computer;
        private static int s_GridSize;

        public static void InitGame()
        {
            UI.WelcomeMassage();
            s_GridSize = UI.AskGridSize(k_Min, k_Max);
            s_Computer = chooseCompetitor();
            UI.DefinedSignsToPlayers(s_PlayersSigns, s_Computer);
            s_PlayerScores = new int[] { 0, 0 };
            startGame();
        }

        private static void startGame()
        {
            s_GameGrid = new Grid(s_GridSize);
            UI.PrintGrid(s_GameGrid);
            s_PlayerTurn = 0;
            engineGame();
        }

        private static void engineGame()
        {
            char sign;

            while (!isGameOver(s_GameGrid, 1 - s_PlayerTurn))
            {
                UI.BeginTurnMessage(s_PlayersSigns, s_PlayerTurn);
                playTurn(s_PlayerTurn == 1);
            }

            sign = s_PlayersSigns[1 - s_PlayerTurn];
            if (s_GameGrid.IsFullRow(sign) || s_GameGrid.IsFullCol(sign) || s_GameGrid.IsFullMainDiagonal(sign) || s_GameGrid.IsFullSecodaryDiagonal(sign))
            {
                s_PlayerScores[s_PlayerTurn]++;
            }

            if (s_GameGrid.IsFullBoard() && !s_GameGrid.IsFullRow(sign) && !s_GameGrid.IsFullCol(sign) && !s_GameGrid.IsFullMainDiagonal(sign) && !s_GameGrid.IsFullSecodaryDiagonal(sign))
            {
                s_PlayerScores[s_PlayerTurn]++;
                s_PlayerScores[1 - s_PlayerTurn]++;
            }

            finishRound();
        }

        private static void finishRound()
        {
            string userAnswer = UI.AskForAnotherRound();

            UI.DisplayScore(s_PlayerScores);
            if (userAnswer == "Y")
            {
                startGame();
            }
            else
            {
                leaveGame();
            }
        }

        private static void leaveGame()
        {
            string winner = getWinner(s_PlayerScores);

            UI.LeaveGameMessage(winner);
        }

        private static string getWinner(int[] i_PlayerScores)
        {
            string winner = "O";

            if (i_PlayerScores[0] > i_PlayerScores[1])
            {
                winner = "X";
            }

            if (i_PlayerScores[0] == i_PlayerScores[1])
            {
                winner = "X and O";
            }

            return winner;
        }

        private static int chooseCompetitor()
        {

            return UI.AskForCompetitor();
        }

        private static void playTurn(bool i_Competitor)
        {
            if (!i_Competitor)
            {
                int[] indexes = UI.AskLocation(s_PlayersSigns, s_PlayerTurn, s_GridSize);

                while (!s_GameGrid.AddSign(indexes[0], indexes[1], 'X'))
                {
                    UI.OccupiedLocationNotify();
                    indexes = UI.AskLocation(s_PlayersSigns, s_PlayerTurn, s_GridSize);
                }
            }
            else
            {
                if (s_Computer == 0)
                {
                    List<int> freeCells = s_GameGrid.FreeCells();
                    Random random = new Random();
                    int computerLocation = freeCells[random.Next(0, freeCells.Count)];

                    s_GameGrid.AddSign(computerLocation / 10 + 1, computerLocation % 10 + 1, 'O');
                }
                else
                {
                    int[] indexes = UI.AskLocation(s_PlayersSigns, s_PlayerTurn, s_GridSize);

                    while (!s_GameGrid.AddSign(indexes[0], indexes[1], 'O'))
                    {
                        UI.OccupiedLocationNotify();
                        indexes = UI.AskLocation(s_PlayersSigns, s_PlayerTurn, s_GridSize);
                    }
                }
            }
            UI.PrintGrid(s_GameGrid);
            s_PlayerTurn = s_PlayerTurn == 0 ? 1 : 0;
        }

        private static bool isGameOver(Grid i_GameGrid, int i_PlayerTurn)
        {
            char sign = 'O';

            if (i_PlayerTurn == 0)
            {
                sign = 'X';
            }

            return s_GameGrid.IsFullRow(sign) || s_GameGrid.IsFullCol(sign) || s_GameGrid.IsFullMainDiagonal(sign) || s_GameGrid.IsFullSecodaryDiagonal(sign) || s_GameGrid.IsFullBoard();
        }
    }
}
