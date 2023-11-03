using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbOff : MonoBehaviour
{
    private AudioReverbZone _audioReverb;

    private void Awake()
    {
        _audioReverb = GetComponent<AudioReverbZone>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _audioReverb.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _audioReverb.enabled = false;
    }
}
