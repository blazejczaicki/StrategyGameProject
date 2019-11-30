using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    protected ObjectMovement movement;
    protected CharacterStats stats;

    public abstract void OnUpdate();
}
