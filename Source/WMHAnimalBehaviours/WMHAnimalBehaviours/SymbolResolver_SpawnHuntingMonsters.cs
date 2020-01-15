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

            bool wildGroupFlag = false;
            if (huntingTarget.wildGroupSize != null)
            {
                if (huntingTarget.wildGroupSize.RandomInRange > 3)
                {
                    wildGroupFlag = true;
                }
            }
            if (wildGroupFlag)
            {
                spawnCount = (huntingTarget.wildGroupSize.RandomInRange * 6 + 4) / 5; //times 1.2 to make herd sizes larger
            }
            else
            {
                if (huntingTarget.combatPower > 102)
                {
                    spawnCount = Rand.RangeInclusive(6, 8);
                }
                else
                {
                    spawnCount = Rand.RangeInclusive(4, 5);
                }
                spawnCount = (int)((float)spawnCount * (float)(210f / huntingTarget.combatPower));
            }

            for (int i = 0; i < spawnCount; i++)
            {
                ResolveParams resolveParams = rp;
                Pawn pawnToSpawn = PawnGenerator.GeneratePawn(huntingTarget, null);
                resolveParams.singlePawnToSpawn = pawnToSpawn;
                BaseGen.symbolStack.Push("pawn", resolveParams);
            }
        }
    }
}
