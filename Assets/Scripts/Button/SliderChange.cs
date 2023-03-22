using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private AudioSource audio;
    void Start()
    {
        slider = GetComponent<Slider>();
        audio = FindObjectOfType<AudioSource>();
        slider.value = audio.volume;
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = slider.value;
    }
}
