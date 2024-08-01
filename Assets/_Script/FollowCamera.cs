using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping = 0.25f;

    private Vector3 _currentVelocity;
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (!followTarget) return;
        if (Application.IsPlaying(this))
        {
            transform.position =
                Vector3.SmoothDamp(transform.position, followTarget.position + offset, ref _currentVelocity, damping);
        }
        else
        {
            transform.position = followTarget.position + offset;
        }
    }
}
