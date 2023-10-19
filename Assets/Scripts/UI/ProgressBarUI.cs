using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressGo;
    
    private IHasProgress _hasProgress;

    private void Start()
    {
        _hasProgress = hasProgressGo.GetComponent<IHasProgress>();
        if (_hasProgress == null)
        {
            Debug.LogError("Game Object : " + hasProgressGo.name + " does not have a component that implements IHasProgress!");
            throw new ArgumentOutOfRangeException("Game Object : " + hasProgressGo.name + " does not have a component that implements IHasProgress!");
        }
        _hasProgress.OnProgressChanged += HasProgressOnProgressChanged;
        Hide();
    }

    private void HasProgressOnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.ProgressNormalized;

        if (e.ProgressNormalized is 0f or 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
    
    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
