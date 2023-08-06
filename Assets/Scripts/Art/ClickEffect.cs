using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    private Material _mat;
    private float _previousClickTime;
    private static readonly int Alpha = Shader.PropertyToID("_Alpha");

    public void Awake()
    {
        var mat = _renderer.material;
        _mat = new Material(mat);
        _renderer.material = _mat;
    }

    public void Update()
    {
        _mat.SetFloat(Alpha, Mathf.Max(1f-(Time.time-_previousClickTime)*6, 0f));
    }

    public void OnClick(Vector3 position)
    {
        _previousClickTime = Time.time;
        transform.position = position;
    }
}
