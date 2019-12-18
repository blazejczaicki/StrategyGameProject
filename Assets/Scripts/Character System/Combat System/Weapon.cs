using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected int _range = 0;
    [SerializeField] protected float _damage = 0;
    [SerializeField] protected float _cooldown = 0;
    protected float attackTime = 0;

    public int range { get => _range; }
    public float damage { get => _damage; }
    public float cooldown { get => _cooldown;}

    public virtual void OnAttack(CharacterObjectController target, CombatController combatController)
    {
        //Debug.Log("Att en: " + attackTime);
        //Debug.Log("cool en: " + cooldown);
        //Debug.Log("time en: " + Time.time);
        if (Time.time - attackTime > cooldown)
        {
            attackTime = Time.time;
            combatController.Attack(target.stats, this);
        }
    }
}
