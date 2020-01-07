
using Verse;

namespace WMHAnimalBehaviours
{
    public class CompProperties_DigWhenHungry : CompProperties
    {


        public string customThingToDig = "";
        public int customAmountToDig = 1;


        public CompProperties_DigWhenHungry()
        {
            this.compClass = typeof(CompDigWhenHungry);
        }


    }
}
