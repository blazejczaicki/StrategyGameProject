﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private float _speed = 0;
    public float speed { get => _speed; }
    [SerializeField] private float _health = 0;
    public float health { get => _health; set => _health = value; }
         
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateBar(health);
    }
}
