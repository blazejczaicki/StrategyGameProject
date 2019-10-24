using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterInputController : Controller
{
    [SerializeField] private InputData inputData;

    [SerializeField] private CharacterMovement characterMovement;



    private Vector2 movementDirection= Vector2.zero;

    public override void OnUpdate()
    {
        movementDirection = Vector3.zero;
        if (Input.GetKey(inputData.moveUp))
        {
            movementDirection.y = 1;
        }
        if (Input.GetKey(inputData.moveDown))
        {
            movementDirection.y = -1;
        }
        if (Input.GetKey(inputData.moveRight))
        {
            movementDirection.x = 1;
        }
        if (Input.GetKey(inputData.moveLeft))
        {
            movementDirection.x = -1;
        }
        characterMovement.Move(movementDirection);
    }
}
