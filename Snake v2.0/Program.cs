using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Snake_v2._0
{
    class Program
    {
        private Menu _mainMenu = new Menu();
        private Menu _featuresMenu = new Menu();

        private IoHelper _ioHelper = new IoHelper();
        private GameLogic _gameLogic = new GameLogic();

        public static TimerSpecialPrey TimerSpecialPrey = new TimerSpecialPrey();

        private bool _goBack = false;
        private bool _exitGame = false;
        
        public static Stopwatch Stopwatch = new Stopwatch();

        static void Main(string[] args)
        {
            new Program().RunGame();
        }

        public void RunGame()
        {
            _gameLogic.EnsureDatabaseCreation();
            _ioHelper.NoteFeaturesStates();

            Settings.Setup();

            RegisterMainMenuOptions();
            RegisterFeaturesMenuOptions();

            int userChoice;

            do
            {
                Console.Clear();
                _mainMenu.PrintAvailableMenuOptions();

                userChoice = _ioHelper.GetMenuOption();
                _mainMenu.ExecuteOption(userChoice);
            }
            while (_exitGame == false);
        }
        

        private void RegisterMainMenuOptions()
        {
            _mainMenu.AddOption(new MenuItem { Key = 1, Action = StartNewGame, Description = "New game" });
            _mainMenu.AddOption(new MenuItem { Key = 2, Action = ShowHighestScores, Description = "Highest scores" });
            _mainMenu.AddOption(new MenuItem { Key = 3, Action = ChangeFeatures, Description = "Change game features" });
            _mainMenu.AddOption(new MenuItem { Key = 4, Action = PrintGameRules, Description = "Game rules" });

            _mainMenu.AddOption(new MenuItem { Key = 9, Action = ExitGame, Description = "Exit game" });
        }

        private void RegisterFeaturesMenuOptions()
        {
            _featuresMenu.AddOption(new MenuItem { Key = 1, Action = Settings.ToggleSpeedUpSnakeMoves, Description = "Toggle speeding up snake moves as the game progresses" });
            _featuresMenu.AddOption(new MenuItem { Key = 2, Action = Settings.ToggleWallsGeneration, Description = "Toggle walls generation" });
            _featuresMenu.AddOption(new MenuItem { Key = 3, Action = Settings.ToggleSnakeGrowth, Description = "Toggle snake growth" });
            _featuresMenu.AddOption(new MenuItem { Key = 4, Action = Settings.ToggleSpecialPreyGeneration, Description = "Toggle special prey generation" });
            _featuresMenu.AddOption(new MenuItem { Key = 5, Action = Settings.ToggleBoardEdgesAsPassableWalls, Description = "Toggle board edges as passable walls" });
            _featuresMenu.AddOption(new MenuItem { Key = 6, Action = Settings.ChangeBoardSize, Description = "Change board size" });
            _featuresMenu.AddOption(new MenuItem { Key = 7, Action = Settings.ChangeGameDifficulty, Description = "Change game difficulty" });
            _featuresMenu.AddOption(new MenuItem { Key = 8, Action = () => { _goBack = true; }, Description = "Back" });
        }
        
        private void StartNewGame()
        {
            if (Settings.Size == Settings.BoardSize.Huge)
            {
                Console.WindowWidth = 122;
            }
            else { Console.WindowWidth = 82; }

            TimerSpecialPrey.SetTimer();
            _gameLogic.PlayNewGame(); 
        }

        private void ShowHighestScores()
        {
            DrawingLogic.DrawHighestScores();
        }

        private void ChangeFeatures()
        {
            Console.Clear();
            int userChoice;
            int notDigitCase = 0;

            do
            {
                Console.Clear();

                _featuresMenu.PrintAvailableMenuOptions();
                _ioHelper.PrintSettingsState();

                userChoice = _ioHelper.GetMenuOption();
                _featuresMenu.ExecuteOption(userChoice);
            }
            while (!_goBack || notDigitCase == userChoice);
            _goBack = false;
        }

        private void PrintGameRules()
        {
            GameRulesPrinter.PrintGameRules();
        }

        private void ExitGame()
        {
            _exitGame = true;
        }
    }
}
