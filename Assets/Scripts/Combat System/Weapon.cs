using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected int _range = 0;
    [SerializeField] protected float _damage = 0;
    [SerializeField] protected float _cooldown = 0;

    public int range { get => _range; }
    public float damage { get => _damage; }
    public float cooldown { get => _cooldown;}
}
