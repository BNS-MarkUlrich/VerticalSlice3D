using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BoxFallSound : ObjectSounds
{
    private void Awake()
    {
        smallAudio = Resources.Load<AudioClip>("Music/superliminalSteamBoxFallSmall");
        mediumAudio = Resources.Load<AudioClip>("Music/SuperliminalSteam box impact medium");
        bigAudio = Resources.Load<AudioClip>("Music/SuperliminalSteam box impact big");
    }
}

