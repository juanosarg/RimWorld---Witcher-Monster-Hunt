using RimWorld;
using Verse;

namespace WMHAnimalBehaviours
{
    public class HediffComp_TurnWhenDead : HediffComp
    {
        Map map;
        IntVec3 position;


        public HediffCompProperties_TurnWhenDead Props
        {
            get
            {
                return (HediffCompProperties_TurnWhenDead)this.props;
            }
        }

        public override void Notify_PawnDied()
        {
            base.Notify_PawnDied();
            PawnGenerationRequest request = new PawnGenerationRequest(PawnKindDef.Named(Props.thingToTurnTo), null, PawnGenerationContext.NonPlayer, -1, false, true, false, false, true, false, 1f, false, true, true, false, false);
            Pawn pawn = PawnGenerator.GeneratePawn(request);
            PawnUtility.TrySpawnHatchedOrBornPawn(pawn, this.parent.pawn.Corpse);
            
        }

       

       /* public override void CompPostTick(ref float severityAdjustment)
        {
            position = this.parent.pawn.Position;
            map = this.parent.pawn.Map;
        }*/
    }
}
