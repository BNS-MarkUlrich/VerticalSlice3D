using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSounds : MonoBehaviour
{
    private AudioClip _fallingSound;
    private AudioSource _audioSource;
    private float scaleOfObject;

    void Update()
    {
        scaleOfObject = transform.localScale.x * transform.localScale.y * transform.localScale.z;
        if(scaleOfObject <= 1000)
        {
            _fallingSound = Resources.Load<AudioClip>("");
        }
        else if(scaleOfObject <= 8000)
        {
            _fallingSound = Resources.Load<AudioClip>("");
        }
        else if (scaleOfObject <= 27000)
        {
            _fallingSound = Resources.Load<AudioClip>("");
        }
        else
        {
            _fallingSound = Resources.Load<AudioClip>("");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            _audioSource.PlayOneShot(_fallingSound);
        }
    }
}
