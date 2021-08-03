using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace UnusualTraits
{

    class Need_Kill : Need
    {
        const string THOUGHT_ICON_PATH = "Things/Mote/ThoughtSymbol/Skull";

        public override int GUIChangeArrow => -1;

        public Need_Kill(Pawn pawn) => this.pawn = pawn;

        public bool Disabled =>
            pawn.needs.TryGetNeed(NeedDefOf.Turn_Kill_Desire) == null;

        public override void SetInitialLevel()
        {
            if (pawn.Faction == Faction.OfPlayer)
                CurLevelPercentage = 1f;
            else
                CurLevelPercentage = 0.65f;
        }
            

        private float FallPerNeedIntervalTick =>
            def.fallPerDay / 60000f * 150f;

        public MoodBuff MoodBuffForCurrentLevel
        {
            get
            {
                if (Disabled || CurLevel > 0.4)
                    return MoodBuff.Neutral;
                else if (CurLevel > 0.3)
                    return MoodBuff.SlightlyNegative;
                else if (CurLevel > 0.2)
                    return MoodBuff.Negative;
                else if (CurLevel > 0.1)
                    return MoodBuff.VeryNegative;
                return MoodBuff.ExtremelyNegative;
            }
        }

        public void Notify_KillEvent(float value)
        {
            MoteMaker.MakeThoughtBubble(pawn, THOUGHT_ICON_PATH);
            CurLevel = Math.Min(CurLevel + value, MaxLevel);
        }

        public override void NeedInterval()
        {
            if (Disabled)
            {
                SetInitialLevel();
            }
            else
            {
                if (IsFrozen)
                    return;
                CurLevel -= FallPerNeedIntervalTick;
            }
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
