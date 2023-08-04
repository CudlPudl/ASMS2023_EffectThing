using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Q4 String", menuName = "Q4/Q4 String", order = 20)]
public class Q4VarString : Q4Variable<string, AnimationCurve>
{
    public Q4VarString(string customValue, AnimationCurve animationCurve) : base(customValue, animationCurve) { }

    public override string Evaluate(float progress)
    {
        return Value.Substring(0, Mathf.RoundToInt(Evaluatable.Evaluate(progress) * Value.Length));
    }
}