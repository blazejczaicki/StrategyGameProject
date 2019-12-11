using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<EnemyController> enemies;

    private void Awake()
    {
        enemies = new List<EnemyController>();
    }

    public void OnUpdate()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.gameObject.activeSelf==true)
            {
                enemy.OnUpdate();
            }
        }
    }

    public void AddEnemy(EnemyController enemy)
    {
        enemies.Add(enemy);
    }
}
