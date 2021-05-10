using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake_v2._0
{
    class MovingLogic
    {
        public static ConsoleKey PreviousKey;
        public static ConsoleKey CurrentKey;

        internal void Move(ConsoleKeyInfo currentKey, List<Position> points, Board board, Position preyPosition, int snakeLength, Position specialPreyPosition)
        {
            Position currentPos;

            if (points.Count != 0)
            {
                currentPos = new Position()
                {
                    X = points.Last().X,
                    Y = points.Last().Y
                };
            }
            
            else
            {
                currentPos = new Position()
                {
                    X = board.LetfWallLevel + 6,
                    Y = board.TopWallLevel + 6
                };
            }

            ShiftSnakeDirection(currentPos, currentKey);
            
            DetectCollision(currentPos, board, points, preyPosition, specialPreyPosition);

            points.Add(currentPos);

            CleanUp(points, snakeLength);
        }

        private void ShiftSnakeDirection(Position currentPos, ConsoleKeyInfo currentKey)
        {
            CurrentKey = currentKey.Key;

            if (CurrentKey != ConsoleKey.LeftArrow && CurrentKey != ConsoleKey.RightArrow && CurrentKey != ConsoleKey.UpArrow && CurrentKey != ConsoleKey.DownArrow)
            {
                CurrentKey = PreviousKey;
            }

            switch (CurrentKey)
            {
                case ConsoleKey.LeftArrow:
                    if (PreviousKey == ConsoleKey.RightArrow)
                    {
                        currentPos.X++;
                        CurrentKey = ConsoleKey.RightArrow;
                        break;
                    }
                    currentPos.X--;
                    break;

                case ConsoleKey.RightArrow:
                    if (PreviousKey == ConsoleKey.LeftArrow)
                    {
                        currentPos.X--;
                        CurrentKey = ConsoleKey.LeftArrow;
                        break;
                    }
                    currentPos.X++;
                    break;

                case ConsoleKey.UpArrow:
                    if (PreviousKey == ConsoleKey.DownArrow)
                    {
                        currentPos.Y++;
                        CurrentKey = ConsoleKey.DownArrow;
                        break;
                    }
                    currentPos.Y--;
                    break;

                case ConsoleKey.DownArrow:
                    if (PreviousKey == ConsoleKey.UpArrow)
                    {
                        currentPos.Y--;
                        CurrentKey = ConsoleKey.UpArrow;
                        break;
                    }
                    currentPos.Y++;
                    break;
            }

            if (PreviousKey != CurrentKey)
            {
                PreviousKey = currentKey.Key;
            }
        }

        private void DetectCollision(Position currentPos, Board board, List<Position> points, Position preyPosition, Position specialPreyPosition)
        {
            if (currentPos.Y == board.TopWallLevel || currentPos.Y == board.BottomWallLevel || currentPos.X == board.LetfWallLevel || currentPos.X == board.RightWallLevel)
            {
                if (!Settings.BoardEdgesAsWalls == true)
                {
                    GameLogic.GameOver();
                }
                else
                {
                    GoThroughBoardEdges(currentPos, board);                    
                }
            }

            if (points.Any(p => p.X == currentPos.X && p.Y == currentPos.Y))
            {
                GameLogic.GameOver();
            }

            if (Elements.WallPoints.Any(p => p.X == currentPos.X && p.Y == currentPos.Y))
            {
                GameLogic.GameOver();
            }

            if (preyPosition.X == currentPos.X && preyPosition.Y == currentPos.Y)
            {
                GameLogic.PreyEaten();
            }

            if (specialPreyPosition != null)
            {
                if (specialPreyPosition.X == currentPos.X && specialPreyPosition.Y == currentPos.Y)
                {
                    GameLogic.SpecialPreyEaten();
                }
            }
        }

        private Position GoThroughBoardEdges(Position currentPos, Board board)
        {
            if (currentPos.Y == board.TopWallLevel)
            {
                currentPos.Y = board.BottomWallLevel - 1;
                return currentPos;
            }

            if (currentPos.Y == board.BottomWallLevel)
            {
                currentPos.Y = board.TopWallLevel + 1;
                return currentPos;
            }

            if (currentPos.X == board.LetfWallLevel)
            {
                currentPos.X = board.RightWallLevel - 1;
                return currentPos;
            }

            if (currentPos.X == board.RightWallLevel)
            {
                currentPos.X = board.LetfWallLevel + 1;
                return currentPos;
            }

            return currentPos;
        }

        private static void CleanUp(List<Position> points, int snakeLength)
        {
            while (points.Count() > snakeLength)
            {
                var snakeLastPoint = points.First();
                points.Remove(snakeLastPoint);

                Console.SetCursorPosition(snakeLastPoint.X, snakeLastPoint.Y);
                Console.Write(" ");
            };
        }
    }
}
