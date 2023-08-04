using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaffSelector : MonoBehaviour
{
    [SerializeField] private Camera activeCamera;
    [SerializeField] private float rayDistance = 1000f;
    [SerializeField] private LayerMask rayMask;
    [SerializeField] private int selectLayer = 15;
    [SerializeField] private int walkableLayer = 9;
    [SerializeField] private StaffEvent onStaffSelect;

    public Camera ActiveCamera { get => activeCamera; set => activeCamera = value; }
    public StaffCreature SelectedStaff { get; private set; } = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { TryClick(Input.mousePosition); }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { ChangeWantedVisitorType(VisitorType.none); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { ChangeWantedVisitorType(VisitorType.nerd); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { ChangeWantedVisitorType(VisitorType.geek); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { ChangeWantedVisitorType(VisitorType.gamer); }
    }

    private void TryClick(Vector2 screenPoint)
    {
        Ray ray = ActiveCamera.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, rayMask))
        {
            if (hit.collider.gameObject.layer == selectLayer)
            { SelectStaff(hit.collider.gameObject); }

            else if (hit.collider.gameObject.layer == walkableLayer)
            { SetTargetTo(hit.point); }
        }
    }

    private void SelectStaff(GameObject staff)
    {
        StaffCreature newStaff = staff.transform.root.GetComponent<StaffCreature>();
        if (newStaff == null) { Debug.Log($"Staff {staff.name} doesn't have StaffCreature component."); return; }

        if (SelectedStaff != null) { SelectedStaff.StaffAi.FreeAi(); }

        SelectedStaff = newStaff;
        SelectedStaff.StaffAi.Select();

        onStaffSelect.Invoke(SelectedStaff);
    }

    private void SetTargetTo(Vector3 location)
    {
        if (SelectedStaff == null) { return; }
        SelectedStaff.Ai.SetTargetLocation(location);
    }

    public void ChangeWantedVisitorType(VisitorType visitorType)
    {
        if (SelectedStaff == null) { return; }
        SelectedStaff.WantedVisitorType = visitorType;
    }
}