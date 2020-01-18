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

    public class GenStep_MonsterEncounter_Basilisk : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Basilisk_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Bearofleet : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Bearofleet_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Chort : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Chort_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Cyclops : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Cyclops_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Djinn : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Djinn_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Ekimmara : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Ekimmara_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Fiend : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Fiend_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Fleder : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Fleder_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Fogler : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Fogler_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Ghoul : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Ghoul_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Golem : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Golem_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Hym : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Hym_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_KikimoreQueen : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_KikimoreQueen_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Leshy : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Leshy_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Nekker : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Nekker_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Werewolf : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Werewolf_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Wraith : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Wraith_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

    public class GenStep_MonsterEncounter_Wyvern : GenStep_BasicGenWMH
    {
        public const int edgeSize = 40;

        public override void Generate(Map map, GenStepParams parms)
        {
            PawnKindDef huntingKind = Find.WorldObjects.WorldObjectAt(map.Tile, DefDatabase<WorldObjectDef>.GetNamed("WMH_Wyvern_MonsterEncounterWorldObject", true)).GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef;
            base.Generate(map, parms);
            CellRect rect = new CellRect(Rand.RangeInclusive(this.adventureRegion.minX + 10, this.adventureRegion.maxX - (edgeSize + 10)), Rand.RangeInclusive(this.adventureRegion.minZ + 10, this.adventureRegion.maxZ - (edgeSize + 10)), edgeSize, edgeSize);
            rect.ClipInsideMap(map);
            ResolveParams animalResolveParams = this.baseResolveParams;
            animalResolveParams.rect = rect;
            animalResolveParams.singlePawnKindDef = huntingKind;
            BaseGen.symbolStack.Push("WMH_SpawnHuntingMonstersSymbol", animalResolveParams);
            BaseGen.Generate();
        }
    }

   
}