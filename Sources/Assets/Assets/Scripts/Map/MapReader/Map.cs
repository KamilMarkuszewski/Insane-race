using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotWheels.Map.MapReader
{
	public class Map
	{
        public int Id;
        public string Name;
        public int DifficultyLevel;
        public int MinPlayers;
        public int MaxPlayers;

        public IList<MapElement> mapElements = new List<MapElement>();
	}
}
