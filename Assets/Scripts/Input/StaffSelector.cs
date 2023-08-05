using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class StaffSelector : MonoBehaviour
{
    [SerializeField] private Camera activeCamera;
    [SerializeField] private float rayDistance = 1000f;
    [SerializeField] private LayerMask selectLayer;
    [SerializeField] private LayerMask walkLayer;
    public StaffEvent OnStaffSelect = new StaffEvent();
    [SerializeField] private AreaSelect areaSelect;
    [SerializeField] private float NewSelectionDelay = 0.1f;
    private float previousSelectionTime = 0f;

    public Camera ActiveCamera { get => activeCamera; set => activeCamera = value; }
    public List<StaffCreature> SelectedStaff { get; private set; } = new List<StaffCreature>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { TryClick(Input.mousePosition); }
        if (Input.GetMouseButton(0)) { UpdateArea(Input.mousePosition); }
        if (Input.GetMouseButtonUp(0)) { TryEndClick(Input.mousePosition); }

        if (Input.GetMouseButtonUp(1)) { TryMove(Input.mousePosition);}

        if (Input.GetKeyUp(KeyCode.Space)) { FocusOnSelection(); }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { ChangeWantedVisitorType(VisitorType.none); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { ChangeWantedVisitorType(VisitorType.nerd); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { ChangeWantedVisitorType(VisitorType.geek); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { ChangeWantedVisitorType(VisitorType.gamer); }

        timeSinceLastEndClick += Time.deltaTime;
    }

    private void FocusOnSelection()
    {
        var count = SelectedStaff.Count;
        if (count <= 0)
            return;

        Vector3 sum = Vector3.zero;
        foreach (var staff in SelectedStaff)
        {
            sum += staff.transform.position;
        }
        CameraMover.instance.SetTargetTo(sum / count);
    }

    private void TryMove(Vector3 mousePosition)
    {
        Ray ray = ActiveCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out var hit, rayDistance, walkLayer))
        {
            SetTargetTo(hit.point);
        }

    }

    private Vector3 areaStartPoint = Vector3.zero;
    private void TryClick(Vector2 screenPoint)
    {
        areaIsOn = false;

        Ray ray = ActiveCamera.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, selectLayer))
        {
            SelectStaff(hit.collider.gameObject);
        }
        else if (Physics.Raycast(ray, out hit, rayDistance, walkLayer))
        {
            areaStartPoint = hit.point;
            areaIsOn = true;
        }
    }

    private void UpdateArea(Vector2 screenPoint)
    {
        if (!areaIsOn) { return; }

        Ray ray = ActiveCamera.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, walkLayer))
        {
            areaSelect.UpdateArea(areaStartPoint, hit.point);
        }
    }

    private float timeSinceLastEndClick = 0.0f;
    private void TryEndClick(Vector2 screenPoint)
    {
        Ray ray = ActiveCamera.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, walkLayer))
        {
            if (areaIsOn) { areaSelect.EndArea(areaStartPoint, hit.point); }
        }
        areaStartPoint = Vector3.zero;
        areaIsOn = false;
    }

    private bool areaIsOn = false;

    public void SelectStaff(List<StaffCreature> staff)
    {
        // Avoid spam selection
        if (Time.time - previousSelectionTime < NewSelectionDelay)
            return;
        
        if (SelectedStaff != null) { SelectedStaff.ForEach(x => x.StaffAi.FreeAi()); };

        SelectedStaff = staff;
        for (int i = SelectedStaff.Count - 1; i >= 0; i--)
        {
            if (SelectedStaff[i].Ai.IsMad) { SelectedStaff.RemoveAt(i); continue; }
            SelectedStaff[i].StaffAi.Select();
            OnStaffSelect.Invoke(SelectedStaff[i]);
        }
        
        //Deselect in case none selected
        if (SelectedStaff.Count == 0)
            Deselect();

        previousSelectionTime = Time.time;
    }

    private void SelectStaff(GameObject staff)
    {
        StaffCreature newStaff = staff.transform.root.GetComponent<StaffCreature>();
        if (newStaff == null) { Debug.Log($"Staff {staff.name} doesn't have StaffCreature component."); return; }

        SelectStaff(newStaff);
    }
    public void SelectStaff(StaffCreature staff)
    { SelectStaff(new List<StaffCreature> { staff }); }

    public void Deselect()
    {
        OnStaffSelect.Invoke(null);
    }
    
    private void SetTargetTo(Vector3 location)
    {
        if (SelectedStaff.Count == 0) { return; }
        SelectedStaff.ForEach(x =>
            x.Ai.SetTargetLocation(location));
    }

    public void ChangeWantedVisitorType(VisitorType visitorType)
    {
        if (SelectedStaff.Count == 0) { return; }
        SelectedStaff.ForEach(x =>
            x.WantedVisitorType = visitorType);
    }
}