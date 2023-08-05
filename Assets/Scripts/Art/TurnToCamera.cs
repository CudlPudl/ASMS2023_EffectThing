using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToCamera : MonoBehaviour
{
    private Transform _camera;
        
    public void Start()
    {
        _camera = Camera.main.transform;
    }
    void Update()
    {
        transform.rotation = _camera.rotation;
    } 
}
