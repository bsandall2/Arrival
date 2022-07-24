using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public float volumeToSet;

    void Start()
    {
        AudioListener.volume = volumeToSet;
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = slider.value;
    }
}
