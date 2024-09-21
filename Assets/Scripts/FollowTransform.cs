using System;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    private Transform targetTransform;
    
    public void SetTargetTransform(Transform newTargetTransform)
    {
        this.targetTransform = newTargetTransform;
    }

    private void LateUpdate()
    {
        if (targetTransform is null) return;
        
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
    }
}