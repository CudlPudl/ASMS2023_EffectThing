using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : MonoBehaviour
{
    public void Move(Vector3 amount) { transform.position += amount; }
}