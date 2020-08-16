using System;
using Verse;

namespace WMHAnimalBehaviours
{
    public class HediffCompProperties_TurnWhenDead : HediffCompProperties
    {
        public string thingToTurnTo = "";

        public HediffCompProperties_TurnWhenDead()
        {
            this.compClass = typeof(HediffComp_TurnWhenDead);
        }
    }
}
