using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSelect : MonoBehaviour
{
    [SerializeField] private StaffSelector staffSelector;
    [SerializeField] private Transform rendererPivot;
    [SerializeField] private SpriteRenderer renderer;

    public void UpdateArea(Vector3 pointA, Vector3 pointB)
    {
        Vector2 size = new Vector2(pointB.x - pointA.x, pointB.z - pointA.z);

        transform.position = pointA;
        renderer.size = size;
        rendererPivot.localPosition = new Vector3(size.x / 2f, 0.1f, size.y / 2f);
        if (!gameObject.activeInHierarchy) { gameObject.SetActive(true); }
    }

    public void EndArea(Vector3 pointA, Vector3 pointB) { FindStaff(pointA, pointB); gameObject.SetActive(false); }

    private void FindStaff(Vector3 pointA, Vector3 pointB)
    {
        Bounds bounds = new Bounds((pointA + pointB) / 2f, new Vector3(Mathf.Abs(pointB.x - pointA.x), 200f, Mathf.Abs(pointB.z - pointA.z)));


        List<StaffCreature> result = new List<StaffCreature>();
        ObjectManager.instance.SpawnedStaffs.ForEach(x =>
        {
            if (bounds.Contains(x.transform.position)) { result.Add(x); }
        });

        if (result.Count > 0) { staffSelector.SelectStaff(result); }
    }
}
