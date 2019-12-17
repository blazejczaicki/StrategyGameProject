using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GeneratorManager generatorManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private PlayerController player;

    private void Update()
    {
        generatorManager.OnUpdate();
        enemyManager.OnUpdate();
        player.OnUpdate();
    }
}
