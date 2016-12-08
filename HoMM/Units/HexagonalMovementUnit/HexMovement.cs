using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM.Units.HexagonalMovementUnit
{
    [Serializable]
    abstract class HexMovement
    {
        public abstract Location Turn(Location location);
    }


    [Serializable]
    class Wait : HexMovement
    {
        public override Location Turn(Location location) => location;
    }

    [Serializable]
    class Movement : HexMovement
    {
        public Direction Direction { get; }

        public Movement(Direction direction)
        {
            Direction = direction;
        }

        public override Location Turn(Location location)
        {
            var isEven = location.Y % 2 == 0;

            switch (Direction)
            {
                case Direction.Up:
                    return new Location(location.Y - 1, location.X);
                case Direction.Down:
                    return new Location(location.Y + 1, location.X);
                case Direction.LeftUp:
                    return new Location(isEven ? location.Y - 1 : location.Y, location.X - 1);
                case Direction.LeftDown:
                    return new Location(isEven ? location.Y : location.Y + 1, location.X - 1);
                case Direction.RightUp:
                    return new Location(isEven ? location.Y - 1 : location.Y, location.X + 1);
                case Direction.RightDown:
                    return new Location(isEven ? location.Y : location.Y + 1, location.X + 1);
            }

            throw new ArgumentException($"{nameof(Direction)} contains invalid value!");
        }
    }

}
