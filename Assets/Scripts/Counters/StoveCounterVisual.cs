using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGo;
    [SerializeField] private GameObject particlesGo;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounterOnOnStateChanged;
    }

    private void StoveCounterOnOnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        var showVisual = e.State is StoveCounter.State.Frying or StoveCounter.State.Fried;
        stoveOnGo.SetActive(showVisual);
        particlesGo.SetActive(showVisual);
    }
}
