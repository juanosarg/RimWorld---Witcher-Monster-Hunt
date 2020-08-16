using RimWorld;
using System;
using Verse;
using System.Collections.Generic;

namespace WMHAnimalBehaviours
{
    public class DamageWorker_Demoralizer : DamageWorker_AddInjury
    {
        public override DamageWorker.DamageResult Apply(DamageInfo dinfo, Thing victim)
        {
            Pawn pawn = victim as Pawn;
            if (pawn != null && pawn.Faction == Faction.OfPlayer)
            {
                
                if (!pawn.Dead)
                {
                    
                    pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("WMH_HymPunchEffect"), null);

                }
            }

            DamageWorker.DamageResult damageResult = base.Apply(dinfo, victim);


            return damageResult;
        }


    }
}