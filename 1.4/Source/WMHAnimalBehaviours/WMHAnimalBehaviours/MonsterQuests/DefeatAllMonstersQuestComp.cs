﻿using System;
using System.Collections.Generic;
using Verse;
using RimWorld;
using RimWorld.Planet;


namespace WMHAnimalBehaviours
{
    public class DefeatAllMonstersQuestComp : WorldObjectComp, IThingHolder
    {
        private bool active;

        public Faction requestingFaction;

        public int relationsImprovement;

        public ThingOwner rewards;

        private static List<Thing> tmpRewards = new List<Thing>();

        public bool Active
        {
            get
            {
                return this.active;
            }
        }

        public DefeatAllMonstersQuestComp()
        {
            this.rewards = new ThingOwner<Thing>(this);
        }

        public void StartQuest(Faction requestingFaction, int relationsImprovement, List<Thing> rewards)
        {
            this.StopQuest();
            this.active = true;
            this.requestingFaction = requestingFaction;
            this.relationsImprovement = relationsImprovement;
            this.rewards.ClearAndDestroyContents(DestroyMode.Vanish);
            this.rewards.TryAddRangeOrTransfer(rewards, true, false);
        }

        public void StopQuest()
        {
            this.active = false;
            this.requestingFaction = null;
            this.rewards.ClearAndDestroyContents(DestroyMode.Vanish);
        }

        public override void CompTick()
        {
            base.CompTick();
            if (this.active)
            {
                MapParent mapParent = this.parent as MapParent;
                if (mapParent != null)
                {
                    this.CheckAllEnemiesDefeated(mapParent);
                }
            }
        }

        private void CheckAllEnemiesDefeated(MapParent mapParent)
        {
            if (!mapParent.HasMap)
            {
                return;
            }
            if (GenHostility.AnyHostileActiveThreatToPlayer(mapParent.Map))
            {
                return;
            }
            this.GiveRewardsAndSendLetter();
            this.StopQuest();
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<bool>(ref this.active, "active", false, false);
            Scribe_Values.Look<int>(ref this.relationsImprovement, "relationsImprovement", 0, false);
            Scribe_References.Look<Faction>(ref this.requestingFaction, "requestingFaction", false);
            Scribe_Deep.Look<ThingOwner>(ref this.rewards, "rewards", new object[]
            {
                this
            });
        }

        private void GiveRewardsAndSendLetter()
        {
            Map map = Find.AnyPlayerHomeMap ?? ((MapParent)this.parent).Map;
            DefeatAllMonstersQuestComp.tmpRewards.AddRange(this.rewards);
            this.rewards.Clear();
            IntVec3 intVec = DropCellFinder.TradeDropSpot(map);
            DropPodUtility.DropThingsNear(intVec, map, DefeatAllMonstersQuestComp.tmpRewards, 110, false, false, false);
            DefeatAllMonstersQuestComp.tmpRewards.Clear();
            FactionRelationKind playerRelationKind = this.requestingFaction.PlayerRelationKind;
            TaggedString text = "WMH_LetterDefeatAllMonstersQuestCompleted".Translate(this.requestingFaction.Name, this.relationsImprovement.ToString());
            this.requestingFaction.TryAffectGoodwillWith(Faction.OfPlayer, this.relationsImprovement, false, false, null, null);
            this.requestingFaction.TryAppendRelationKindChangedInfo(ref text, playerRelationKind, this.requestingFaction.PlayerRelationKind, null);
            Find.LetterStack.ReceiveLetter("WMH_LetterLabelDefeatAllMonstersQuestCompleted".Translate(), text, LetterDefOf.PositiveEvent, new GlobalTargetInfo(intVec, map, false), this.requestingFaction, null);
        }

        public void GetChildHolders(List<IThingHolder> outChildren)
        {
            ThingOwnerUtility.AppendThingHoldersFromThings(outChildren, this.GetDirectlyHeldThings());
        }

        public ThingOwner GetDirectlyHeldThings()
        {
            return this.rewards;
        }

        public override void PostPostRemove()
        {
            base.PostPostRemove();
            this.rewards.ClearAndDestroyContents(DestroyMode.Vanish);
        }

        public override string CompInspectStringExtra()
        {
            if (this.active)
            {
                string value = GenThing.ThingsToCommaList(this.rewards, true, true, 5).CapitalizeFirst();
                return "QuestTargetDestroyInspectString".Translate(this.requestingFaction.Name, value, GenThing.GetMarketValue(this.rewards).ToStringMoney(null)).CapitalizeFirst();
            }
            return null;
        }
    }
}
