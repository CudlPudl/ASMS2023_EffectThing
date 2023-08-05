using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGroup : MonoBehaviour
{
    public void SelectToggle(int index)
    {
        for (var i = 0; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i).GetComponent<ISelectable>();
            if (child != null)
                child.SetSelected(i == index);
        }
    }
}
