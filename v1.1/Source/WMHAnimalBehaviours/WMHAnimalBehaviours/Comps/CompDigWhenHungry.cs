using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;

namespace WMHAnimalBehaviours
{
    public class CompDigWhenHungry : ThingComp
    {
        public int stopdiggingcounter = 0;
        private Effecter effecter;


        public CompProperties_DigWhenHungry Props
        {
            get
            {
                return (CompProperties_DigWhenHungry)this.props;
            }
        }


       

        public override void CompTick()
        {
            base.CompTick();
            Pawn pawn = this.parent as Pawn;
            if ((pawn.Map != null) && (pawn.needs.food.CurLevelPercentage < pawn.needs.food.PercentageThreshHungry) && (pawn.Awake()))
            {

                if (stopdiggingcounter <= 0)
                {

                    ThingDef newThing = ThingDef.Named(this.Props.customThingToDig);
                    Thing newcorpse = GenSpawn.Spawn(newThing, pawn.Position, pawn.Map, WipeMode.Vanish);
                    newcorpse.stackCount = this.Props.customAmountToDig;
                    if (this.effecter == null)
                    {
                        this.effecter = EffecterDefOf.Mine.Spawn();
                    }
                    this.effecter.Trigger(pawn, newcorpse);


                    stopdiggingcounter = 40000;
                }
                stopdiggingcounter--;

            }
        }


    }
}

