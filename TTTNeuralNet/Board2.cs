using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTNeuralNet
{
    class Board2
    {
        string[][] board;
        int totalMoves;

        public Board2()
        {
            board = new string[][]
            {
                new string[]{"1", "2", "3"},
                new string[]{"4", "5", "6"},
                new string[]{"7", "8", "9"}
            };

            totalMoves = 0;
        }

        // return true if player1 spot is valid and adds X if valid

        public bool player1Move(string spot)
        {
            bool posAdded = false; // position added

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == spot)
                    {
                        board[i][j] = "X";
                        posAdded = true;
                    }
                }
            }
            totalMoves++;
            return posAdded;
        }

        // added O for p2
        public bool player2Move(string spot)
        {
            bool posAdded = false; // position added

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == spot)
                    {
                        board[i][j] = "O";
                        posAdded = true;
                    }
                }
            }
            totalMoves++;
            return posAdded;
        }

        public int GetTotalMoves()
        {
            return totalMoves;
        }

        public bool p1GameCheck() // need to change hard coded down check
        {
            int cross = 0; // check straight across
            int down = 0; // checks down
            int dL = 0; // down left
            int dM = 0; // down middle
            int dR = 0; // down right
            int diagL = 0; // check diagonal left
            int diagR = 0; // diagonal right
            int total = board.Length;

            string symbol = "X"; // for player 1

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == symbol) // board[i][0])
                    {
                        cross++;
                    }
                    if (j == 0)
                    {
                        if (board[i][j] == symbol) // board[0][0])
                        {
                            dL++;
                        }
                    }
                    if (j == 1)
                    {
                        if (board[i][j] == symbol) //board[0][1])
                        {
                            dM++;
                        }
                    }
                    if (j == 2)
                    {
                        if (board[i][j] == symbol) //board[0][2])
                        {
                            dR++;
                        }
                    }
                    if (i == j)
                    {
                        if (board[i][j] == symbol) //board[0][0])
                        {
                            diagL++;
                        }
                    }
                    if (i + j == 2)
                    {
                        if (board[i][j] == symbol) //board[0][board.Length - 1]) //[0][2] on 3X3
                        {
                            diagR++;
                        }
                    }
                }
                if (cross == total || dL == total || dM == total || dR == total || diagL == total || diagR == total)
                {
                    return true;
                }
                else
                {
                    cross = 0;
                }
            }

            return false;
        }

        public bool p2GameCheck() // need to change hard coded down check
        {
            int cross = 0; // check straight across
            int down = 0; // checks down
            int dL = 0; // down left
            int dM = 0; // down middle
            int dR = 0; // down right
            int diagL = 0; // check diagonal left
            int diagR = 0; // diagonal right
            int total = board.Length;

            string symbol = "O"; // for player 2

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == symbol) // board[i][0])
                    {
                        cross++;
                    }
                    if (j == 0)
                    {
                        if (board[i][j] == symbol) // board[0][0])
                        {
                            dL++;
                        }
                    }
                    if (j == 1)
                    {
                        if (board[i][j] == symbol) //board[0][1])
                        {
                            dM++;
                        }
                    }
                    if (j == 2)
                    {
                        if (board[i][j] == symbol) //board[0][2])
                        {
                            dR++;
                        }
                    }
                    if (i == j)
                    {
                        if (board[i][j] == symbol) //board[0][0])
                        {
                            diagL++;
                        }
                    }
                    if (i + j == 2)
                    {
                        if (board[i][j] == symbol) //board[0][board.Length - 1]) //[0][2] on 3X3
                        {
                            diagR++;
                        }
                    }
                }
                if (cross == total || dL == total || dM == total || dR == total || diagL == total || diagR == total)
                {
                    return true;
                }
                else
                {
                    cross = 0;
                }
            }

            return false;
        }
        public void printBoard()
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    Console.Write($"{board[i][j]}       ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }


    }
}
