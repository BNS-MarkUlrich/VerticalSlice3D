using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockImpactSound : ObjectSounds
{
    private void Awake()
    {
        smallAudio = Resources.Load<AudioClip>("Music/superliminalSteamBlockFallSmall");
        mediumAudio = Resources.Load<AudioClip>("Music/SuperliminalSteamBlockMediumFall");
        bigAudio = Resources.Load<AudioClip>("Music/SuperliminalSteamBlockBigFall");
    }
}
