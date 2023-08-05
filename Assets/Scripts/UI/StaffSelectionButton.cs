using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StaffSelectionButton : MonoBehaviour
{

    [System.Serializable]
    public struct VisitorTypeIcon
    {
        public VisitorType Type;
        public Sprite Sprite;
    }

    [SerializeField] private List<VisitorTypeIcon> _icons;
    [SerializeField] private Image _iconImage;
    [SerializeField] private StaffSelectorWidget _selector;
    [SerializeField] VisitorType _type;

    public void Start()
    {
        for (var i = 0; i < _icons.Count; ++i)
        {
            if (_icons[i].Type == _type)
            {
                _iconImage.sprite = _icons[i].Sprite;
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

    public void OnValidate()
    {
        if (_iconImage == null)
            return;
        Start();
    }
}
