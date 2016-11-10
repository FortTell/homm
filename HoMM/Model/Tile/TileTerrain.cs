﻿using System;
using System.Collections.Generic;

namespace HoMM
{
    public class TileTerrain
    {
        public static readonly TileTerrain Road = new TileTerrain(0.75);
        public static readonly TileTerrain Grass = new TileTerrain(1);
        public static readonly TileTerrain Arid = new TileTerrain(1.25);
        public static readonly TileTerrain Snow = new TileTerrain(1.5);
        public static readonly TileTerrain Desert = new TileTerrain(1.5);
        public static readonly TileTerrain Marsh = new TileTerrain(1.75);

        public static readonly IEnumerable<TileTerrain> Nature = new TileTerrain[]
        {
            Grass, Arid, Snow, Desert, Marsh
        };

        public double TravelCost { get; private set; }

        private TileTerrain(double travelCost)
        {
            TravelCost = travelCost;
        }

        static Dictionary<char, TileTerrain> terrainParser = new Dictionary<char, TileTerrain>
        {
            { 'A', Arid },
            { 'D', Desert },
            { 'G', Grass },
            { 'M', Marsh },
            { 'R', Road },
            { 'S', Snow }
        };

        public static TileTerrain Parse(char c)
        {
            if (terrainParser.ContainsKey(c)) return terrainParser[c];
            throw new ArgumentException("Unknown terrain type!");
        }
    }
}
