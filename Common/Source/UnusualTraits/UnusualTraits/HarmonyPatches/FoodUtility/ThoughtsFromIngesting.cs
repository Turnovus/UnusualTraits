using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using HarmonyLib;

namespace UnusualTraits
{
    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("ThoughtsFromIngesting")]
    class ThoughtsFromIngesting
    {
        static void Postfix(
            ref List<FoodUtility.ThoughtFromIngesting> ___ingestThoughts,
            Pawn ingester,
            Thing foodSource,
            ThingDef foodDef)
        {
            bool isPasteMeal =
                foodDef == RimWorld.ThingDefOf.MealNutrientPaste ||
                foodSource is Building_NutrientPasteDispenser;
            /*
            if (isPasteMeal)
            {
                Log.Message(foodDef.defName);
                foreach (Pawn p in
                    ingester.Map.mapPawns.FreeColonistsAndPrisonersSpawned)
                {
                    Need_GlobalPasteConsumption pasteNeed =
                        p.needs.TryGetNeed<Need_GlobalPasteConsumption>();
                    if (pasteNeed != null)
                        pasteNeed.NotifyPasteConsumed(ingester);
                }
            }
            */
            if (ingester.story != null &&
                ingester.story.traits.allTraits.Where(t =>
                t.def == TraitDefOf.Turn_PasteFiend).Any())
            {
                
                FoodUtility.ThoughtFromIngesting pasteThought =
                    new FoodUtility.ThoughtFromIngesting()
                    {
                        thought = isPasteMeal ? 
                        ThoughtDefOf.Turn_AteFiendPaste : 
                        ThoughtDefOf.Turn_AteFiendNotPaste,
                        fromPrecept = null
                    };
                ___ingestThoughts.Add(pasteThought);
            }
            
        }
    }
}
