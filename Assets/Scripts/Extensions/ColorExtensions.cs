using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtensions
{
    public static float GetIntensity(this Color color)
    {
        return 0.299f * color.r + 0.587f * color.g + 0.114f * color.b;
        // return (color.r + color.g + color.b) / 3f;
    }

    public static Color SetIntensity(this Color color, float intensity)
    {
        float currentIntensity = color.GetIntensity();
        float normalizedIntensity = Mathf.Clamp01(intensity);

        Color.RGBToHSV(color, out float hue, out float saturation, out float brightness);

        brightness = Mathf.Pow(2, brightness + (normalizedIntensity - currentIntensity));


        Color result = Color.HSVToRGB(hue, saturation, brightness);
        result.a = color.a;
        return result;

        //intensity = Mathf.Pow(2, intensity);
        // return new Color(color.r * intensity, color.g * intensity, color.b * intensity);
    }
}
