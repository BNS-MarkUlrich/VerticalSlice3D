using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DialogueSoundScript : MonoBehaviour
{
    [SerializeField] private VideoClip _dialogueSounds;
    private VideoPlayer _dialogueVideoPlayer;

    private void Awake()
    {
        _dialogueVideoPlayer = GetComponent<VideoPlayer>();
    }
    void Start()
    {
        _dialogueVideoPlayer.SetDirectAudioVolume(0,0.2f);
        _dialogueVideoPlayer.clip = _dialogueSounds;
    }
}
