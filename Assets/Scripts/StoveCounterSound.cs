using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefs;
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.clip = audioClipRefs.stoveSizzle;
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        var playSound = e.State is StoveCounter.State.Frying or StoveCounter.State.Fried;
        if (playSound)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }
}
