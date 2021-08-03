using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;
using RimWorld;
using HarmonyLib;

namespace UnusualTraits
{
    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("TryFindBestFoodSourceFor")]
    class TryFindBestFoodSourceFor
    {
        static bool Prefix(
            ref bool __result,
            Pawn getter,
            Pawn eater,
            FoodPreferability minPrefOverride,
            bool desperate,
            bool forceScanWholeMap,
            bool ignoreReservations,
            ref Thing foodSource,
            ref ThingDef foodDef,
            bool canRefillDispenser = true,
            bool allowForbidden = false,
            bool calculateWantedStackCount = false)
        {
            //Skip prefix for non-paste fiends
            if (eater.story == null ||
                !eater.story.traits.allTraits.Where(t =>
                t.def == TraitDefOf.Turn_PasteFiend).Any())
            {
                return true;
            }

            Predicate<Thing> validator = (t =>
                //is a working dispenser AND
                (t is Building_NutrientPasteDispenser dispenser &&
                dispenser.powerComp.PowerOn &&
                (
                //Dispenser has nutrient source OR
                dispenser.HasEnoughFeedstockInHoppers() || canRefillDispenser)
                ) ||
                //Always except loose paste meals
                t.def == RimWorld.ThingDefOf.MealNutrientPaste);

            bool ignoreEntirelyForbiddenRegions = !allowForbidden &&
                ForbidUtility.CaresAboutForbidden(getter, cellTarget: true) &&
                getter.playerSettings != null &&
                getter.playerSettings.EffectiveAreaRestrictionInPawnCurrentMap
                    != null;
            int maxRegions = forceScanWholeMap ? -1 :
                getter.Faction == Faction.OfPlayer ? 100 : 40;
            ThingRequest request = ThingRequest.ForGroup(
                ThingRequestGroup.FoodSource);

            Thing bestFood = GenClosest.ClosestThingReachable(
                getter.Position,
                getter.Map,
                request,
                PathEndMode.OnCell,
                TraverseParms.For(getter),
                validator: validator,
                searchRegionsMax: maxRegions,
                ignoreEntirelyForbiddenRegions: ignoreEntirelyForbiddenRegions
                );
            
            if (bestFood != null)
            {
                foodSource = bestFood;
                foodDef = FoodUtility.GetFinalIngestibleDef(bestFood);
                Log.Message(bestFood.ToString());
                Log.Message(foodDef.ToString());
                __result = true;
                return false;
            }
            else
            {
                request = ThingRequest.ForDef(
                    RimWorld.ThingDefOf.NutrientPasteDispenser);

                bestFood = GenClosest.ClosestThingReachable(
                getter.Position,
                getter.Map,
                request,
                PathEndMode.ClosestTouch,
                TraverseParms.For(getter),
                validator: validator,
                searchRegionsMax: maxRegions,
                ignoreEntirelyForbiddenRegions: ignoreEntirelyForbiddenRegions
                );

                if (bestFood != null)
                {
                    foodSource = bestFood;
                    foodDef = FoodUtility.GetFinalIngestibleDef(bestFood);
                    Log.Message(bestFood.ToString());
                    Log.Message(foodDef.ToString());
                    __result = true;
                    return false;
                }
                else
                {
                    Log.Message("No food for fiend");
                }
            }

            return desperate;
        }
    }
}
