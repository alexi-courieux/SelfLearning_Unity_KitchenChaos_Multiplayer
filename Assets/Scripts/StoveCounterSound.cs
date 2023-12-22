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
    private float _warningSoundTimer;
    private bool _playWarningSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.clip = audioClipRefs.stoveSizzle;
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void Update()
    {
        if (_playWarningSound)
        {
            _warningSoundTimer -= Time.deltaTime;
            if (_warningSoundTimer > 0f) return;
            SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            const float warningSoundTimerMax = 0.2f;
            _warningSoundTimer = warningSoundTimerMax;
        }
    }
    
    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        const float burnShowProgressAmount = .5f;
        _playWarningSound = stoveCounter.IsFried() && e.ProgressNormalized >= burnShowProgressAmount;
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
