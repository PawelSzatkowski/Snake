using Snake_v2._0.Models;

namespace Snake_v2._0.DataLayer.Models
{
    public class HighScore
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public string SpeedUpSnakeMoves { get; set; }
        public string GenerateWalls { get; set; }
        public string SnakeGrowth { get; set; }
        public string GenerateSpecialPrey { get; set; }
        public string BoardEdgesAsWalls { get; set; }


        public string BoardSize { get; set; }

        public string Difficulty { get; set; }
    }
}
