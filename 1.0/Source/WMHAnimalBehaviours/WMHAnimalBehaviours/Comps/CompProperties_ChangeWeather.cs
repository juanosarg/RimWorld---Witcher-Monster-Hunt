
using Verse;

namespace WMHAnimalBehaviours
{
    public class CompProperties_ChangeWeather : CompProperties
    {


        public string weatherDef = "Fog";
       

        public CompProperties_ChangeWeather()
        {
            this.compClass = typeof(CompChangeWeather);
        }


    }
}
