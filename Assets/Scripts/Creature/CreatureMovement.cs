using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : MonoBehaviour
{
    [SerializeField] private Creature creature;
    [SerializeField] private GoalAction onForcebounds;
    [SerializeField] private float speedMultiplier = 1.0f;
    public void Move(Vector3 amount)
    {
        transform.position += amount * speedMultiplier;
        if (transform.position.x > 39.5f || transform.position.x < -39.5f || transform.position.z > 39.5f || transform.position.z < -39.5f)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -39.5f, 39.5f), 0f, Mathf.Clamp(transform.position.z, -39.5f, 39.5f));
            onForcebounds.SetAction(creature);
        }

    }
}