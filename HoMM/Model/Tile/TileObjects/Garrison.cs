using System.Collections.Generic;

namespace HoMM
{
    public class Garrison : CapturableObject
    {
        public override bool IsPassable => true;

        public Dictionary<Unit, int> guards;
        public Garrison(Dictionary<Unit, int> guards, Location location) : base(location)
        {
            this.guards = guards;
        }

        public override void InteractWithPlayer(Player p)
        {
            base.InteractWithPlayer(p);
        }
    }
}
