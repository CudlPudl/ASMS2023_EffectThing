using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Q4 Int", menuName = "Q4/Q4 Int", order = 11)]
public class Q4VarInt : Q4Variable<int, AnimationCurve>
{
    public Q4VarInt(int customValue, AnimationCurve animationCurve) : base(customValue, animationCurve) { }

    public override int Evaluate(float progress)
    {
        return Mathf.RoundToInt(Evaluatable.Evaluate(progress) * Value);
    }
}