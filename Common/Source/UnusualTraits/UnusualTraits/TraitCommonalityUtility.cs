using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Verse;
using RimWorld;

namespace UnusualTraits
{
    [StaticConstructorOnStartup]
    static class TraitCommonalityUtility
    {
        const float MAX_MULT = 3.8f;

        private static readonly List<string> traitModsInt =
            new List<string>() { 
                "Vanilla Traits Expanded",
                "[KV] Consolidated Traits",
                "[SYR] Individuality",
            };
        private static List<TraitDef> affectedTraitsInt =
            new List<TraitDef>()
            {
                TraitDefOf.Turn_Bloodthirst,
                TraitDefOf.Turn_PasteFiend,
            };

        static TraitCommonalityUtility()
        {
            float mult = 1f;
            FieldInfo commonalityField = typeof(TraitDef).GetField(
                "commonality", BindingFlags.NonPublic | BindingFlags.Instance);
            
            foreach (string s in traitModsInt)
            {
                if (ModLister.HasActiveModWithName(s))
                    mult = Math.Min(MAX_MULT, mult + 0.5f);
            }

            if (mult <= 1f)
                return;

            Log.Message("Unusual Traits: Multiplied commonality for some " +
                "traits by x" + mult.ToString() + " to compensate for other " +
                "trait mods being present.");

            foreach (TraitDef d in affectedTraitsInt)
            {
                commonalityField.SetValue(d, 
                    (float)commonalityField.GetValue(d) * mult);
            }
        }
    }
}
