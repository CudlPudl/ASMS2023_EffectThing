using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleButton : MonoBehaviour, ISelectable
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private WidgetSwitcher _switcher;
    [SerializeField]
    private ToggleGroup _group;
    
    [Header("Debug")]
    [SerializeField]
    private bool _state = false;

    [FormerlySerializedAs("_onValueChanged")] [SerializeField]
    public UnityEvent<bool> OnValueChanged = new UnityEvent<bool>();

    public void OnValidate()
    {
        _button = GetComponent<Button>();
        _switcher = GetComponentInChildren<WidgetSwitcher>();
        if (transform.parent != null)
            _group = transform.parent.GetComponent<ToggleGroup>();
        SetToggleState(_state);
    }

    public void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (_group != null)
        {
            _group.SelectToggle(transform.GetSiblingIndex());
            OnValueChanged.Invoke(_state);
            return;
        }
        SetToggleState(!_state);
        OnValueChanged.Invoke(_state);
    }

    public void SetToggleState(bool value)
    {
        _state = value;
        _switcher.SetActiveIndex(value ? 0 : 1);
    }

    public void SetSelected(bool value)
    {
        if (_state != value)
            SetToggleState(value);
    }
}
