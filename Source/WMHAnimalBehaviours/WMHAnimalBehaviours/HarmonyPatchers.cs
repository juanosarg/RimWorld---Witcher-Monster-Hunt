using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using System.Reflection;
using System.Reflection.Emit;
using Harmony;

namespace WMHAnimalBehaviours
{

    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = HarmonyInstance.Create("com.witchermonsterhunt");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }


    }


    public static class Patch_ThingDef
    {

        [HarmonyPatch(typeof(ThingDef))]
        [HarmonyPatch(nameof(ThingDef.SpecialDisplayStats))]
        public static class Patch_SpecialDisplayStats
        {

            public static void Postfix(ThingDef __instance, ref IEnumerable<StatDrawEntry> __result)
            {
                Log.Message(__instance.defName);
                if (__instance.defName.Contains("WMH_"))
                {
                    Log.Message(__instance.defName + " Detected");
                    MonsterClassEnum monsterClass = MonsterClassEnum.CursedOne;
                    var extendedRaceProps = __instance.GetModExtension<MonsterClass>();
                    if (extendedRaceProps != null)
                        monsterClass = extendedRaceProps.monsterClass;
                    __result = __result.Add(new StatDrawEntry(StatCategoryDefOf.BasicsPawn, "WMH_MonsterClass".Translate(), $"WMH_MonsterClass_{monsterClass}".Translate(),
                        overrideReportText: $"WMH_MonsterClass_{monsterClass}_Description".Translate()));
                }
            }

        }

    }


    /*This Harmony Postfix changes terrain calculation for floating creatures
  */
    [HarmonyPatch(typeof(Verse.AI.Pawn_PathFollower))]
    [HarmonyPatch("CostToMoveIntoCell")]
    [HarmonyPatch(new Type[] { typeof(Pawn), typeof(IntVec3) })]
    public static class Pawn_PathFollower_CostToMoveIntoCell_Patch
    {
        [HarmonyPostfix]
        public static void MakeHoveringCreaturesGreatAgain(Pawn pawn, IntVec3 c, ref int __result)

        {
            if ((pawn.Map != null) && (pawn.TryGetComp<CompHovering>() != null))
            {
                if (pawn.TryGetComp<CompHovering>().Props.isFloater)
                {
                    int num;
                    if (c.x == pawn.Position.x || c.z == pawn.Position.z)
                    {
                        num = pawn.TicksPerMoveCardinal;
                    }
                    else
                    {
                        num = pawn.TicksPerMoveDiagonal;
                    }
                    TerrainDef terrainDef = pawn.Map.terrainGrid.TerrainAt(c);
                    if (terrainDef == null)
                    {
                        num = 10000;
                    }
                    else if ((terrainDef.passability == Traversability.Impassable) && !terrainDef.IsWater)
                    {
                        num = 10000;
                    }
                    else if (terrainDef.IsWater && !pawn.TryGetComp<CompHovering>().Props.canCrossWater)
                    {
                        num = 10000;
                    }
                    List<Thing> list = pawn.Map.thingGrid.ThingsListAt(c);
                    for (int i = 0; i < list.Count; i++)
                    {
                        Thing thing = list[i];
                        if (thing.def.passability == Traversability.Impassable)
                        {
                            num = 10000;
                        }

                        if (thing is Building_Door)
                        {
                            num += 45;
                        }
                    }

                    __result = num;

                }

            }




        }
    }


}
