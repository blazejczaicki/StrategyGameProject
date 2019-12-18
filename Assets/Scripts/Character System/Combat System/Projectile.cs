using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float explosionLimit=1.0f;
    [SerializeField] private float projectileSpeed=100.0f;
    private Rigidbody2D rb;

    private CharacterObjectController _target;
    private CombatController _combatController;
    private RangeWeapon _rangeWeapon;

    public CharacterObjectController target { get => _target; set => _target = value; }
    public CombatController combatController { get => _combatController; set => _combatController = value; }
    public RangeWeapon rangeWeapon { get => _rangeWeapon; set => _rangeWeapon = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (target!=null)
        {
            FlyToTarget();
            if (TargetAchived())
            {
                combatController.Attack(target.stats, _rangeWeapon);
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void FlyToTarget()
    {
        var heading = transform.position - target.transform.position;
        var movementDirection = -heading / heading.magnitude;
        rb.MovePosition(rb.position + (Vector2)movementDirection * projectileSpeed * Time.fixedDeltaTime);
    }

    private bool TargetAchived()
    {
        return Vector2.Distance(target.transform.position, transform.position) < explosionLimit;
    }
}
