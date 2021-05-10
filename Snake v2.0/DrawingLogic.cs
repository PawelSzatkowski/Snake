using Snake_v2._0.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Snake_v2._0
{
    public class DrawingLogic
    {
        private static Random _rnd = new Random();
        private static Position _writingStartPoint = new Position();

        public void DrawBoard(Board board)
        {
            for (int i = board.LetfWallLevel; i <= board.Width; i++) //drukuje górna krawędź
            {
                Console.SetCursorPosition(i, board.TopWallLevel);
                Console.Write("-");
            }

            for (int i = board.LetfWallLevel; i <= board.Width; i++) //dolna krawędź
            {
                Console.SetCursorPosition(i, (board.BottomWallLevel));
                Console.Write("-");
            }

            for (int i = board.TopWallLevel; i <= board.BottomWallLevel; i++) //lewa krawędź
            {
                Console.SetCursorPosition(board.LetfWallLevel, i);
                Console.Write("|");
            }

            for (int i = board.TopWallLevel; i <= board.BottomWallLevel; i++) //prawa krawędź
            {
                Console.SetCursorPosition(board.RightWallLevel, i);
                Console.Write("|");
            }
        }
        internal void DrawWalls(Board board)
        {
            for (int i = 1; i <= Settings.WallsCount; i++)
            {
                Position startingPosition = new Position()
                {
                    X = _rnd.Next(board.LetfWallLevel + 10, board.RightWallLevel - 10),
                    Y = _rnd.Next(board.TopWallLevel + 10, board.BottomWallLevel - 10)
                };
                
                var wallLength = _rnd.Next(2, 10);
                var wallDirection = _rnd.Next(1, 4);

                DrawSingleWall(wallLength, wallDirection, startingPosition);
            }
        }

        private void DrawSingleWall(int wallLength, int wallDirection, Position startingPosition)
        {
            Console.SetCursorPosition(startingPosition.X, startingPosition.Y);
            Console.Write("▒");
            AddWallPoint(startingPosition.X, startingPosition.Y);

            switch (wallDirection)
            {
                case 1:
                    for (int i = 1; i <= wallLength; i++) //kierunek północny
                    {
                        Console.SetCursorPosition(startingPosition.X, startingPosition.Y - i);
                        Console.Write("▒");

                        var wallX = startingPosition.X;
                        var wallY = startingPosition.Y - i;

                        AddWallPoint(wallX, wallY);
                    }
                    break;
                case 2:
                    for (int i = 1; i <= wallLength; i++) //wschód
                    {
                        Console.SetCursorPosition(startingPosition.X + i, startingPosition.Y);
                        Console.Write("▒");

                        var wallX = startingPosition.X + i;
                        var wallY = startingPosition.Y;

                        AddWallPoint(wallX, wallY);
                    }
                    break;
                case 3:
                    for (int i = 1; i <= wallLength; i++) //południe
                    {
                        Console.SetCursorPosition(startingPosition.X, startingPosition.Y + i);
                        Console.Write("▒");

                        var wallX = startingPosition.X;
                        var wallY = startingPosition.Y + i;

                        AddWallPoint(wallX, wallY);
                    }
                    break;
                case 4:
                    for (int i = 1; i <= wallLength; i++) //zachód
                    {
                        Console.SetCursorPosition(startingPosition.X - i, startingPosition.Y);
                        Console.Write("▒");

                        var wallX = startingPosition.X - i;
                        var wallY = startingPosition.Y;

                        AddWallPoint(wallX, wallY);
                    }
                    break;
            }
        }

        internal static void DrawScoreGotForSpecialPrey(int score)
        {
            Timer timer = new Timer();
            timer.Interval = 5000;
            timer.AutoReset = false;
            timer.Start();

            if (Settings.Size == Settings.BoardSize.Small || Settings.Size == Settings.BoardSize.Medium)
            {
                _writingStartPoint.X = 0;
                _writingStartPoint.Y = Elements.Board.BottomWallLevel + 2;
            }
            else
            {
                _writingStartPoint.X = (Elements.Board.Width / 2) + 3;
                _writingStartPoint.Y = Elements.Board.BottomWallLevel + 1;
            }

            timer.Elapsed += new ElapsedEventHandler(ClearScoreForSpecialPrey);

            Console.SetCursorPosition(_writingStartPoint.X, _writingStartPoint.Y);
            Console.WriteLine($"Congrats! Score for special prey: {score}");
        }

        private static void ClearScoreForSpecialPrey(object sender, ElapsedEventArgs e)
        {
            Console.SetCursorPosition(_writingStartPoint.X, _writingStartPoint.Y);
            int writingLength = 36;

            for (int i = 0; i <= writingLength; i++) //drukuje górna krawędź
            {
                Console.SetCursorPosition(_writingStartPoint.X + i, _writingStartPoint.Y);
                Console.Write(" ");
            }
        }

        private void AddWallPoint(int wallX, int wallY)
        {
            var wallPoint = new Position()
            {
                X = wallX,
                Y = wallY,
            };
            Elements.WallPoints.Add(wallPoint);
            GameLogic.AddWallPointToBannedPreySpawnArea(wallPoint);
        }

        internal void DeleteSpecialPrey()
        {
            if (Elements.SpecialPreyPosition != null)
            {
                Console.SetCursorPosition(Elements.SpecialPreyPosition.X, Elements.SpecialPreyPosition.Y);
                Console.Write(" ");
                Elements.SpecialPreyPosition = null;
            }
        }

        public void DrawScreen(List<Position> points, Position preyPosition, float gameSpeedInMovesPerSecond, int score)
        {
            DrawScore(score);
            DrawGameSpeed(gameSpeedInMovesPerSecond);
            DrawElapsedTime();

            foreach (var point in points)
            {
                Console.SetCursorPosition(point.X, point.Y);
                Console.Write("X");
            }

            if (preyPosition != null)
            {
                Console.SetCursorPosition(preyPosition.X, preyPosition.Y);
                Console.Write("O");
            }

            if (Elements.SpecialPreyPosition != null)
            {
                Console.SetCursorPosition(Elements.SpecialPreyPosition.X, Elements.SpecialPreyPosition.Y);
                Console.Write("S");
            }
        }

        internal Position DrawPrey(Position preyPosition, Board board)
        {
            if (preyPosition == null)
            {
                do
                {
                    preyPosition = new Position()
                    {
                        X = _rnd.Next(board.LetfWallLevel + 1, board.Width),
                        Y = _rnd.Next(board.TopWallLevel + 2, board.Height)
                    };
                }
                while (Elements.BannedPreySpawnArea.Any(p => p.X == preyPosition.X && p.Y == preyPosition.Y));
            }
            return preyPosition;
        }
        internal void DrawSpecialPrey()
        {
            if (Elements.SpecialPreyPosition == null)
            {
                do
                {
                    Elements.SpecialPreyPosition = new Position()
                    {
                        X = _rnd.Next(Elements.Board.Width),
                        Y = _rnd.Next(Elements.Board.Height)
                    };
                }
                while (Elements.BannedPreySpawnArea.Any(p => p.X == Elements.SpecialPreyPosition.X && p.Y == Elements.SpecialPreyPosition.Y));

                if (Elements.SpecialPreyPosition.X <= Elements.Board.LetfWallLevel)
                {
                    Elements.SpecialPreyPosition.X++;
                }

                if (Elements.SpecialPreyPosition.Y <= Elements.Board.TopWallLevel)
                {
                    Elements.SpecialPreyPosition.Y += 2;
                }
            }
        }

        internal static void DrawHighestScores() // a jak to dałoby radę skrócić/uładnić?
        {
            Console.Clear();

            //List < List < HighScore >>    <- ej a może tak? lista w liście?
            
            List<HighScore> highestScores;

            string difficulty = "easy";
            List<HighScore> easyHighestScores = GameLogic.GetHighestScores(difficulty);
            difficulty = "medium";
            List<HighScore> mediumHighestScores = GameLogic.GetHighestScores(difficulty);
            difficulty = "hard";
            List<HighScore> hardHighestScores = GameLogic.GetHighestScores(difficulty);

            DrawHighestScoresDescription();

            highestScores = easyHighestScores;
            int yCoord = 1;
            DrawSpecificDifficultyHighestScores(highestScores, yCoord);

            highestScores = mediumHighestScores;
            yCoord += easyHighestScores.Count + 1;
            DrawSpecificDifficultyHighestScores(highestScores, yCoord);

            highestScores = hardHighestScores;
            yCoord += mediumHighestScores.Count +  easyHighestScores.Count - 1;
            DrawSpecificDifficultyHighestScores(highestScores, yCoord);

            Console.WriteLine("\n" + "Press \"e\" to exit");
            var userExitKey = Console.ReadKey();

            while (userExitKey.KeyChar.ToString() != "e")
            {
                userExitKey = Console.ReadKey();
            }
        }

        private static void DrawHighestScoresDescription()
        {
            Console.Write("  Player name:    Score:    Difficulty:     Board size:");
        }

        private static void DrawSpecificDifficultyHighestScores(List<HighScore> highestScores, int yCoord)
        {
            int i = 0;

            foreach (var highScore in highestScores)
            {
                Console.SetCursorPosition(0, yCoord + i);
                Console.Write($"{i + 1}. {highScore.PlayerName}");
                Console.SetCursorPosition(20, yCoord + i);
                Console.Write($"{highScore.Score}");
                Console.SetCursorPosition(30, yCoord + i);
                Console.Write($"{highScore.Difficulty}");
                Console.SetCursorPosition(45, yCoord + i);
                Console.Write($"{highScore.BoardSize}");
                i++;
            }
            Console.WriteLine();
        }


        private static void DrawGameSpeed(float gameSpeedInMovesPerSecond)
        {
            Console.SetCursorPosition(20, 0);
            Console.Write($"Snake moves per second: {gameSpeedInMovesPerSecond}");
        }

        private static void DrawScore(int score)
        {
            Console.SetCursorPosition(1, 0);
            Console.Write($"Score: {score + Settings.SpecialScore}");
        }

        private static void DrawElapsedTime()
        {
            Program.Stopwatch.Stop();
            Console.SetCursorPosition(1, Elements.Board.BottomWallLevel + 1);

            TimeSpan timeSpan = Program.Stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
            Console.Write($"Elapsed game time: {elapsedTime}");
            Program.Stopwatch.Start();
        }

        internal static void DrawEndGame()
        {
            string gameOver = "Game over. Press any key to exit.";

            var X = (Elements.Board.Width / 2) - (gameOver.Length / 2);
            if (X < 0) { X = 1; }
            var Y = Elements.Board.Height / 2;

            DrawEndGameBracket(X, Y, gameOver.Length);

            Console.SetCursorPosition(X, Y);
            Console.Write(gameOver);
            Console.ReadKey();
            Console.Clear();
        }

        private static void DrawEndGameBracket(int x, int y, int length)
        {
            DrawingLogic drawingLogic = new DrawingLogic(); // bardzo głupio zrobiłem z tymi staticami chyba co? XD
            Board endGameBracket = new Board()
            {
                Height = 3,
                Width = x + length - 1,
                TopWallLevel = y - 1,
                BottomWallLevel = y + 1,
                LetfWallLevel = x - 1,
                RightWallLevel = x + length
            };
            
            drawingLogic.DrawBoard(endGameBracket);
        }
    }
}
