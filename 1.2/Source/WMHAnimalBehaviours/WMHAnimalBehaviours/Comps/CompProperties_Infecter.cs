
using Verse;

namespace WMHAnimalBehaviours
{
    public class CompProperties_Infecter : CompProperties
    {


        public int infectionChance = 10;
       
        public CompProperties_Infecter()
        {
            this.compClass = typeof(CompInfecter);
        }


    }
}
