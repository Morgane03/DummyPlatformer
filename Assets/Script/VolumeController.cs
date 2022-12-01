using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float Volume)
    {
        Debug.Log(Volume);
        audioMixer.SetFloat("Volume", Volume);
    }
}
