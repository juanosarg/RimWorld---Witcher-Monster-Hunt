using RimWorld;
using RimWorld.Planet;
using Verse;
using RimWorld.QuestGen;
using System.Collections.Generic;

namespace WMHAnimalBehaviours
{



    public class WorldComponent_MonsterQuests : WorldComponent
    {
   
        public int tickCounterHunt;
        public int ticksToNextHuntQuest = 60000 * 15;


        public WorldComponent_MonsterQuests(World world) : base(world)
        {
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();



        }

        public override void WorldComponentTick()
        {
            base.WorldComponentTick();



           
            if (tickCounterHunt > ticksToNextHuntQuest)
            {
                List<QuestScriptDef> questList = new List<QuestScriptDef>() { InternalDefOf.WMH_Wraith_MonsterEncounterQuest, InternalDefOf.WMH_Werewolf_MonsterEncounterQuest, InternalDefOf.WMH_Nekker_MonsterEncounterQuest, InternalDefOf.WMH_Leshy_MonsterEncounterQuest,
        InternalDefOf.WMH_Kikimore_MonsterEncounterQuest, InternalDefOf.WMH_Hym_MonsterEncounterQuest, InternalDefOf.WMH_Golem_MonsterEncounterQuest, InternalDefOf.WMH_Ghoul_MonsterEncounterQuest, InternalDefOf.WMH_Fogler_MonsterEncounterQuest, 
        InternalDefOf.WMH_Fleder_MonsterEncounterQuest, InternalDefOf.WMH_Fiend_MonsterEncounterQuest, InternalDefOf.WMH_Ekimmara_MonsterEncounterQuest, InternalDefOf.WMH_Djinn_MonsterEncounterQuest, InternalDefOf.WMH_Cyclops_MonsterEncounterQuest,
        InternalDefOf.WMH_Chort_MonsterEncounterQuest, InternalDefOf.WMH_Bearofleet_MonsterEncounterQuest, InternalDefOf.WMH_Wyvern_MonsterEncounterQuest, InternalDefOf.WMH_Basilisk_MonsterEncounterQuest};

                Slate slate = new Slate();
                Quest quest = QuestUtility.GenerateQuestAndMakeAvailable(questList.RandomElement(), slate);

                QuestUtility.SendLetterQuestAvailable(quest);
                ticksToNextHuntQuest = (int)(60000 * Rand.RangeInclusive(20, 40) * WMH_Mod.settings.WMH_QuestMultiplier);
                tickCounterHunt = 0;




            }
            tickCounterHunt++;





        }

        public override void ExposeData()
        {
            base.ExposeData();
          
            Scribe_Values.Look(ref this.tickCounterHunt, nameof(this.tickCounterHunt));
          
            Scribe_Values.Look(ref this.ticksToNextHuntQuest, nameof(this.ticksToNextHuntQuest));
        }
    }
}
