using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjustment : MonoBehaviour {

    public Slider volumeeAdjuster;
    public AudioSource audioSource;

    public void VolumeAdjust()
    {
        audioSource.volume = volumeeAdjuster.value;
    }
}
