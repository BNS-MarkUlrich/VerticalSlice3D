using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PionFallSound : ObjectSounds
{
    private void Awake()
    {
        smallAudio = Resources.Load<AudioClip>("Music/superliminalSteamPionFallSmall");
        mediumAudio = Resources.Load<AudioClip>("Music/SuperliminalSteamPionMediumFall");
        bigAudio = Resources.Load<AudioClip>("Music/SuperliminalSteamPionBigFall");
    }
}
