using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioSource : MonoBehaviour
{
    void Awake()
    {
		try
		{
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            AudioManager.Initialize(audioSource);
        }
		catch (System.Exception)
		{
		}
        
    }
}
