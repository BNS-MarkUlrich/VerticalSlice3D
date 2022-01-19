using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerBoardImpactSound : ObjectSounds
{
    private void Awake()
    {
        smallAudio = Resources.Load<AudioClip>("Music/superliminalSteamCheckerBoardFallSmall");
        mediumAudio = Resources.Load<AudioClip>("Music/SuperliminalSteamCheckerBoardMediumFall");
        bigAudio = Resources.Load<AudioClip>("Music/SuperliminalSteamCheckerBoardBigFall");
    }
}
