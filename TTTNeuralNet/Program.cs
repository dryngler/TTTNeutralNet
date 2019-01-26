using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTNeuralNet
{
    class Program
    {
        public static Random rand = new Random();

        static void Main(string[] args)
        {
            //game();
            //iniliaze
            //fitness calc
            //selection - sort and crossove
            //mutate

            var net = Train(100, 25);

            //string netString = JsonConvert.SerializeObject(net);
            //File.WriteAllText("network.json", netString);

            //string netBlah = File.ReadAllText("network.json");
            //Network net = JsonConvert.DeserializeObject<Network>(netBlah);


            game(net);
            ;
        }

        private string NumToString(int num)
        {
            char x = (char)(num);
            return x.ToString();
        }

        /*Train2
        static Network Train2(int populationSize, int numGames)
        {
            (Network net, double fitness)[] population = new(Network, double)[populationSize];

            for (int i = 0; i < populationSize; i++)
            {
                population[i] = (new Network(new Sigmoid(), 2, 4, 9), 0);
                population[i].net.Randomize(rand);
            }

            (Network net, double fitness) lastBest = population[0];

            int gen = 0;
            while (true) // hasn't lost 
            {
                gen++;
                if (gen == 10)
                {
                    Console.WriteLine("Done");
                    break;
                }
                // hasn't lost 
                // get last best board before playing, sorting and mutating
                //(Network net, double fitness) lastBest = population[0];

                for (int i = 0; i < population.Length; i++)
                {
                    if (i == 9)
                    {
                        ;
                    }
                    //Board board = new Board();
                    int[] games = new int[numGames];

                    for (int g = 0; g < numGames; g++)
                    {
                        while (true) // makes sure not playing against itself
                        {
                            int rnum = rand.Next(population.Length);
                            if (rnum != i)
                            {
                                games[g] = rand.Next(1, population.Length);
                                break;
                            }
                        }
                    }

                    for (int j = 0; j < numGames; j++)
                    {
                        Board board = new Board();

                        while (true)
                        {
                            //player 1 = outside net
                            double[] output = population[i].net.Compute(board.GetBoard2());
                            int spotChosen = output.ToList().IndexOf(output.Max());
                            board.Player1Move(spotChosen);
                            Console.Clear();
                            board.PrintBoard();

                            if (board.Win(1))
                            {
                                Console.WriteLine("Player 1 you win!");
                                population[i].fitness += 1;
                                break;
                            }
                            else if (board.GetTotalMoves() > 9)
                            {
                                population[i].fitness += 0.5;
                                //population[games[j]].fitness += 0.5;
                                break;
                            }

                            //player 2 = 10 random games
                            double[] output2 = population[games[j]].net.Compute(board.GetBoard2()); //Nan when pased in an altered board
                            int spotChosen2 = output2.ToList().IndexOf(output2.Max());
                            board.Player2Move(spotChosen2);
                            Console.Clear();
                            board.PrintBoard();

                            if (board.Win(2))
                            {
                                Console.WriteLine("Player 1 you lost.");
                                //population[games[j]].fitness += 1;
                                break;
                            }
                            else if (board.GetTotalMoves() > 9)
                            {
                                population[i].fitness += 0.5;
                                //population[games[j]].fitness += 0.5;
                                break;
                            }
                        }
                    }
                }

                // sort population from largest fitness to lowest
                Array.Sort(population, (a, b) => a.fitness.CompareTo(b.fitness));
                (Network net, double fitness)[] popSort = new(Network, double)[population.Length];
                int p = 0;
                for (int i = population.Length - 1; i >= 0; i--)
                {
                    popSort[p] = population[i];
                    p++;
                }
                population = popSort;

                // get top fit
                // crossover
                //mutate

                (Network net, double fitness) bestBoard = population[0];

                var topFitness = bestBoard.fitness;
                foreach (var board in population)
                    if (board.fitness > topFitness)
                    {
                        topFitness = board.fitness;
                        bestBoard = board;
                    }

                if (bestBoard.fitness < lastBest.fitness)
                    bestBoard = lastBest;
                lastBest = bestBoard;

                int start = (int)(population.Length * 0.10);
                int end = (int)(population.Length * 0.90);

                for (int i = start; i < end; i++)
                {
                    population[i].net.Crossover(rand, population[rand.Next(start)].net);
                    population[i].net.Mutate(rand, 0.2);
                }

                for (int i = end; i < population.Length; i++)
                {
                    population[i].net.Randomize(rand);
                }
                if (gen > 10)
                {
                    break;
                }

                //gen++;
                Console.WriteLine($"{gen}");
            }

            return population[0].net;
        }
        */

        static Network Train(int populationSize, int numGames)
        {
            (Network net, double fitness)[] population = new(Network, double)[populationSize];

            for (int i = 0; i < populationSize; i++)
            {
                population[i] = (new Network(Activations.Sigmoid, 9, 16, 9), 0);
                population[i].net.Randomize(rand);
            }

            (Network net, double fitness) lastBest = population[0];

            int gen = 0;
            while (true) // hasn't lost 
            {
                gen++;
                if (gen >= 10)
                {
                    break;
                }

                for (int i = 0; i < population.Length; i++)
                {
                    population[i].fitness = 0;
                }
                for (int gg = 0; gg < numGames; gg++)
                {
                    population = population.OrderBy(a => Guid.NewGuid()).ToArray();

                    Parallel.For(0, population.Length / 2, (int p) =>
                      {
                          ref var playerA = ref population[p];
                          ref var playerB = ref population[p + population.Length / 2];

                          Board board = new Board();

                          while (true) //play until 2 nets complete game
                          {
                              //player 1 = outside net
                              double[] output = playerA.net.Compute(board.GetBoard());

                              List<(int index, double value)> outs = new List<(int, double)>();
                              for (int index = 0; index < output.Length; index++)
                              {
                                  outs.Add((index, output[index]));
                              }
                              outs.Sort((a, b) => b.value.CompareTo(a.value));

                              int spotChosen = 0;
                              for (int x = 0; x < outs.Count; x++)
                              {
                                  if (board.GetBoard()[outs[x].index] == 0)
                                  {
                                      spotChosen = outs[x].index;
                                      break;
                                  }
                                  //if outs[x].index is valid, spotChosen = outs[x].index & valid = true & break
                                  //
                              }

                              //if no spots valid: board full

                              board.Player1Move(spotChosen);

                              if (board.Win(1))
                              {
                                  playerA.fitness += 1;
                                  break;
                              }
                              else if (board.GetTotalMoves() > 9)
                              {
                                  playerA.fitness += 0.5;
                                  playerB.fitness += 0.5;
                                  break;
                              }

                              //player 2 = 10 random games
                              double[] output2 = playerB.net.Compute(board.GetBoard()); //Nan when pased in an altered board

                              List<(int index, double value)> outs2 = new List<(int, double)>();
                              for (int index = 0; index < output2.Length; index++)
                              {
                                  outs2.Add((index, output2[index]));
                              }
                              outs2.Sort((a, b) => b.value.CompareTo(a.value));

                              int spotChosen2 = 0;

                              for (int x = 0; x < outs2.Count; x++)
                              {
                                  if (board.GetBoard()[outs2[x].index] == 0)
                                  {
                                      spotChosen2 = outs2[x].index;
                                      break;
                                  }

                              }

                              board.Player2Move(spotChosen2);

                              if (board.Win(2))
                              {
                                  playerB.fitness += 1;
                                  break;
                              }
                              else if (board.GetTotalMoves() > 9)
                              {
                                  playerA.fitness += 0.5;
                                  playerB.fitness += 0.5;
                                  break;
                              }
                          }
                      });
                }

                // sort population from largest fitness to lowest
                Array.Sort(population, (a, b) => b.fitness.CompareTo(a.fitness));
                // get top fit
                // crossover
                //mutate

                (Network net, double fitness) bestBoard = population[0];

                int start = (int)(population.Length * 0.10);
                int end = (int)(population.Length * 0.90);

                for (int i = start; i < end; i++)
                {
                    population[i].net.Crossover(rand, population[rand.Next(start)].net);
                    population[i].net.Mutate(rand, 0.2);
                }

                for (int i = end; i < population.Length; i++)
                {
                    population[i].net.Randomize(rand);
                }
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"{gen}");
                Console.WriteLine($"{population[0].fitness}");
            }

            return population[0].net;
        }

        static void game(Network net)
        {
            while (true)
            {
                Board board = new Board();
                int totalMoves = 0;
                bool gameWon = false;

                while (totalMoves < 9)
                {
                    Console.Clear();
                    board.PrintBoard();
                    bool validMove1 = false; //gives oppurtunity to go again if spot is taken

                    double[] output = net.Compute(board.GetBoard());

                    List<(int index, double value)> outs = new List<(int, double)>();

                    for (int index = 0; index < output.Length; index++)
                    {
                        outs.Add((index, output[index]));
                    }
                    outs.Sort((a, b) => b.value.CompareTo(a.value));

                    int spotChosen = 0;

                    for (int x = 0; x < outs.Count; x++)
                    {
                        if (board.GetBoard()[outs[x].index] == 0)
                        {
                            spotChosen = outs[x].index;
                            break;
                        }
                    }

                    board.Player1Move(spotChosen);
                    Console.Clear();
                    board.PrintBoard();

                    bool p1GameCheck = board.Win(1);
                    if (p1GameCheck)
                    {
                        Console.WriteLine("Player 1 you win!");
                        Console.WriteLine("Hit enter to play again");
                        gameWon = true;
                        break;
                    }

                    totalMoves = board.GetTotalMoves();
                    if (totalMoves > 9) // way to not need to check this mid loop
                    {
                        Console.WriteLine("Tie");
                        Console.WriteLine("Hit enter to play again");
                        break;
                    }

                    
                    Console.Clear();
                    board.PrintBoard();
                    bool validMove2 = false;
                    while (!validMove2)
                    {
                        Console.WriteLine("Player 2 enter spot");
                        int p2Spot = int.Parse(Console.ReadLine());
                        validMove2 = board.Player2Move(p2Spot - 1);
                        if (!validMove2)
                        {
                            Console.WriteLine("Player 2 spot taken");
                        }
                    }

                    hbool p2GameCheck = board.Win(2); // not getting if player 2 wins
                    if (p2GameCheck)
                    {
                        Console.WriteLine("Player 2 you win!");
                        Console.WriteLine("Hit enter to play again");
                        gameWon = true;
                        break;
                    }

                    totalMoves = board.GetTotalMoves();
                    if (totalMoves > 9) // way to not need to check this mid loop
                    {
                        Console.WriteLine("Tie");
                        Console.WriteLine("Hit enter to play again");
                        break;
                    }

                }
                if (!gameWon)
                {
                    Console.WriteLine("Its a tie!");
                    Console.WriteLine("Hit enter to play again");
                    break;
                }
                Console.ReadKey();
            }
        }
    }
}
