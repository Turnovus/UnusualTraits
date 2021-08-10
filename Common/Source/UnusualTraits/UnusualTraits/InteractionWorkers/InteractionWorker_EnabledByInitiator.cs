using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace UnusualTraits
{
    class InteractionWorker_EnabledByInitiator : InteractionWorker
    {
        public override float RandomSelectionWeight(
            Pawn initiator,
            Pawn recipient)
        {
            float highestWeight = 0f;
            if (initiator.story != null)
            {
                foreach (Trait t in initiator.story.traits.allTraits)
                {
                    EnablesInitiatingInteraction e =
                        t.def.GetModExtension<EnablesInitiatingInteraction>();
                    if (e != null) {
                        Log.Message(e.weight.ToString());
                        if (e.interactions.Contains(interaction) &&
                        e.weight > highestWeight)
                            highestWeight = e.weight;
                    }
                }
            }
            return highestWeight;
        }
    }
}
