using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public static GameManager instance;
    [SerializeField] private GeneratorManager generatorManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private PlayerController player;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    return;
        //}
    }

    private void Update()
    {
        generatorManager.OnUpdate();
        enemyManager.OnUpdate();
        player.OnUpdate();
    }
}
