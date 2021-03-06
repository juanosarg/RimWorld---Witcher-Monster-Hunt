﻿using Verse;
using System.Collections.Generic;
using System.Linq;
using System;
using RimWorld;
using RimWorld.Planet;
using RimWorld.QuestGen;

namespace WMHAnimalBehaviours
{
    public class IncidentWorker_MonsterEncounter : IncidentWorker
    {
        private const int MinDistance = 5;

        private const int MaxDistance = 10;

        private static readonly IntRange TimeoutDaysRange = new IntRange(10, 24);

       

        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }
            int num;
            return GetHostingFaction() != null && this.TryFindTile(out num);
        }

        private bool TryFindTile(out int tile)
        {
            return TileFinder.TryFindNewSiteTile(out tile, MinDistance, MaxDistance);
        }

        public static Site MakeSite(int tile, int timeOutDays, Faction faction, PawnKindDef animalKind)
        {
            Site site = (Site)WorldObjectMaker.MakeWorldObject(DefDatabase<WorldObjectDef>.GetNamed(animalKind.defName+"_MonsterEncounterWorldObject", true));
            site.Tile = tile;
           
            site.parts.Add( new SitePart(site,DefDatabase<SitePartDef>.GetNamed(animalKind.defName + "_MonsterEncounterCore", true), DefDatabase<SitePartDef>.GetNamed(animalKind.defName + "_MonsterEncounterCore", true).Worker.GenerateDefaultParams(StorytellerUtility.DefaultSiteThreatPointsNow(),tile,faction)));
            site.SetFaction(faction);
            site.desiredThreatPoints = StorytellerUtility.DefaultSiteThreatPointsNow();
            //Log.Message(site.desiredThreatPoints.ToString());

            site.GetComponent<TimeoutComp>().StartTimeout(timeOutDays * 60000);
            site.GetComponent<WorldObjectComp_MonsterToHunt>().monsterKindDef = animalKind;
            Find.WorldObjects.Add(site);
            return site;
        }

        public Faction GetHostingFaction()
        {
            return Find.FactionManager.RandomNonHostileFaction();
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Faction faction = GetHostingFaction();
            if (faction == null)
            {
                return false;
            }
            if (faction.leader == null)
            {
                return false;
            }
            int tile;
            if (!this.TryFindTile(out tile))
            {
                return false;
            }
            int timeOutDays = TimeoutDaysRange.RandomInRange;
            //int fee = CalculateFee(faction);
            IEnumerable<PawnKindDef> animalList = (from k in DefDatabase<PawnKindDef>.AllDefs
                                                   where k.defName.Contains("WMH_")
                                                   select k);

            if (animalList.Count() < 1)
            {
                return false;
            }
            
            PawnKindDef huntingTarget = animalList.RandomElement();

            Random random = new Random();
            

            Site site = MakeSite(tile, 20, faction, huntingTarget);
            site.sitePartsKnown = true;
            List<Thing> list = GenerateRewards(faction, site.desiredThreatPoints, huntingTarget);
            
            site.GetComponent<DefeatAllMonstersQuestComp>().StartQuest(faction, 20, list);
            string text;
            string label;
            this.GetLetterText(faction, huntingTarget, GenLabel.ThingsLabel(list, string.Empty),GenThing.GetMarketValue(list).ToStringMoney(null),out text, out label);
            Find.LetterStack.ReceiveLetter(label, text, this.def.letterDef,site,faction);

            return true;
        }

        private List<Thing> GenerateRewards(Faction alliedFaction, float siteThreatPoints, PawnKindDef huntingTarget)
        {
            float relictModifier = 1;
            MonsterClassEnum monsterClass = MonsterClassEnum.CursedOne;
            var extendedRaceProps = huntingTarget.race.GetModExtension<MonsterClass>();
            if (extendedRaceProps != null)
            {
                monsterClass = extendedRaceProps.monsterClass;
            }
            if (monsterClass == MonsterClassEnum.Relict)
            {
                relictModifier = 1.5f;
            }
          
            ThingSetMakerParams parms = default(ThingSetMakerParams);
            FloatRange floatRange = new FloatRange(1750,3500);
            parms.totalMarketValueRange = floatRange * relictModifier;
            
           
             
            return DefDatabase<ThingSetMakerDef>.GetNamed("WMH_Reward_Monster", true).root.Generate(parms);
        }

        private void GetLetterText(Faction alliedFaction, PawnKindDef animalDef, string things,string money, out string letter, out string label)
        {
            letter = string.Format(base.def.letterText, alliedFaction.leader.LabelShort, alliedFaction.def.leaderTitle, alliedFaction.Name, (animalDef.defName+"_LetterText").Translate(), money,things);
            label = this.def.letterLabel;


        }
    }
}
