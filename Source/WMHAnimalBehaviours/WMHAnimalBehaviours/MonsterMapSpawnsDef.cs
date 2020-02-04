using RimWorld;
using System;
using Verse;
using System.Collections.Generic;

namespace WMHAnimalBehaviours
{
 

        public class MonsterMapSpawnsDef : Def
        {
            public ThingDef thingDef;
            public bool allowOnWater;
            public int numberToSpawn;
           
            public PawnKindDef associatedMonster;

        }
    }
