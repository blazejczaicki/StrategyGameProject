using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using System;

public class EnemyController : CharacterObjectController, IInteractable
{
    private CharacterObjectController target;
    [SerializeField] private List<CharacterTypes> potentialTargets;
    [SerializeField] private int escapeDistance = 10;

    protected override void Awake()
    {
        base.Awake();
        _type = CharacterTypes.enemy;
        movement = GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var characterCon = collision.GetComponent<CharacterObjectController>();
        if (characterCon!=null && potentialTargets.Exists(potentialTar=> potentialTar== characterCon.type) && target==null)
        {
            target = characterCon;
        }
    }
    private void OnMissingTarget()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) > escapeDistance)
        {
            target = null;
            movement.ResetMovement();
        }
    }

    private void OnTargedAcquired()
    {
        movement.Move(target, CheckOnWhichChunkYouStayed(transform.position));
    }

    private void TryAttack()
    {
        if (target!=null && Vector2.Distance(target.transform.position, transform.position)<weapon.range)
        {
            combatController.Attack(target.stats, weapon);
        }
    }

    public override void OnUpdate()
    {
        Profiler.BeginSample("EnemyUpd");
        OnMissingTarget();
        OnTargedAcquired();
        TryAttack();
        OnDead();
        Profiler.EndSample();
    }

    protected override void OnDead()
    {
        if (stats.health<=0)
        {
            target = null;
            movement.ResetMovement();
            gameObject.SetActive(false);
        }
    }

    public void OnLeftClickObject(PlayerController controller)
    {    }

    public void OnRightClickObject(PlayerController controller)
    {
        Debug.Log("nnnaatt");
        controller.TryAttack(this);
    }
}
