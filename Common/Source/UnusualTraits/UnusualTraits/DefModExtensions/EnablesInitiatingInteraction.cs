using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace UnusualTraits
{
    public class EnablesInitiatingInteraction : DefModExtension
    {
        public List<InteractionDef> interactions = new List<InteractionDef>();
        public List<int> degrees = new List<int>() { 0 };
        public float weight = 1f;
    }
}
