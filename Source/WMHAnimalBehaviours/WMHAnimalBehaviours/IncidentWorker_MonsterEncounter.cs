using Verse;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.Planet;

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
            return TileFinder.TryFindNewSiteTile(out tile, MinDistance, MaxDistance, false, false, -1);
        }

        public static Site MakeSite(int tile, int timeOutDays, Faction faction, PawnKindDef animalKind)
        {
            Site site = (Site)WorldObjectMaker.MakeWorldObject(DefDatabase<WorldObjectDef>.GetNamed("WMH_MonsterEncounterWorldObject", true));
            site.Tile = tile;

            site.core = new SiteCore(DefDatabase<SiteCoreDef>.GetNamed("WMH_MonsterEncounterCore", true), DefDatabase<SiteCoreDef>.GetNamed("WMH_MonsterEncounterCore", true).Worker.GenerateDefaultParams(site, StorytellerUtility.DefaultSiteThreatPointsNow()));
            site.SetFaction(faction);
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
            IEnumerable<PawnKindDef> animalList = (from k in Find.WorldGrid[tile].biome.AllWildAnimals
                                                   where Find.World.tileTemperatures.SeasonAndOutdoorTemperatureAcceptableFor(tile, k.race)
                                                   select k);
            if (animalList.Count() < 1)
            {
                return false;
            }
            PawnKindDef huntingTarget = animalList.RandomElement();
            Site site = MakeSite(tile, 20, faction, huntingTarget);
            string text;
            string label;
            this.GetLetterText(faction, huntingTarget, out text, out label);
            Find.LetterStack.ReceiveLetter(label, text, this.def.letterDef,site,faction);

            return true;
        }

        private void GetLetterText(Faction alliedFaction, PawnKindDef animalDef, out string letter, out string label)
        {
            letter = string.Format(base.def.letterText, alliedFaction.leader.LabelShort, alliedFaction.def.leaderTitle, alliedFaction.Name, animalDef.GetLabelPlural(-1));
            label = this.def.letterLabel;


        }
    }
}
