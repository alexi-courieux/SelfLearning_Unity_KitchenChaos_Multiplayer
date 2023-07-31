using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OpenClose = "OpenClose";
    
    [SerializeField] private ContainerCounter containerCounter;
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabObject += HandlePlayerGrabObject;
    }

    private void HandlePlayerGrabObject(object sender, EventArgs e)
    {
        _animator.SetTrigger(OpenClose);
    }
}
