﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HoMM.Generators
{
    public partial class HommMapGenerator : IMapGenerator
    {
        IMazeGenerator mazeGenerator;
        ITerrainGenerator terrainGenerator;
        ISpawner[] entitiesGenerators;

        private HommMapGenerator(
            IMazeGenerator mazeGenerator, 
            ITerrainGenerator terrainGenerator,
            params ISpawner[] entitiesGenerators)
        {
            if (mazeGenerator == null)
                throw new InvalidOperationException("should select one IMazeGenerator");

            if (terrainGenerator == null)
                throw new InvalidOperationException("should select one ITerrainGenerator");

            this.mazeGenerator = mazeGenerator;
            this.terrainGenerator = terrainGenerator;
            this.entitiesGenerators = entitiesGenerators;
        }

        public Map GenerateMap(int size)
        {
            if (size < 0)
                throw new ArgumentException("Cannot create map of negative size");

            if (size % 2 == 1)
                throw new ArgumentException("Cannot create map of odd size");

            var mapSize = new MapSize(size, size);

            var maze = mazeGenerator.Construct(mapSize);
            var terrainMap = terrainGenerator.Construct(maze);

            var entities = entitiesGenerators
                .Aggregate(SigmaMap.Empty<TileObject>(maze.Size), 
                (m, g) => m.Merge(g.Spawn(maze)));

            var tiles = Location.Square(mapSize)
                .Select(s => new Tile(s, terrainMap[s],
                    maze[s] == MazeCell.Empty ? entities[s] : new Wall(s)));

            return new Map(size, size, tiles);
        }
    }
}
