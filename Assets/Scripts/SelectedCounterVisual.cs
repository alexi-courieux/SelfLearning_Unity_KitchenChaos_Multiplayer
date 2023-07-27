using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter counter;
    [SerializeField] private GameObject[] selectedCounterVisual;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += HandleSelectedCounterChanged;
    }

    private void HandleSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter == counter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (var visual in selectedCounterVisual)
        {
            visual.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (var visual in selectedCounterVisual)
        {
            visual.SetActive(false);
        }
    }
}
