using System;
using System.Collections.Generic;
using System.Linq;

namespace HoMM
{
    public class Round
    {
        public Map Map { get; }
        public Player[] Players { get; }
        public int DaysPassed { get; private set; } = 0;

        public Round(Map map, params Player[] players)
        {
            Map = map;
            Players = players;
        }

        public Round(string filename, params string[] playerNames)
        {
            Map = new Map(filename);
            Players = playerNames.Select(name => new Player(name, Map)).ToArray();
        }

        public void Update(Player player, Vector2i newLocation)
        {
            if (!Players.Contains(player))
                throw new ArgumentException($"{nameof(player)} is not playing this round!");

            if (player.Location == newLocation)
                return;

            player.Location = newLocation;
            Map[newLocation.X, newLocation.Y].tileObject?.InteractWithPlayer(player);
        }

        private void InteractWithObject(Player currentPlayer, TileObject obj)
        {
            switch (obj.GetType().Name)
            {
                case "Mine":
                    {
                        var m = (Mine)obj;
                        m.Owner = currentPlayer;
                        break;
                    }
                case "ResourcePile":
                    {
                        var rp = (ResourcePile)obj;
                        currentPlayer.GainResources(rp.resource, rp.quantity);
                        obj = null;
                        break;
                    }
                default:
                    break;
            }
        }

        public void DailyTick()
        {
            foreach (var tile in Map)
                if (tile.tileObject is Mine)
                {
                    var m = tile.tileObject as Mine;
                    if (m.Owner != null)
                        m.Owner.GainResources(m.Resource, m.Yield);
                }



            DaysPassed++;
            if (DaysPassed % 7 == 0)
                WeeklyTick();
        }
        public void WeeklyTick()
        {
            foreach (var tile in Map)
                if (tile.tileObject is Dwelling)
                {
                    var d = tile.tileObject as Dwelling;
                    d.AddWeeklyGrowth();
                }
        }
    }
}
