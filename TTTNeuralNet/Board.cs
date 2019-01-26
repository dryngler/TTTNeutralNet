
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTNeuralNet
{
    class Board
    {
        double[] board;
        int totalMoves;

        public Board()
        {
            //board = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            board = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //board = new double[] { 3, 3, 3, 3, 3, 3, 3, 3, 3 };
            totalMoves = 0;
        }

        public double[] GetBoard()
        {
            return board;
        }

        // returns tuple of player 1 and player2 scores based on power of twos
        public double[] GetBoard2()
        {
            double p1 = 0;
            double p2 = 0;
            double[] output = new double[2];

            for (int i = 0; i < board.Length; i++)
            {
                p1 += board[i] == 1 ? Math.Pow(2, i - 1) : 0;
                p2 += board[i] == 2 ? Math.Pow(2, i - 1) : 0;
            }

            output[0] = (p1);
            output[1] = (p2);

            return output;
        }

        public int GetTotalMoves()
        {
            return totalMoves;
        }

        // return true if player1 spot is valid and adds X if valid
        public bool Player1Move(int spot)
        {
            bool valid = true;
            //88 = X 79 = O
            //1 = X 2 = O

            if (board[spot] != 1 && board[spot] != 2)
            {
                board[spot] = 1;
            }
            else
            {
                valid = false;
            }

            totalMoves++;

            return valid;
        }

        public bool Player2Move(int spot)
        {
            bool valid = true;
            if (board[spot] != 1 && board[spot] != 2)
            {
                board[spot] = 2;
            }
            else
            {
                valid = false;
            }
            totalMoves++;
            return valid;
        }

        public bool Win(int player) // 1 = player1; 2 = player 2
        {
            bool gameOver = false;
            double[] win;
            double mark;

            if (player == 1)
            {
                win = new double[] { 1, 1, 1 };
                mark = 1;
            }
            else
            {
                win = new double[] { 2, 2, 2 };
                mark = 2;
            }

            if (board.Take(3) == win)
            {
                gameOver = true;
            }
            else if (board.Skip(3).Take(3) == win)
            {
                gameOver = true;
            }
            else if (board.Skip(6).Take(3) == win)
            {
                gameOver = true;
            }
            else if (board[0] == mark && board[3] == mark && board[6] == mark)
            {
                gameOver = true;
            }
            else if (board[1] == mark && board[4] == mark && board[7] == mark)
            {
                gameOver = true;
            }
            else if (board[2] == mark && board[5] == mark && board[8] == mark)
            {
                gameOver = true;
            }
            else if (board[0] == mark && board[4] == mark && board[8] == mark)
            {
                gameOver = true;
            }
            else if (board[2] == mark && board[4] == mark && board[6] == mark)
            {
                gameOver = true;
            }

            return gameOver;
        }

        public string NumToString(double num)
        {
            char x = (char)(num);
            return x.ToString();
        }

        public void PrintBoard()
        {
            string[] stringBoard = new string[9];

            for (int i = 0; i < board.Length; i++)
            {
                stringBoard[i] = board[i].ToString();

                if (board[i] == 1)
                {
                    stringBoard[i] = "X";
                    //stringBoard[i] = NumToString(88);
                }
                else if (board[i] == 2)
                {
                    stringBoard[i] = "O";
                }
                else
                {
                    stringBoard[i] = "*";
                    //stringBoard[i] = board[i].ToString();
                }

                if (i == 0)
                {
                    Console.Write($"{stringBoard[i]}       ");
                }
                else if (i % 3 != 0)
                {
                    Console.Write($"{stringBoard[i]}       ");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write($"{stringBoard[i]}       ");
                }
            }
            Console.WriteLine();
        }
    }
}
