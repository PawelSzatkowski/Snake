using Snake_v2._0.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_v2._0
{
    public class IoHelper
    {
        private FeaturesState _featuresState = new FeaturesState();

        public int GetMenuOption()
        {
            int key;
            int.TryParse(Console.ReadKey().KeyChar.ToString(), out key);

            return key;
        }
        public bool AcceptInput()
        {
            if (!Console.KeyAvailable)
            {
                return false;
            }

            GameLogic.CurrentKey = Console.ReadKey(true);
            var currentKey = GameLogic.CurrentKey.Value.Key;

            if (currentKey == ConsoleKey.UpArrow || currentKey == ConsoleKey.DownArrow || currentKey == ConsoleKey.LeftArrow || currentKey == ConsoleKey.RightArrow)
            {
                return true;
            }

            return false;
        }

        internal void PrintSettingsState() // to jak coś to wiem że da się ładniej, ale przyznaje bez bicia - nie chciało mi się już
        {
            NoteFeaturesStates();

            Console.SetCursorPosition(60, 0);
            Console.WriteLine($"(currently: {FeaturesState.SpeedUpSnakeMoves})");
            Console.SetCursorPosition(60, 1);
            Console.WriteLine($"(currently: {FeaturesState.GenerateWalls})");
            Console.SetCursorPosition(60, 2);
            Console.WriteLine($"(currently: {FeaturesState.SnakeGrowth})");
            Console.SetCursorPosition(60, 3);
            Console.WriteLine($"(currently: {FeaturesState.GenerateSpecialPrey})");
            Console.SetCursorPosition(60, 4);
            Console.WriteLine($"(currently: {FeaturesState.BoardEdgesAsWalls})");
            Console.SetCursorPosition(60, 6);
            Console.WriteLine($"(currently: {FeaturesState.BoardSize})");
            Console.SetCursorPosition(60, 7);
            Console.WriteLine($"(currently: {FeaturesState.Difficulty})");
        }

        public void NoteFeaturesStates()
        {
            FeaturesState.SpeedUpSnakeMoves = (Settings.SpeedUpSnakeMoves == true ? "on" : "off");
            FeaturesState.GenerateWalls = (Settings.GenerateWalls == true ? "on" : "off");
            FeaturesState.SnakeGrowth = (Settings.SnakeGrowth == true ? "on" : "off");
            FeaturesState.GenerateSpecialPrey = (Settings.GenerateSpecialPrey == true ? "on" : "off");
            FeaturesState.BoardEdgesAsWalls = (Settings.BoardEdgesAsWalls == true ? "on" : "off");
            FeaturesState.BoardSize = DetermineMapSize();
            FeaturesState.Difficulty = DetermineGameDifficulty();
        }

        private string DetermineGameDifficulty()
        {
            if (Settings.Difficulty == Settings.GameDifficulty.Easy)
            {
                return "easy";
            }

            if (Settings.Difficulty == Settings.GameDifficulty.Medium)
            {
                return "medium";
            }

            else
            {
                return "hard";
            }
        }

        private string DetermineMapSize()
        {
            if (Settings.Size == Settings.BoardSize.Small)
            {
                return "small";
            }

            if (Settings.Size == Settings.BoardSize.Medium)
            {
                return "medium";
            }

            if (Settings.Size == Settings.BoardSize.Large)
            {
                return "large";
            }

            else
            {
                return "huge";
            }
        }

        internal static string GetPlayerName()
        {
            Console.WriteLine("Enter your name: (max 10 characters)");
            var playerName = Console.ReadLine();

            while (playerName.Length > 10)
            {
                Console.WriteLine("Your name is too long!");
                playerName = Console.ReadLine();
            }

            return playerName;
        }
    }
}
