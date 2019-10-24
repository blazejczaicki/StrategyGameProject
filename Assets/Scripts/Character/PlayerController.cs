using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<Controller> controllers;
    void Update()
    {
        foreach (var controller in controllers)
        {
            controller.OnUpdate();
        }
    }
}
