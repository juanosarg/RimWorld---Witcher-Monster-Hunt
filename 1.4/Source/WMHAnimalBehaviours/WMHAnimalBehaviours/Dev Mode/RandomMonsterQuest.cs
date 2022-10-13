using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RimWorld;
using Verse.AI.Group;
using Verse;
using RimWorld.QuestGen;

namespace WMHAnimalBehaviours
{
	public static class RandomMonsterQuest
	{


		[DebugAction("RimWorld - Witcher Monster Hunt", "Generate random monster quest", false, false, allowedGameStates = AllowedGameStates.PlayingOnMap)]
		private static void GenerateRandomMonsterQuest()
		{
			List<QuestScriptDef> questList = new List<QuestScriptDef>() { InternalDefOf.WMH_Wraith_MonsterEncounterQuest, InternalDefOf.WMH_Werewolf_MonsterEncounterQuest, InternalDefOf.WMH_Nekker_MonsterEncounterQuest, InternalDefOf.WMH_Leshy_MonsterEncounterQuest,
		InternalDefOf.WMH_Kikimore_MonsterEncounterQuest, InternalDefOf.WMH_Hym_MonsterEncounterQuest, InternalDefOf.WMH_Golem_MonsterEncounterQuest, InternalDefOf.WMH_Ghoul_MonsterEncounterQuest, InternalDefOf.WMH_Fogler_MonsterEncounterQuest,
		InternalDefOf.WMH_Fleder_MonsterEncounterQuest, InternalDefOf.WMH_Fiend_MonsterEncounterQuest, InternalDefOf.WMH_Ekimmara_MonsterEncounterQuest, InternalDefOf.WMH_Djinn_MonsterEncounterQuest, InternalDefOf.WMH_Cyclops_MonsterEncounterQuest,
		InternalDefOf.WMH_Chort_MonsterEncounterQuest, InternalDefOf.WMH_Bearofleet_MonsterEncounterQuest, InternalDefOf.WMH_Wyvern_MonsterEncounterQuest, InternalDefOf.WMH_Basilisk_MonsterEncounterQuest};
			Slate slate = new Slate();
			Quest quest = QuestUtility.GenerateQuestAndMakeAvailable(questList.RandomElement(), slate);

			QuestUtility.SendLetterQuestAvailable(quest);
		}




	}
}

