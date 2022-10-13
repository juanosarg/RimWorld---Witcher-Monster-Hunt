using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld.Planet;

namespace WMHAnimalBehaviours
{
    public class WorldObjectComp_MonsterToHunt : WorldObjectComp
    {
        public PawnKindDef monsterKindDef;

        public override void PostExposeData()
        {
            Scribe_Defs.Look<PawnKindDef>(ref this.monsterKindDef, "monsterKindDef");
        }
    }
}
