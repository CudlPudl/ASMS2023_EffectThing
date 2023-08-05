using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisitorCapturer : MonoBehaviour
{
    public List<VisitorCreature> RegisteredVisitors { get; private set; } = new List<VisitorCreature>();

    public void RegisterVisitor(VisitorCreature visitor)
    {
        if (visitor.CurrentlyCapturedBy != null)
        {
            if (visitor.CurrentlyCapturedBy == this) { return; }
            visitor.CurrentlyCapturedBy.DeregisterVisitor(visitor);
        }
        visitor.CurrentlyCapturedBy = this;
        RegisteredVisitors.Add(visitor);
    }

    public void DeregisterVisitor(VisitorCreature visitor)
    {
        visitor.CurrentlyCapturedBy = null;
        RegisteredVisitors.Remove(visitor);
    }

    public void DeregisterAllVisitors()
    {
        for (int i = RegisteredVisitors.Count - 1; i >= 0; i--)
        {
            RegisteredVisitors[i].CurrentlyCapturedBy = null;
            RegisteredVisitors.RemoveAt(i);
        }
    }

    public List<(VisitorType, int)> GetVisitorCountsByType()
    {
        List<(VisitorType, int)> result = new List<(VisitorType, int)>();

        for (int i = 0; i < RegisteredVisitors.Count; i++)
        {
            bool done = false;
            for (int j = 0; j < result.Count; j++)
            {
                if (result[j].Item1 == RegisteredVisitors[i].VisitorType)
                { result[j] = (result[j].Item1, result[j].Item2 + 1); done = true; break; }
            }
            if (!done) { result.Add((RegisteredVisitors[i].VisitorType, 1)); }
        }

        return result;
    }
}