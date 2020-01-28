using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld.BaseGen;
using Verse.AI.Group;
using Verse;
using RimWorld;

namespace WMHAnimalBehaviours
{
    public class SymbolResolver_SpawnHuntingMonsters : SymbolResolver
    {

        public override void Resolve(ResolveParams rp)
        {
            ResolveParams canReachEdgeParams = rp;
            BaseGen.symbolStack.Push("ensureCanReachMapEdge", canReachEdgeParams);

            CellRect rect = rp.rect;
            //Chance to use the right half of the rect
            if (Rand.Chance(0.5f))
            {
                rp.rect = new CellRect(rect.minX + (rect.Width / 2), rect.minZ, rect.Width / 2, rect.Height);
            }
            else
            {
                rp.rect = new CellRect(rect.minX, rect.minZ, rect.Width / 2, rect.Height);
            }

            int spawnCount;
            PawnKindDef huntingTarget = rp.singlePawnKindDef;

            if (huntingTarget.wildGroupSize==null) {
                spawnCount = 1;

            } else {
                spawnCount = Rand.RangeInclusive(huntingTarget.wildGroupSize.min, huntingTarget.wildGroupSize.max);

            }

            
           /* if (huntingTarget.combatPower > 102)
                {
                    
                }
                else
                {
                    spawnCount = Rand.RangeInclusive(4, 5);
                }
                spawnCount = (int)((float)spawnCount * (float)(210f / huntingTarget.combatPower));
            */

            for (int i = 0; i < spawnCount; i++)
            {
                ResolveParams resolveParams = rp;
                Faction faction = FactionUtility.DefaultFactionFrom(huntingTarget.defaultFactionType);
                Pawn pawnToSpawn = PawnGenerator.GeneratePawn(huntingTarget, faction);
              
                Lord lord = resolveParams.singlePawnLord;
                if (lord == null)
                {
                    Map map = BaseGen.globalSettings.map;
                    IntVec3 point;
                    LordJob lordJob;
                    if (Rand.Bool && (from x in rp.rect.Cells
                                      where !x.Impassable(map)
                                      select x).TryRandomElement(out point))
                    {
                        lordJob = new LordJob_LongRanged(point);
                    }
                    else
                    {
                        lordJob = new LordJob_AssaultColony(faction, false, false, false, false, false);
                    }
                    lord = LordMaker.MakeNewLord(faction, lordJob, map, null);

                }
                    /* if (pawnToSpawn.Map.mapPawns.SpawnedPawnsInFaction(faction).Any((Pawn p) => p != pawnToSpawn))
                     {
                         Pawn p2 = (Pawn)GenClosest.ClosestThing_Global(pawnToSpawn.Position, pawnToSpawn.Map.mapPawns.SpawnedPawnsInFaction(faction), 99999f, (Thing p) => p != pawnToSpawn && ((Pawn)p).GetLord() != null, null);
                         lord = p2.GetLord();
                     }
                     if (lord == null)
                     {
                         LordJob_DefendPoint lordJob = new LordJob_DefendPoint(pawnToSpawn.Position);
                         lord = LordMaker.MakeNewLord(faction, lordJob, pawnToSpawn.Map, null);
                     }*/

                resolveParams.singlePawnLord = lord;
                resolveParams.faction = faction;
                resolveParams.singlePawnToSpawn = pawnToSpawn;
               
                BaseGen.symbolStack.Push("pawn", resolveParams);
            }
        }
    }
}
