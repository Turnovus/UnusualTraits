using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace UnusualTraits
{
    public class InteractionWorker_Bloodthirsty : InteractionWorker
    {
        public override float RandomSelectionWeight(
            Pawn initiator,
            Pawn recipient)
        {
            if (initiator.story.traits.allTraits.Where(
                t => 
                t.def.GetModExtension<EnablesBloodthirstyRambling>() != null
                ).Any()
                )
                return 5f;
            return 0f;
        }
    }
}
