using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using RimWorld.BaseGen;

namespace WMHAnimalBehaviours
{

    public class GenStep_BasicGenWMH : GenStep
    {

        protected Dictionary<string, float> randomRoomEvents = new Dictionary<string, float>();

        protected CellRect adventureRegion;

        protected ResolveParams baseResolveParams;

        public override int SeedPart => 44867541;

        public override void Generate(Map map, GenStepParams parms)
        {
            int num = map.Size.x / 10;
            int num2 = 8 * map.Size.x / 10;
            int num3 = map.Size.z / 10;
            int num4 = 8 * map.Size.z / 10;
            this.adventureRegion = new CellRect(num, num3, num2, num4);
            this.adventureRegion.ClipInsideMap(map);
            BaseGen.globalSettings.map = map;
            this.randomRoomEvents.Clear();
            IntVec3 playerStartSpot;
            CellFinder.TryFindRandomEdgeCellWith((IntVec3 v) => GenGrid.Standable(v, map), map, 0f, out playerStartSpot);
            MapGenerator.PlayerStartSpot = playerStartSpot;
            this.baseResolveParams = default(ResolveParams);
            foreach (string current in this.randomRoomEvents.Keys)
            {
                this.baseResolveParams.SetCustom<float>(current, this.randomRoomEvents[current], false);
            }
        }
    }

    public class GenStep_MonsterEncounter : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            //seperate the rect into the herd and lodge half
           /* CellRect animalRect = new CellRect(rect.minX, rect.minZ, edgeSize, edgeSize / 2);
            CellRect lodgeRect = new CellRect(rect.minX, rect.minZ, edgeSize, edgeSize / 2);
            if (Rand.Chance(0.5f))
            {
                animalRect = new CellRect(rect.minX, rect.minZ + edgeSize / 2, edgeSize, edgeSize / 2);
            }
            else
            {
                lodgeRect = new CellRect(rect.minX, rect.minZ + edgeSize / 2, edgeSize, edgeSize / 2);
            }*/

            //Spawn hunting lodge
           /* ResolveParams lodgeResolveParams = this.baseResolveParams;
            lodgeResolveParams.rect = lodgeRect;
            BaseGen.symbolStack.Push("spawnHuntingLodgeSW", lodgeResolveParams);
            */
            //Spawn herd of animals
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);

            BaseGen.Generate();
        }
    }
}