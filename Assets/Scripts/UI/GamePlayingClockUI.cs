using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockText;
    [SerializeField] private Image timerImage;

    private void Update()
    {
        timerImage.fillAmount = 1 - GameManager.Instance.GetPlayingTimerNormalized();
    }
}
