using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q4MaterialColor : Q4ComponentPlayer<Q4VarColor, Color, Gradient>
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private string colorName = "_BaseColor";
    [SerializeField] private int materialId = 0;
    [SerializeField] private bool useBaseIntensity = true;
    private Material mat;
    private float intensity = 1f;

    protected override void Awake()
    {
        base.Awake();
        mat = Instantiate(renderer.materials[materialId]);
        renderer.materials[materialId] = mat;
        if (useBaseIntensity) { intensity = mat.GetColor(colorName).GetIntensity(); }
    }
    protected override void CreatePreset()
    {
        Preset = new Q4VarColor(CustomValue, CustomEvaluatable);
    }

    protected override Color Get()
    {
        return renderer.materials[materialId].GetColor(colorName);

    }

    protected override void Set(Color currentValue)
    {
        if (useBaseIntensity) { currentValue = currentValue.SetIntensity(intensity); }
        renderer.materials[materialId].SetColor(colorName, currentValue);
    }
}