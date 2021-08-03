using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace UnusualTraits
{
    [DefOf]
    public static class TraitDefOf
    {
        public static TraitDef Turn_Bloodthirst;
        public static TraitDef Turn_PasteFiend;

        static TraitDefOf() =>
            DefOfHelper.EnsureInitializedInCtor(typeof(TraitDefOf));
    }
}
