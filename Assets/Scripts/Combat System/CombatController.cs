using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterStats))]
public class CombatController : MonoBehaviour
{
    private CharacterStats ownerStats;
    private float attackTime=0;

    private void Awake()
    {
        ownerStats = GetComponent<CharacterStats>();
    }

    public void Attack(CharacterStats target, Weapon weapon )
    {
        if (Time.time-attackTime>weapon.cooldown)
        {
            target.TakeDamage(weapon.damage);
            attackTime = Time.time;
        }
    }
}
