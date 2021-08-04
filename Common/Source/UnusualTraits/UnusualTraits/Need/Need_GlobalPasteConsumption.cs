using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using RimWorld.Planet;

namespace UnusualTraits
{
    class Need_GlobalPasteConsumption : Need
    {
        public override int GUIChangeArrow => -1;

        public Need_GlobalPasteConsumption(Pawn pawn) => this.pawn = pawn;

        public bool Disabled =>
            pawn.needs.TryGetNeed(
                NeedDefOf.Turn_GlobalPasteConsumption) == null ||
            pawn.Map.mapPawns.ColonistsSpawnedCount < 3;

        public MoodBuff MoodBuffForCurrentLevel
        {
            get
            {
                if (Disabled || CurLevel > 0.7)
                    return MoodBuff.Neutral;
                else if (CurLevel > 0.6)
                    return MoodBuff.SlightlyNegative;
                else if (CurLevel > 0.4)
                    return MoodBuff.Negative;
                else if (CurLevel > 0.3)
                    return MoodBuff.VeryNegative;
                return MoodBuff.ExtremelyNegative;
            }
        }

        public override void NeedInterval()
        {
            if (pawn.Map == null)
            {
                CurLevel = MaxLevel;
                return;
            }
            int numColonists = pawn.Map.mapPawns.ColonistsSpawnedCount;
            if (numColonists > 2)
                CurLevel -= 0.5f / 60000f * 150f;
            else
                CurLevel = MaxLevel;
        }

        public void NotifyPasteConsumed(Pawn p)
        {
            if (p != pawn)
                CurLevel += 0.5f / pawn.Map.mapPawns.ColonistsSpawnedCount;
        }

        public enum MoodBuff
        {
            ExtremelyNegative,
            VeryNegative,
            Negative,
            SlightlyNegative,
            Neutral,
        }
    }
}
