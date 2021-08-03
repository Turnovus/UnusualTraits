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
    [HarmonyPatch("InappropriateForTitle")]
    class InappropriateForTitle
    {
        static void Postfix(bool __result, ThingDef food, Pawn p)
        {
            if (p.story != null &&
                p.story.traits.allTraits.Where(t =>
                    t.def == TraitDefOf.Turn_PasteFiend).Any())
            {
                if (food == RimWorld.ThingDefOf.NutrientPasteDispenser ||
                    food == RimWorld.ThingDefOf.MealNutrientPaste)
                    __result = false;
            }
        }
    }
}
