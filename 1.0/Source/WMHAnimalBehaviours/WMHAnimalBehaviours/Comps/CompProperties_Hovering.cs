using Verse;


namespace WMHAnimalBehaviours
{


    public class CompProperties_Hovering : CompProperties
    {
        public bool isFloater = false;
        public bool canCrossWater = false;

        public CompProperties_Hovering()
        {
            this.compClass = typeof(CompHovering);
        }

    }
}
