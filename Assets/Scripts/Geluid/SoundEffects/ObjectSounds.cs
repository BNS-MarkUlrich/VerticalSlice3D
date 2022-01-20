using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ObjectSounds : MonoBehaviour
{
    public AudioClip smallAudio;
    public AudioClip mediumAudio;
    public AudioClip bigAudio;

    private AudioClip _fallingSound;
    private AudioSource _audioSource;
    private float scaleOfObject;

    private void Start()
    {
        _audioSource = FindObjectOfType<AudioSource>();
    }
    void Update()
    {
        scaleOfObject = transform.localScale.x * transform.localScale.y * transform.localScale.z;
        if (scaleOfObject <= 1000)
        {
            _fallingSound = smallAudio;
            Debug.Log("Small");
        }
        else if(scaleOfObject <= 8000)
        {
            _fallingSound = mediumAudio;
            Debug.Log("medium");
        }
        else 
        {
            _fallingSound = bigAudio;
            Debug.Log("Big");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude > 1 && collision.gameObject.tag != "Player")
        {
            _audioSource.PlayOneShot(_fallingSound);
        }
    }
}
