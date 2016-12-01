using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM.Generators
{
    public class GraphSpawner : RandomSpawner
    {
        public GraphSpawner(
            Random random,
            SpawnerConfig config,
            Func<Vector2i, TileObject> factory)

            : base(random, config, factory,
                  maze => Graph.BreadthFirstTraverse(SigmaIndex.Zero, s => s.Neighborhood
                        .Clamp(maze.Size)
                        .Where(n => maze[n] == MazeCell.Empty))
                    .Select((x, i) => new { Distance = i, Node = x.Node })
                    .SkipWhile(x => x.Distance < config.StartRadius)
                    .TakeWhile(x => x.Distance < config.EndRadius)
                    .Select(x => x.Node))
        { }
    }

}
