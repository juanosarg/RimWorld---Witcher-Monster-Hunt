using RimWorld;
using Verse;
using System.Collections.Generic;
using RimWorld.Planet;
using System;
using System.Diagnostics;

namespace WMHAnimalBehaviours
{
    public class WorldObjectCompProperties_DefeatAllMonstersQuest : WorldObjectCompProperties
    {
        public WorldObjectCompProperties_DefeatAllMonstersQuest()
        {
            this.compClass = typeof(DefeatAllMonstersQuestComp);
        }

        [DebuggerHidden]
        public override IEnumerable<string> ConfigErrors(WorldObjectDef parentDef)
        {
            foreach (string e in base.ConfigErrors(parentDef))
            {
                yield return e;
            }
            if (!typeof(MapParent).IsAssignableFrom(parentDef.worldObjectClass))
            {
                yield return parentDef.defName + " has WorldObjectCompProperties_DefeatAllMonstersQuest but it's not MapParent.";
            }
        }
    }
}