using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StaffSelectionButton : MonoBehaviour
{

    [SerializeField] private List<(VisitorType type, Sprite icon)> _icons;
    [SerializeField] private Image _iconImage;
    [SerializeField] private StaffSelectorWidget _selector;
    [SerializeField] VisitorType _type;

    public void Start()
    {
        for (var i = 0; i < _icons.Count; ++i)
        {
            if (_icons[i].type == _type)
            {
                _iconImage.sprite = _icons[i].icon;
            }
        }
    }

    public void SetType(VisitorType type)
    {
        _type = type;
        //pretty crude but w/e :DDD
        Start();
    }

    public void OnClick()
    {
         _selector.OnWantedTypeChanged(_type);
    }
}
