using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtensions
{
    private static byte k_MaxByteForOverexposedColor = 191;
    public static float GetIntensity(this Color color)
    {
        var maxColor = color.maxColorComponent;
        var scale = k_MaxByteForOverexposedColor / maxColor;
        return Mathf.Log(255f / scale) / Mathf.Log(2f);
    }

    public static Color SetIntensity(this Color color, float intensity)
    {
        return color.NormalizeColor() * Mathf.Pow(2, intensity);
    }

    public static Color NormalizeColor(this Color color)
    {
        float highets = 0.0f;
        if (color.r > highets) { highets = color.r; }
        if (color.g > highets) { highets = color.g; }
        if (color.b > highets) { highets = color.b; }
        if (highets == 0f) { return color; }
        return new Color(color.r / highets, color.g / highets, color.b / highets);
    }
}
