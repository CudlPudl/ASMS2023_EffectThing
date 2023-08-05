using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StaffelectionButton : MonoBehaviour
{

    [SerializeField] private List<(VisitorType type, Sprite icon)> _icons;
    [SerializeField] private Image _iconImage;
    [SerializeField] private StaffSelector _selector;

    public void OnStart()
    {
        for (var i = 0; i < _icons.Count; ++i)
        {
            if (_icons[i].type == _type)
            {
                _iconImage.sprite = _icons[i].icon;
            }
        }
    }
    [SerializeField] VisitorType _type;

    public void OnClick()
    {
        _selector.ChangeWantedVisitorType(_type);
    }
}
