using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWorldObject : MonoBehaviour
{
    [SerializeField] protected Transform _followTarget;
    [SerializeField] protected Camera _camera;

    protected void Update()
    {
        if (_followTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = _camera.WorldToScreenPoint(_followTarget.position);
    }
}
