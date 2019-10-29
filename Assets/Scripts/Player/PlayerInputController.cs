using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputController : Controller
{
    [SerializeField] private InputData inputData;

    [SerializeField] private PlayerMovement characterMovement;
    [SerializeField] private CameraController cameraController;

    public Action OnClickInteractionLeft;
    public Action OnClickInteractionRight;

    private Vector2 movementDirection= Vector2.zero;

    public override void OnUpdate()
    {
        MouseInput();
        MovementInput();
    }

    private void MouseInput()
    {
        if (Input.GetKeyDown(inputData.interactionLeft))
        {
            OnClickInteractionLeft();
        }
        if (Input.GetKeyDown(inputData.interactionRight))
        {
            OnClickInteractionRight();
        }
        //  cameraController.Zoom(Input.GetAxis("Mouse ScrollWheel"));

        Debug.Log(Input.mouseScrollDelta);


        if (Input.mouseScrollDelta.y < 0)
        {
            cameraController.UnZoom();
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            cameraController.Zoom();
        }

        //do prawego przycisku
        //GetMouseButton do tworzenia strefy myszki
        //if (Input.GetMouseButtonDown(0))
        //{

        //}
        //if(Input.GetMouseButtonDown(1))
        //{

        //}
    }

    private void MovementInput()
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
