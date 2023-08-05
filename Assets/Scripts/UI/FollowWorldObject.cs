using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWorldObject : MonoBehaviour
{
    [SerializeField] protected Transform _followTarget;
    [SerializeField] protected Camera _camera;

    void Update()
    {
        _followTarget.position = _camera.WorldToScreenPoint(_followTarget.position);
    }
}
