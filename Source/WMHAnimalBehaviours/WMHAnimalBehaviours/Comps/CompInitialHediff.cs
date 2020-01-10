
using UnityEngine;
using Verse;
using System.Linq;


namespace WMHAnimalBehaviours
{
    public class CompInitialHediff : ThingComp
    {
        private bool addHediffOnce = true;



        public CompProperties_InitialHediff Props
        {
            get
            {
                return (CompProperties_InitialHediff)this.props;
            }
        }




        public override void CompTickRare()
        {

            base.CompTickRare();

            if (addHediffOnce)
            {
               
                Pawn pawn = this.parent as Pawn;
                pawn.health.AddHediff(HediffDef.Named(Props.hediffname));
                addHediffOnce = false;
            }
        }


    }
}



