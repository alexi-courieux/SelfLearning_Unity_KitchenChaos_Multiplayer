using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    private const string TriggerNumberPopup = "NumberPopup";
    private static readonly int NumberPopup = Animator.StringToHash(TriggerNumberPopup);
    
    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator _animator;
    private int _previousCountdownNumber;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        int countDownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countDownNumber.ToString(CultureInfo.CurrentCulture);
        
        if (countDownNumber != _previousCountdownNumber)
        {
            _previousCountdownNumber = countDownNumber;
            _animator.SetTrigger(NumberPopup);
            SoundManager.Instance.PlayCountdownSound();
        }
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
