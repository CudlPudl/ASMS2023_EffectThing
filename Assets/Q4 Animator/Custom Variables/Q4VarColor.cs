using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Q4 Color", menuName = "Q4/Q4 Color", order = 100)]
public class Q4VarColor : Q4Variable<Color, Gradient>
{
    public Q4VarColor(Color customValue, Gradient gradient) : base(customValue, gradient) { }

    public override Color Evaluate(float progress)
    {
        return Evaluatable.Evaluate(progress) * Value;
    }
}