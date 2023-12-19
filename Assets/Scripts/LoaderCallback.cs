using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool _isFirstUpdate = true;

    // We wait for the first update to call the LoaderCallback
    // it ensure the scene is loaded before calling the LoaderCallback
    private void Update()
    {
        if (!_isFirstUpdate) return;
        _isFirstUpdate = false;
        Loader.LoaderCallback();
    }
}
