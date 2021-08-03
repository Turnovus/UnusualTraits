using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;


namespace UnusualTraits
{
    class ThoughtWorker_NeedKill : ThoughtWorker
    {
        private const int NEUTRAL = 4;

        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            Need_Kill thirst = p.needs.TryGetNeed<Need_Kill>();
            if (thirst == null || thirst.Disabled)
                return false;
            int currentBuff = (int)thirst.MoodBuffForCurrentLevel;
            return currentBuff < NEUTRAL ?
                ThoughtState.ActiveAtStage(NEUTRAL - currentBuff) :
                false;
        }
    }
}
