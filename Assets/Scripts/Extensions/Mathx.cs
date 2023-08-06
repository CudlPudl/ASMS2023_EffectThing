using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mathx
{
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreetoVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    public static string ToDigiTime(this float seconds)
    {
        seconds = Mathf.FloorToInt(seconds);
        string result = "";
        float temp = 0;
        if (seconds < 3600)
        {
            temp = seconds % 60;
            if (temp < 10) { result += "0"; }
            result += temp + ":";
            seconds -= temp * 60;

            temp = seconds % 1;
            if (temp < 10) { result += "0"; }
            result += temp + ":";
            seconds -= temp * 1;

            seconds *= 100;
            temp = seconds % 1;
            if (temp < 10) { result += "0"; }
            result += temp;
        }
        else if (seconds < 86400)
        {
            temp = seconds % 3600;
            if (temp < 10) { result += "0"; }
            result += temp + ":";
            seconds -= temp * 3600;

            temp = seconds % 60;
            if (temp < 10) { result += "0"; }
            result += temp + ":";
            seconds -= temp * 60;

            temp = seconds % 1;
            if (temp < 10) { result += "0"; }
            result += temp;
        }
        else
        {
            temp = seconds % 86400;
            if (temp < 10) { result += "0"; }
            result += temp + ":";
            seconds -= temp * 86400;

            temp = seconds % 3600;
            if (temp < 10) { result += "0"; }
            result += temp + ":";
            seconds -= temp * 3600;

            temp = seconds % 60;
            if (temp < 10) { result += "0"; }
            result += temp;
        }
        return result;
    }


    /// <summary>
    /// Converts coordinate Char to Byte
    /// </summary>
    /// <param name="c">Coordinate Char</param>
    /// <param name="aValue">Coordinate value for 'a'</param>
    /// <param name="dir">1 if value is ascending, -1 if decending</param>
    public static byte CharToCoordinate(char c, int aValue = 0, int dir = 1)
    {
        char cc = char.ToLower(c);
        if (aValue + (dir * (cc - 'a')) < 0) { return 255; }
        if (aValue + (dir * (cc - 'a')) > 255) { return 255; }
        return (byte)(aValue + (dir * (cc - 'a')));
    }

    public static RaycastHit ScreenRay(Vector2 point, float distance, LayerMask mask)
    {
        Ray ray = Camera.main.ScreenPointToRay(point);
        RaycastHit hit;
        Physics.Raycast(ray.origin, ray.direction, out hit, distance, mask);
        return hit;
    }
    public static RaycastHit2D ScreenRay2D(Vector2 point, float distance, LayerMask mask)
    {
        Ray ray = Camera.main.ScreenPointToRay(point);
        return Physics2D.Raycast(ray.origin, ray.direction, distance, mask);
    }
}