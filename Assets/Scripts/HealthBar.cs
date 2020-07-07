using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthBar;
    [SerializeField] private float maxHealth;

    private void Awake()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void UpdateBar(float currentHealth)
    {
        if (currentHealth<=0)
        {
            currentHealth = maxHealth;
        }
        healthBar.value = currentHealth;
    }
}
