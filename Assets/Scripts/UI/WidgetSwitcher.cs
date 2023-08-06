using UnityEngine;

public class WidgetSwitcher : MonoBehaviour
{
    public void Awake()
    {
        SetActiveIndex(0);
    }

    public void SetActiveIndex(int index)
    {
        for (var i = 0; i < transform.childCount; ++i)
            transform.GetChild(i).gameObject.SetActive(i == index);
    }

    public void QuitGame() { Application.Quit(); }
}
