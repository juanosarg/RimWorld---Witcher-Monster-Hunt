using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace WMHAnimalBehaviours
{

    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.witchermonsterhunt");
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
                //Log.Message(__instance.defName);
                if (   ((__instance.defName.Contains("WMH_")) || (__instance.defName.Contains("WMAux_")))&& !__instance.IsCorpse)
                {
                    
                        // Log.Message(__instance.defName + " Detected");
                        MonsterClassEnum monsterClass = MonsterClassEnum.CursedOne;
                        var extendedRaceProps = __instance.GetModExtension<MonsterClass>();
                        if (extendedRaceProps != null)
                            monsterClass = extendedRaceProps.monsterClass;
                        __result = __result.AddItem(new StatDrawEntry(StatCategoryDefOf.BasicsPawn, "WMH_MonsterClass".Translate(), $"WMH_MonsterClass_{monsterClass}".Translate(),
                            $"WMH_MonsterClass_{monsterClass}_Description".Translate(),1));

                   
                   
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

    /*This Harmony Postfix makes the golem drop rock chunks in Glory Mode
*/
    [HarmonyPatch(typeof(Pawn_HealthTracker))]
    [HarmonyPatch("DropBloodFilth")]
   
    public static class Pawn_HealthTracker_DropBloodFilth_Patch
    {
        [HarmonyPostfix]
        public static void MakeGolemDropRockChunksInGloryMode(Pawn_HealthTracker __instance)

        {
            Pawn pawn = ((Pawn)typeof(Pawn_HealthTracker).GetField("pawn", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance));

            if (WMH_Settings.WMH_GloryMode && pawn.def.defName == "WMH_Golem")
            {
                if ((pawn.Spawned || pawn.ParentHolder is Pawn_CarryTracker) && pawn.SpawnedOrAnyParentSpawned)
                {
                    Thing thing3 = ThingMaker.MakeThing(ThingDef.Named("ChunkGranite"), null);
                    GenPlace.TryPlaceThing(thing3, pawn.Position, pawn.Map, ThingPlaceMode.Near, null, null);
                   
                }
            }



        }
    }


}
