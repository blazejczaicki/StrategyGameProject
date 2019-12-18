using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CharacterStats))]
public class CombatController : MonoBehaviour
{
    public void Attack(CharacterStats target, Weapon weapon )
    {
      target.TakeDamage(weapon.damage);
    }
}
