using System;
using System.Collections.Generic;
using System.Text;

namespace Snake_v2._0
{
    public class Elements
    {
        public static Board Board = new Board();

        public static List<Position> SnakePoints = new List<Position>();
        public static List<Position> WallPoints = new List<Position>();
        public static List<Position> BannedPreySpawnArea = new List<Position>();

        public static Position PreyPosition = null;
        public static Position SpecialPreyPosition = null;
    }
}
