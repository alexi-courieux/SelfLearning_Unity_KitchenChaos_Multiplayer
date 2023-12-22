using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    private const string IsFlashing = "IsFlashing";
    private static readonly int Flashing = Animator.StringToHash(IsFlashing);
    
    [SerializeField] private StoveCounter stoveCounter;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged; ;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        const float burnShowProgressAmount = .5f;
        bool flash = stoveCounter.IsFried() && e.ProgressNormalized >= burnShowProgressAmount;
        _animator.SetBool(Flashing, flash);
    }
}
