using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public static AudioManager instance;

    private Object[] _popClips;
    private AudioClip _perfectSound;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        _popClips = Resources.LoadAll("Sounds/Pop", typeof(AudioClip));
        _perfectSound = (AudioClip)Resources.Load("Sounds/SFX/Perfect");
    }

    public void PlayPopSound()
    {
        AudioClip audioClip = (AudioClip) _popClips[Random.Range(0, _popClips.Length)];
        PlayAudio(audioClip);
    }
    private void PlayAudio(AudioClip audioClip)
    {
        source.clip = audioClip;
        source.Play();
    }
   
    public void PlayPerfectSound()
    {
        source.clip = _perfectSound;
        source.Play();
    }
}
