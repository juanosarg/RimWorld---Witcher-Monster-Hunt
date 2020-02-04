using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;
using RimWorld.Planet;

namespace WMHAnimalBehaviours
{
    public class MapComponentExtender : MapComponent
    {
        public PawnKindDef monsterKindDef = null;
        public bool verifyFirstTime = true;
        public int spawnCounter = 0;

        public MapComponentExtender(Map map) : base(map)
        {

        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<bool>(ref this.verifyFirstTime, "verifyFirstTime", true, true);


        }

        public override void FinalizeInit()
        {

            base.FinalizeInit();

            if (verifyFirstTime)
            {
                this.doMapSpawns();
               

            }

        }

        public void doMapSpawns()
        {

            IEnumerable<IntVec3> tmpTerrain = map.AllCells.InRandomOrder();
            
            if (monsterKindDef != null)
            {
                //Log.Message(monsterKindDef.ToString());
                foreach (MonsterMapSpawnsDef element in DefDatabase<MonsterMapSpawnsDef>.AllDefs.Where(element => element.associatedMonster == this.monsterKindDef))
                {
                    bool canSpawn = true;
                    if (spawnCounter == 0)
                    {
                        spawnCounter = element.numberToSpawn;
                    }
                    foreach (IntVec3 c in tmpTerrain)
                    {

                        TerrainDef terrain = c.GetTerrain(map);

                      
                       

                        if (canSpawn)
                        {

                            Thing thing = (Thing)ThingMaker.MakeThing(element.thingDef, null);
                            CellRect occupiedRect = GenAdj.OccupiedRect(c, thing.Rotation, thing.def.Size);
                            if (occupiedRect.InBounds(map))
                            {
                                GenSpawn.Spawn(thing, c, map);
                                spawnCounter--;
                            }

                        }
                        if (canSpawn && spawnCounter <= 0)
                        {
                            spawnCounter = 0;
                            break;
                        }
                    }


                }



            }



            this.verifyFirstTime = false;

        }

    }
}
