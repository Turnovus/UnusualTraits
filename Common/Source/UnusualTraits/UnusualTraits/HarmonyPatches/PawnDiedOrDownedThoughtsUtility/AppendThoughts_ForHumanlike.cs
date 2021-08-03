﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;
using HarmonyLib;

namespace UnusualTraits
{
    [HarmonyPatch(typeof(PawnDiedOrDownedThoughtsUtility))]
    [HarmonyPatch("AppendThoughts_ForHumanlike")]
    class AppendThoughts_ForHumanlike
    {
        static void Postfix(
            Pawn victim,
            DamageInfo? dinfo,
            PawnDiedOrDownedThoughtsKind thoughtsKind,
            List<IndividualThoughtToAdd> outIndividualThoughts,
            List<ThoughtToAddToAll> outAllColonistsThoughts)
        {
            if (
                dinfo.HasValue &&
                dinfo.Value.Def.ExternalViolenceFor((Thing)victim) &&
                dinfo.Value.Instigator != null &&
                dinfo.Value.Instigator is Pawn instigator &&
                !instigator.Dead &&
                victim.Dead
                )
            {
                Need_Kill bloodthirst = (Need_Kill)instigator.needs.TryGetNeed(
                    NeedDefOf.Turn_Kill_Desire);
                if (bloodthirst != null)
                {
                    bloodthirst.Notify_KillEvent(0.25f);
                }
            }
            if (thoughtsKind == PawnDiedOrDownedThoughtsKind.Died)
            {
                foreach (Pawn pawn in 
                    PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive)
                {
                    if (
                        pawn != victim &&
                        pawn.needs != null &&
                        ThoughtUtility.Witnessed(pawn, victim)
                        )
                    {
                        Need_Kill bloodthirst =
                            (Need_Kill)pawn.needs.TryGetNeed(
                                NeedDefOf.Turn_Kill_Desire);
                        if (bloodthirst != null)
                        {
                            bloodthirst.Notify_KillEvent(0.10f);
                        }
                    }
                }
            }
        }
    }
}
