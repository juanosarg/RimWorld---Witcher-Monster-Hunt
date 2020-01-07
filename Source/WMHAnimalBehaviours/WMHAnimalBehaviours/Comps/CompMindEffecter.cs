using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;
using UnityEngine;
using System.Collections;

namespace WMHAnimalBehaviours
{
    public class CompMindEffecter : ThingComp
    {
        public int tickCounter = 0;
        public List<Pawn> pawnList = new List<Pawn>();
        public Pawn thisPawn;


        public CompProperties_MindEffecter Props
        {
            get
            {
                return (CompProperties_MindEffecter)this.props;
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
                    
                      if (pawnList.Count > 0) {
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

                                  if (!chosenOne.Dead && !chosenOne.Downed)
                                  {
                                    Find.TickManager.slower.SignalForceNormalSpeedShort();

                                    chosenOne.mindState.mentalStateHandler.TryStartMentalState(DefDatabase<MentalStateDef>.GetNamed(Props.mentalState, true), null, true, false, null, false);
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

