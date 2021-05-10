using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_v2._0
{
    public class Settings
    {
        public enum GameDifficulty
        {
            Easy = 1,
            Medium = 2,
            Hard = 3
        }
        public enum BoardSize
        {
            Small = 1,
            Medium = 2,
            Large = 3,
            Huge = 4
        }
        public static bool InPlay;

        public static bool SpeedUpSnakeMoves = true;
        public static bool GenerateWalls = true;
        public static bool SnakeGrowth = true;
        public static bool GenerateSpecialPrey = true;
        public static bool BoardEdgesAsWalls = true;
        public static BoardSize Size = BoardSize.Medium;

        public static int WallsCount; 

        public static int SnakeLength;
        public static int Score;
        public static int SpecialScore;
        public static int GameSpeedInMovesPerSecond;

        public static int StartingTurnTime;
        public static int ScoresNeededToFastenUpSpeed;
        public static int GameSpeedMultiplier;

        public static GameDifficulty Difficulty = GameDifficulty.Medium;

        internal static void Setup()
        {
            Console.WindowHeight = 43;
            Console.WindowWidth = 82;

            GameLogic.CurrentKey = null;

            Elements.SnakePoints.Clear();
            Elements.WallPoints.Clear();
            Elements.BannedPreySpawnArea.Clear();

            DetermineAndSetMapSize();
            
            Elements.PreyPosition = null;
            Elements.SpecialPreyPosition = null;

            InPlay = true;

            SnakeLength = 5;
            Score = 0;
            SpecialScore = 0;

            DetermineAndSetDifficulty();
            GetOneTurnTimeInMs();

            Console.CursorVisible = false;
        }

        internal static int GetOneTurnTimeInMs()
        {
            int oneTurnTimeInMs = StartingTurnTime;
            var oneSecond = 1000;

            if (SpeedUpSnakeMoves == true)
            {
                oneTurnTimeInMs = Convert.ToInt32(StartingTurnTime / Math.Sqrt(CalculateGameSpeedMultiplier() + 1));
                GameSpeedInMovesPerSecond = oneSecond / oneTurnTimeInMs;
            }

            GameSpeedInMovesPerSecond = oneSecond / oneTurnTimeInMs;
            return oneTurnTimeInMs;
        }

        private static int CalculateGameSpeedMultiplier()
        {
            GameSpeedMultiplier = Convert.ToInt32(Score / ScoresNeededToFastenUpSpeed);
            
            return GameSpeedMultiplier;
        }

        internal static void ToggleSpeedUpSnakeMoves()
        {
            if (SpeedUpSnakeMoves == true)
            {
                SpeedUpSnakeMoves = false;
            }
            else
            {
                SpeedUpSnakeMoves = true;
            }
        }
        
        internal static void ToggleWallsGeneration()
        {
            if (GenerateWalls == true)
            {
                GenerateWalls = false;
            }
            else
            {
                GenerateWalls = true;
            }
        }

        internal static void ToggleSnakeGrowth()
        {
            if (SnakeGrowth == true)
            {
                SnakeGrowth = false;
            }
            else
            {
                SnakeGrowth = true;
            }
        }

        internal static void ToggleSpecialPreyGeneration()
        {
            if (GenerateSpecialPrey == true)
            {
                GenerateSpecialPrey = false;
            }
            else
            {
                GenerateSpecialPrey = true;
            }
        }

        internal static void ToggleBoardEdgesAsPassableWalls()
        {
            if (BoardEdgesAsWalls == true)
            {
                BoardEdgesAsWalls = false;
            }
            else
            {
                BoardEdgesAsWalls = true;
            }
        }
        internal static void ChangeBoardSize()
        {
            if (Size == BoardSize.Medium)
            {
                Size = BoardSize.Large;
                SetBoardSizeLarge();
                return;
            }

            if (Size == BoardSize.Large)
            {
                Size = BoardSize.Huge;
                SetBoardSizeHuge();
                return;
            }

            if (Size == BoardSize.Huge)
            {
                Size = BoardSize.Small;
                SetBoardSizeSmall();
                return;
            }

            if (Size == BoardSize.Small)
            {
                Size = BoardSize.Medium;
                SetBoardSizeMedium();
                return;
            }
        }

        private static void DetermineAndSetMapSize()
        {
            if (Size == BoardSize.Small)
            {
                SetBoardSizeSmall();
                return;
            }

            if (Size == BoardSize.Medium)
            {
                SetBoardSizeMedium();
                return;
            }

            if (Size == BoardSize.Large)
            {
                SetBoardSizeLarge();
                return;
            }

            else
            {
                SetBoardSizeHuge();
            }
        }

        private static void SetBoardSizeSmall()
        {
            Elements.Board.Height = 20;
            Elements.Board.Width = 30;
            
            SetBoardEdgesLevels();

            WallsCount = 10;
        }

        private static void SetBoardSizeMedium()
        {
            Elements.Board.Height = 30;
            Elements.Board.Width = 45;
            
            SetBoardEdgesLevels();

            WallsCount = 20;
        }

        private static void SetBoardSizeLarge()
        {
            Elements.Board.Height = 40;
            Elements.Board.Width = 60;
            
            SetBoardEdgesLevels();

            WallsCount = 30;
        }

        private static void SetBoardSizeHuge()
        {
            Elements.Board.Height = 40;
            Elements.Board.Width = 120;
            
            SetBoardEdgesLevels();

            WallsCount = 50;
        }

        private static void SetBoardEdgesLevels()
        {
            Elements.Board.TopWallLevel = 1;
            Elements.Board.BottomWallLevel = Elements.Board.Height + Elements.Board.TopWallLevel;
            Elements.Board.LetfWallLevel = 0;
            Elements.Board.RightWallLevel = Elements.Board.Width + 1;
        }

        internal static void ChangeGameDifficulty()
        {
            if (Difficulty == GameDifficulty.Easy)
            {
                Difficulty = GameDifficulty.Medium;
                SetMediumDifficulty();
                return;
            }

            if (Difficulty == GameDifficulty.Medium)
            {
                Difficulty = GameDifficulty.Hard;
                SetHardDifficulty();
                return;
            }

            if (Difficulty == GameDifficulty.Hard)
            {
                Difficulty = GameDifficulty.Easy;
                SetEasyDifficulty();
                return;
            }
        }

        private static void DetermineAndSetDifficulty()
        {
            if (Difficulty == GameDifficulty.Easy)
            {
                SetEasyDifficulty();
                return;
            }

            if (Difficulty == GameDifficulty.Medium)
            {
                SetMediumDifficulty();
                return;
            }

            else 
            {
                SetHardDifficulty();
            }
        }

        private static void SetEasyDifficulty()
        {
            StartingTurnTime = 200;
            ScoresNeededToFastenUpSpeed = 7;
        }

        private static void SetMediumDifficulty()
        {
            StartingTurnTime = 150;
            ScoresNeededToFastenUpSpeed = 5;
        }

        private static void SetHardDifficulty()
        {
            StartingTurnTime = 150;
            ScoresNeededToFastenUpSpeed = 3;
        }
    }
}
