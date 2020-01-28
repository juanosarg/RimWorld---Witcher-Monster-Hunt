using RimWorld;
using UnityEngine;
using Verse;

namespace WMHAnimalBehaviours
{
    

   
    
        public class WMH_Settings : ModSettings

        {

            public static bool WMH_DisableEvents = false;
            public static bool WMH_GloryMode = false;








        public override void ExposeData()
            {
                base.ExposeData();
               
                Scribe_Values.Look(ref WMH_DisableEvents, "WMH_DisableEvents", false, true);
                Scribe_Values.Look(ref WMH_GloryMode, "WMH_GloryMode", false, true);






        }

        public static void DoWindowContents(Rect inRect)
            {
                Listing_Standard ls = new Listing_Standard();


                ls.Begin(inRect);
                ls.Gap(12f);
               

                ls.CheckboxLabeled("WMH_DisableEvents".Translate(), ref WMH_DisableEvents, null);
                ls.Gap(12f);
                ls.CheckboxLabeled("WMH_GloryMode".Translate(), ref WMH_GloryMode, null);

                ls.End();
            }



        }










    }

