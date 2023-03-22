using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();
    public static void Initialize(AudioSource source)
    {
        audioSource = source;
        audioClips.Add(AudioClipName.TowerShoot,
            Resources.Load<AudioClip>("shoot"));
        audioClips.Add(AudioClipName.AddCoint,
            Resources.Load<AudioClip>("earnCoint"));
        audioClips.Add(AudioClipName.BuyTower,
            Resources.Load<AudioClip>("buyTower"));
        audioClips.Add(AudioClipName.BuyFail,
            Resources.Load<AudioClip>("buyFail"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}