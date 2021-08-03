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
    public static class NeedDefOf
    {
        public static NeedDef Turn_Kill_Desire;
        public static NeedDef Turn_GlobalPasteConsumption;

        static NeedDefOf() =>
            DefOfHelper.EnsureInitializedInCtor(typeof(NeedDefOf));
    }
}
