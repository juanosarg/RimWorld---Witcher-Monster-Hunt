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


  



}
