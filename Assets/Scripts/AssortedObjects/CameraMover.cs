using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public static CameraMover instance;

    [SerializeField] private Transform targetObject;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float bounds = 40f;
    [SerializeField] private float followSpeed = 1f;
    [SerializeField] private float targetMoveSpeed = 1f;
    [Space(20)]
    [SerializeField] private int cursorMovementPadding = 200;
    [Space(20)]
    [SerializeField] private KeyCode up = KeyCode.W;
    [SerializeField] private KeyCode down = KeyCode.S;
    [SerializeField] private KeyCode right = KeyCode.D;
    [SerializeField] private KeyCode left = KeyCode.A;
    [Space(20)]
    [SerializeField] private KeyCode focusPlus = KeyCode.E;
    [SerializeField] private KeyCode focusMinus = KeyCode.Q;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }

    void Update()
    {
        cameraPivot.position += (targetObject.position - cameraPivot.position) * (Time.deltaTime * followSpeed);
        
        Vector3 totalMovement = MouseBasedMovement();
        if (Input.GetKey(up)) { totalMovement += Vector3.forward; }
        if (Input.GetKey(down)) { totalMovement -= Vector3.forward; }
        if (Input.GetKey(right)) { totalMovement += Vector3.right; }
        if (Input.GetKey(left)) { totalMovement -= Vector3.right; }

        if (totalMovement != Vector3.zero)
        {
            totalMovement.Normalize();
            MoveTarget(totalMovement * (Time.deltaTime * targetMoveSpeed));
        }
        
        if (Input.GetKeyDown(focusPlus)) { FocusToStaff(1); }
        if (Input.GetKeyDown(focusMinus)) { FocusToStaff(-1); }
    }

    public Vector3 MouseBasedMovement()
    {
        // Don't really need to calculate this every frame. But you know, game jam. :^)
        var res = new Vector2(Screen.width, Screen.height);
        var compensation = 1080f / res.y;
        var paddingSize = cursorMovementPadding * compensation;

        var mousePos = Input.mousePosition;
        var xDelta = NegativeValue(mousePos.x, res.x, paddingSize)
                     + PositiveValue(mousePos.x, res.x, paddingSize);
        var yDelta = NegativeValue(mousePos.y, res.y, paddingSize)
                     + PositiveValue(mousePos.y, res.y, paddingSize);

        return new Vector3(xDelta, 0, yDelta);
    }

    public float NegativeValue(float value, float max, float div)
    {
        return Mathf.Clamp(SafeDivide(value - div, div), -1f, 0f);
    }

    public float PositiveValue(float value, float max, float div)
    {
        Debug.LogWarning(value + div - max);
        return Mathf.Clamp(SafeDivide(value + div - max, div), 0f, 1f);
    }

    public float SafeDivide(float a, float b)
    {
        return Mathf.Abs(a) < 0.001f ? 0f : a / b;
    }

    public void MoveTarget(Vector3 amount)
    {
        targetObject.position += amount;
        targetObject.position = new Vector3(Mathf.Clamp(targetObject.position.x, -bounds, bounds), 0, Mathf.Clamp(targetObject.position.z, -bounds, bounds));
    }

    public void SetTargetTo(GameObject target) { SetTargetTo(target.transform); }
    public void SetTargetTo(Transform target) { SetTargetTo(target.position); }
    public void SetTargetTo(Vector3 location)
    {
        targetObject.position = location;
        targetObject.position = new Vector3(Mathf.Clamp(targetObject.position.x, -bounds, bounds), 0, Mathf.Clamp(targetObject.position.z, -bounds, bounds));
    }


    private int previousFocus = 0;
    /// <summary>
    /// Direction: -1 or +1, or whatever.
    /// </summary>
    public void FocusToStaff(int direction)
    {
        if (ObjectManager.instance.SpawnedStaffs.Count == 0) { return; }

        previousFocus += direction;
        while (previousFocus >= ObjectManager.instance.SpawnedStaffs.Count)
        { previousFocus -= ObjectManager.instance.SpawnedStaffs.Count; }
        while (previousFocus < 0)
        { previousFocus += ObjectManager.instance.SpawnedStaffs.Count; }

        SetTargetTo(ObjectManager.instance.SpawnedStaffs[previousFocus].transform);
    }
}
