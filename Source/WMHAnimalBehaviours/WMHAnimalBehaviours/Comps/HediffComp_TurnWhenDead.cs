using RimWorld;
using Verse;
using Verse.Sound;
using System;

namespace WMHAnimalBehaviours
{
    public class HediffComp_TurnWhenDead : HediffComp
    {
       

        public HediffCompProperties_TurnWhenDead Props
        {
            get
            {
                return (HediffCompProperties_TurnWhenDead)this.props;
            }
        }

        public override void Notify_PawnDied()
        {
            //base.Notify_PawnDied();
            if (this.parent.pawn.Corpse.Map != null && this.parent.Severity>0.85) {
                Gender oldGender = this.parent.pawn.gender;
                PawnGenerationRequest request = new PawnGenerationRequest(PawnKindDef.Named(Props.thingToTurnTo), Find.FactionManager.FirstFactionOfDef(FactionDef.Named("WMH_Monsters")), PawnGenerationContext.NonPlayer, -1, false, true, false, false, true, false, 1f, false, true, true, false, false);
                Pawn pawn = PawnGenerator.GeneratePawn(request);
                PawnUtility.TrySpawnHatchedOrBornPawn(pawn, this.parent.pawn.Corpse);
                pawn.gender = oldGender;
                pawn.mindState.mentalStateHandler.TryStartMentalState(DefDatabase<MentalStateDef>.GetNamed("ManhunterPermanent", true), null, true, false, null, false);

                for (int i = 0; i < 20; i++)
                {
                    IntVec3 c;
                    CellFinder.TryFindRandomReachableCellNear(this.parent.pawn.Corpse.Position, this.parent.pawn.Corpse.Map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);
                   
                    FilthMaker.MakeFilth(c, this.parent.pawn.Corpse.Map, ThingDefOf.Filth_Blood);
                    
                }
                
               
                SoundDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(this.parent.pawn.Corpse.Position, this.parent.pawn.Corpse.Map, false));
                this.parent.pawn.Corpse.Destroy();

            }

        }

       

       /* public override void CompPostTick(ref float severityAdjustment)
        {
            position = this.parent.pawn.Position;
            map = this.parent.pawn.Map;
        }*/
    }
}
