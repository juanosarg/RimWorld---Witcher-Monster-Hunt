using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;

namespace WMHAnimalBehaviours
{
    public class WorldObjectCompProperties_MonsterToHunt : WorldObjectCompProperties
    {
        public WorldObjectCompProperties_MonsterToHunt()
        {
            this.compClass = typeof(WorldObjectComp_MonsterToHunt);
        }
    }
}
