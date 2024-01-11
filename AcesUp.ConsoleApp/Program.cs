﻿using AcesUp.Common;

namespace AcesUp.ConsoleApp
{
    internal static class Program
    {
        private static void Main()
        {
            SimulateGamesAndPrintStatistics(10_000);
        }

        private static void SimulateGamesAndPrintStatistics(int games)
        {
            var wins = 0;

            for (var i = 0; i < games; i++)
            {
                if (RunSimulation())
                {
                    wins++;
                }
            }

            Console.WriteLine($"Total simulations: {games}");
            Console.WriteLine($"Won games: {wins}");
            Console.WriteLine($"Win rate: {Math.Round(wins / (double)games * 100, 2)}%");
        }

        private static bool RunSimulation()
        {
            var deck = Deck.CreateShuffledDeck(Random.Shared);
            var game = new Game();
            while (!deck.IsEmpty)
            {
                game.RunAllSteps(deck);
            }

            return game.IsGameWon();
        }
    }
}