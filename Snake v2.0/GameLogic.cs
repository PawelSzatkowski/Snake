using Snake_v2._0.DataLayer;
using Snake_v2._0.DataLayer.Models;
using Snake_v2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Snake_v2._0
{
    class GameLogic
    {
        public static ConsoleKeyInfo? CurrentKey;

        private IoHelper _ioHelper = new IoHelper();
        private DrawingLogic _drawingLogic = new DrawingLogic();
        private MovingLogic _movingLogic = new MovingLogic();

        private static Random _rnd = new Random();

        internal void PlayNewGame()
        {
            Console.Clear();

            _drawingLogic.DrawScreen(Elements.SnakePoints, Elements.PreyPosition, Settings.GameSpeedInMovesPerSecond, Settings.Score);
            _drawingLogic.DrawBoard(Elements.Board);

            if (Settings.GenerateWalls == true)
            {
                _drawingLogic.DrawWalls(Elements.Board);
            }

            while (Settings.InPlay)
            {
                if (_ioHelper.AcceptInput())
                {
                    _drawingLogic.DrawScreen(Elements.SnakePoints, Elements.PreyPosition, Settings.GameSpeedInMovesPerSecond, Settings.Score);
                }

                Elements.PreyPosition = _drawingLogic.DrawPrey(Elements.PreyPosition, Elements.Board);

                if (CurrentKey.HasValue)
                {
                    _movingLogic.Move(CurrentKey.Value, Elements.SnakePoints, Elements.Board, Elements.PreyPosition, Settings.SnakeLength, Elements.SpecialPreyPosition);
                }

                _drawingLogic.DrawScreen(Elements.SnakePoints, Elements.PreyPosition, Settings.GameSpeedInMovesPerSecond, Settings.Score);

                Thread.Sleep(Settings.GetOneTurnTimeInMs());
            }
        }

        internal void EnsureDatabaseCreation()
        {
            using (var context = new SnakeDbContext())
            {
                context.Database.EnsureCreated();
            }
        }

        public static void PreyEaten()
        {
            if (Settings.SnakeGrowth == true)
            {
                Settings.SnakeLength++;
            }

            Settings.Score++;
            Elements.PreyPosition = null;
        }

        public static void SpecialPreyEaten()
        {
            int score = _rnd.Next(1, 20);
            Settings.SpecialScore += score;
            Elements.SpecialPreyPosition = null;
            DrawingLogic.DrawScoreGotForSpecialPrey(score);
        }

        public static void GameOver()
        {
            Settings.InPlay = false;

            DrawingLogic.DrawEndGame();

            Program.Stopwatch.Stop();
            Program.Stopwatch.Reset();

            Program.TimerSpecialPrey.StopTimer();

            AddHighScore();

            new Program().RunGame();
        }

        internal static void AddWallPointToBannedPreySpawnArea(Position wallPoint)
        {
            for (int a = 1; a <= 3; a++)
            {
                for (int i = 1; i <= 3; i++)
                {
                    Position bannedPreySpawnPoint = new Position();

                    bannedPreySpawnPoint.X = wallPoint.X - 2 + i;
                    bannedPreySpawnPoint.Y = wallPoint.Y - 2 + a;
                    
                    if (!Elements.BannedPreySpawnArea.Any(p => p.X == bannedPreySpawnPoint.X && p.Y == bannedPreySpawnPoint.Y))
                    { 
                        Elements.BannedPreySpawnArea.Add(bannedPreySpawnPoint); 
                    }
                }
            }
        }

        private static void AddHighScore() // ... w tym miejscu wydaje mi się że odkryłem w końcu dlaczego nie powinienem wszystkie wrzucać w statica (przy nieudanej próbie dziedziczenia HighScore po FeaturesState i próbie zapisania tego do bazy danych - znalazłem sposób na obejście, ale na około i brzydko :<)
        {
            HighScore highScore = new HighScore()
            {
                PlayerName = IoHelper.GetPlayerName(),
                Score = Settings.Score + Settings.SpecialScore,
                SpeedUpSnakeMoves = FeaturesState.SpeedUpSnakeMoves,
                GenerateWalls = FeaturesState.GenerateWalls,
                SnakeGrowth = FeaturesState.SnakeGrowth,
                GenerateSpecialPrey = FeaturesState.GenerateSpecialPrey,
                BoardEdgesAsWalls = FeaturesState.BoardEdgesAsWalls,
                BoardSize = FeaturesState.BoardSize,
                Difficulty = FeaturesState.Difficulty
            };
            
            using (var context = new SnakeDbContext())
            {
                context.HighScores.Add(highScore);
                context.SaveChanges();
            }
        }

        internal static List<HighScore> GetHighestScores(string difficulty)
        {
            using (var context = new SnakeDbContext())
            {
                return context.HighScores
                    .Where(x => x.Difficulty == difficulty)
                    .OrderByDescending(x => x.Score)
                    .Take(5)
                    .ToList();
            }
        }
    }
}
