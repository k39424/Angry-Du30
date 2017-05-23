using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjustment : MonoBehaviour {

    public Slider volumeeAdjuster;
    public AudioSource audioS;

    public void VolumeAdjust()
    {
        audioS.volume = volumeeAdjuster.value;
    }
}
