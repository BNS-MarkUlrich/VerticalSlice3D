using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSounds : MonoBehaviour
{
    public bool MakeSound;

    private AudioClip _fallingSound;
    private AudioSource _audioSource;
    private float scaleOfObject;

    void Update()
    {
        scaleOfObject = transform.localScale.x * transform.localScale.y * transform.localScale.z;
        if(scaleOfObject <= 1000)
        {
            _fallingSound = Resources.Load<AudioClip>("");
            Debug.Log("Small");
        }
        else if(scaleOfObject <= 8000)
        {
            _fallingSound = Resources.Load<AudioClip>("");
            Debug.Log("medium");
        }
        else if (scaleOfObject <= 27000)
        {
            _fallingSound = Resources.Load<AudioClip>("");
            Debug.Log("Big");
        }
        else
        {
            _fallingSound = Resources.Load<AudioClip>("");
            Debug.Log("Huge");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(MakeSound == true)
        {
            //_audioSource.PlayOneShot(_fallingSound);
            Debug.Log("play music");
            MakeSound = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if( MakeSound == false)
        {
            MakeSound = true;
        }
    }
}
