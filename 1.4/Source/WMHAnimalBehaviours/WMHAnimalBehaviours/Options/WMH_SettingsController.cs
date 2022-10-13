using RimWorld;
using UnityEngine;
using Verse;


namespace WMHAnimalBehaviours
{




    public class WMH_Mod : Mod
    {
        public static WMH_Settings settings;

        public WMH_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<WMH_Settings>();
        }
        public override string SettingsCategory() => "RimWorld - Witcher Monster Hunt";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoWindowContents(inRect);
        }





    }
}

