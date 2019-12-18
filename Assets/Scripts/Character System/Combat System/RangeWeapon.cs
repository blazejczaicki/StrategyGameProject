using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangeWeapon : Weapon
{
    [SerializeField] private Transform projectile;

    public override void OnAttack(CharacterObjectController target, CombatController combatController)
    {
        //Debug.Log("Att pla: " + attackTime);
        //Debug.Log("cool pla: " + cooldown);
        //Debug.Log("time pla: " + Time.time);
        if (Time.time - attackTime > cooldown)
        {
            var newProjectile = Instantiate(projectile.gameObject, transform.position, Quaternion.identity);
            newProjectile.SetActive(true);
            attackTime = Time.time;
            var projectileComponent = newProjectile.GetComponent<Projectile>();
            projectileComponent.target = target;
            projectileComponent.combatController=combatController;
            projectileComponent.rangeWeapon = this;
        }
    }
}
