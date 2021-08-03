using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace UnusualTraits
{
    class ThoughWorker_PasteConsumption : ThoughtWorker
    {
        private const int NEUTRAL = 4;

        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            Need_GlobalPasteConsumption paste =
                p.needs.TryGetNeed<Need_GlobalPasteConsumption>();
            if (paste == null || paste.Disabled)
                return false;
            int currentBuff = (int)paste.MoodBuffForCurrentLevel;
            return currentBuff < NEUTRAL ?
                ThoughtState.ActiveAtStage(NEUTRAL - currentBuff) :
                false;
        }
    }
}
