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
    class ThoughtDefOf
    {
        public static ThoughtDef Turn_AteFiendPaste;
        public static ThoughtDef Turn_AteFiendNotPaste;

        static ThoughtDefOf() =>
            DefOfHelper.EnsureInitializedInCtor(typeof(ThoughtDefOf));
    }
}
