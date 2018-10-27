using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager _audioManager;

    public AudioSource myFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    private static bool created = false;

    void Awake()
    {
        if(this.gameObject.tag == "AudioManager")
        {
            if (!created)
            {
                DontDestroyOnLoad(this.gameObject);
                created = true;
            }

            if (_audioManager == null)
            {
                _audioManager = this;
            }
            else
            {
                DestroyObject(gameObject);
            }
        }   
    }

    public void HoverSound()
    {
        myFx.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        myFx.PlayOneShot(clickFx);
    }
}
