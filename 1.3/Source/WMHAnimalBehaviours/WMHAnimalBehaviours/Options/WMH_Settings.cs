using RimWorld;
using UnityEngine;
using Verse;
using System;
using System.Collections.Generic;
using System.Linq;


namespace WMHAnimalBehaviours
{
    public class WMH_Settings : ModSettings

    {



        public const float WMH_QuestMultiplierBase = 1;
        public float WMH_QuestMultiplier = WMH_QuestMultiplierBase;

      

        private static Vector2 scrollPosition = Vector2.zero;





        public override void ExposeData()
        {
            base.ExposeData();


            Scribe_Values.Look(ref WMH_QuestMultiplier, "WMH_QuestMultiplier", WMH_QuestMultiplierBase);
          

        }
        public void DoWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();

            var scrollContainer = inRect.ContractedBy(10);
            scrollContainer.height -= listingStandard.CurHeight;
            scrollContainer.y += listingStandard.CurHeight;
            Widgets.DrawBoxSolid(scrollContainer, Color.grey);
            var innerContainer = scrollContainer.ContractedBy(1);
            Widgets.DrawBoxSolid(innerContainer, new ColorInt(42, 43, 44).ToColor);
            var frameRect = innerContainer.ContractedBy(5);
            frameRect.y += 15;
            frameRect.height -= 15;
            var contentRect = frameRect;
            contentRect.x = 0;
            contentRect.y = 0;
            contentRect.width -= 20;




            contentRect.height = 350f;


            Widgets.BeginScrollView(frameRect, ref scrollPosition, contentRect, true);
            listingStandard.Begin(contentRect.AtZero());





            listingStandard.GapLine();


            var questRateLabel = listingStandard.LabelPlusButton("WMH_QuestMultiplier".Translate() + ": " + WMH_QuestMultiplier, "WMH_QuestMultiplierTooltip".Translate());
            WMH_QuestMultiplier = (float)Math.Round(listingStandard.Slider(WMH_QuestMultiplier, 0.1f, 5f), 1);
            if (listingStandard.Settings_Button("WMH_Reset".Translate(), new Rect(0f, questRateLabel.position.y + 35, 180f, 29f)))
            {
                WMH_QuestMultiplier = WMH_QuestMultiplierBase;
            }

            listingStandard.End();
            Widgets.EndScrollView();

            base.Write();

        }




    }










}
