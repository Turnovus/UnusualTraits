using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;

namespace UnusualTraits
{
    [StaticConstructorOnStartup]
    public class PatchRunner
    {
        static PatchRunner()
        {
            DoPatching("com.turnovus.unusualtraits");
        }

        static void DoPatching(string domain)
        {
            Harmony harmony = new Harmony(domain);
            harmony.PatchAll();
        }
    }
}