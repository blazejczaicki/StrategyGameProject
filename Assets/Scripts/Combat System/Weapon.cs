using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected int range = 0;
    protected float damage = 0;
    protected float cooldown = 0;

    public abstract float GetDamage();
}
