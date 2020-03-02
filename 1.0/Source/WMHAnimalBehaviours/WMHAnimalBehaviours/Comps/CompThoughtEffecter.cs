using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;
using UnityEngine;
using System.Collections;

namespace WMHAnimalBehaviours
{
    public class CompThoughtEffecter : ThingComp
    {
        public int tickCounter = 0;
        public List<Pawn> pawnList = new List<Pawn>();
        public Pawn thisPawn;


        public CompProperties_ThoughtEffecter Props
        {
            get
            {
                return (CompProperties_ThoughtEffecter)this.props;
            }
        }




        public override void CompTick()
        {
            base.CompTick();
            tickCounter++;
            if (tickCounter > Props.tickInterval)
            {
                thisPawn = this.parent as Pawn;
                if (thisPawn != null && thisPawn.Map != null && !thisPawn.Dead && !thisPawn.Downed)
                {
                    List<Pawn> allPawnsSpawned = thisPawn.Map.mapPawns.AllPawnsSpawned;

                    for (int k = 0; k < allPawnsSpawned.Count; k++)
                    {
                        if (allPawnsSpawned[k] != null && allPawnsSpawned[k].IsColonist)
                        {
                            pawnList.Add(allPawnsSpawned[k]);
                        }
                    }

                    if (pawnList.Count > 0)
                    {
                        IntVec3 thisPawnLocation = thisPawn.Position;
                        List<Pawn> tempList = new List<Pawn>();
                        for (int k = 0; k < pawnList.Count; k++)
                        {
                            if (IntVec3Utility.ManhattanDistanceFlat(thisPawnLocation, pawnList[k].Position) < Props.radius)
                            {
                                tempList.Add(pawnList[k]);
                            }
                        }

                        if (tempList.Count > 0)
                        {
                            Pawn chosenOne = tempList.RandomElement();
                            if (chosenOne != null)
                            {

                                if (!chosenOne.Dead && !chosenOne.Downed && chosenOne.GetStatValue(StatDefOf.PsychicSensitivity, true) > 0f)
                                {
                                    Find.TickManager.slower.SignalForceNormalSpeedShort();

                                    chosenOne.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named(Props.thoughtDef), null);
                                }

                            }
                        }

                        tempList.Clear();


                    }


                }
                pawnList.Clear();
                tickCounter = 0;
            }
        }


    }
}

