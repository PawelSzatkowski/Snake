using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_v2._0.Models
{
    public class FeaturesState
    {
        public int Id { get; set; }
        public static string SpeedUpSnakeMoves { get; set; }
        public static string GenerateWalls { get; set; }
        public static string SnakeGrowth { get; set; }
        public static string GenerateSpecialPrey { get; set; }
        public static string BoardEdgesAsWalls { get; set; }


        public static string BoardSize { get; set; }

        public static string Difficulty { get; set; }
    }
}
