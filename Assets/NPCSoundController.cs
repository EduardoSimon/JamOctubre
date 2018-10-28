using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NPCSoundController : MonoBehaviour
{

    public AudioClip screamingClip;
    public AudioClip talkingClip;
    public AudioClip walkingClip;

    private AudioSource _audioSource;

    // Use this for initialization
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundTalking()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(talkingClip);
    }

    public void PlaySoundScreaming()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(screamingClip);
    }

    public void StopActiveSound(){
        if(_audioSource.isPlaying)
            _audioSource.Stop();    
    }

}
