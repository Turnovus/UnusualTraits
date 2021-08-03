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
    [HarmonyPatch(typeof(Thing))]
    [HarmonyPatch("Ingested")]
    class Ingested
    {
        public static void Postfix(
            Thing __instance,
            Pawn ingester)
        {
            if (__instance.def == RimWorld.ThingDefOf.MealNutrientPaste)
            {
                Log.Message(__instance.def.defName);
                foreach (Pawn p in
                    ingester.Map.mapPawns.FreeColonistsAndPrisonersSpawned)
                {
                    Need_GlobalPasteConsumption pasteNeed =
                        p.needs.TryGetNeed<Need_GlobalPasteConsumption>();
                    if (pasteNeed != null)
                        pasteNeed.NotifyPasteConsumed(ingester);
                }
            }
        }
    }
}
