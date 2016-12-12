using CVARC.V2;
using HoMM.Rules;
using HoMM.Sensors;
using HoMM.Units.HexagonalMovementUnit;
using HoMM.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM.Hero
{
    class HeroRobot : Robot<HommWorld, HommSensorData, HommCommand, HommRules>,
        IHexMovRobot
    {
        public override IEnumerable<IUnit> Units { get; }
        
        public Player Player { get; }
        public double VelocityModifier { get; }

        public Location Location
        {
            get { return Player.Location; }
            set { World.Round.Update(Player, value); }
        }
        
        public HeroRobot()
        {
            Units = new List<IUnit>
            {
                new HexMovUnit(this),
            };
        }
    }
}
