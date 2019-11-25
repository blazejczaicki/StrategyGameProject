using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputController : Controller
{
    [SerializeField] private InputData inputData;
    [SerializeField] private PlayerMovement characterMovement;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private List<GameObject> UIElements;
    [SerializeField] private GameObject buildUI;
    [SerializeField] private GameObject inventoryUI;

    //List<RectTransform>

    public Action OnClickInteractionLeft;
    public Action OnClickInteractionRight;

    private Vector2 movementDirection= Vector2.zero;
    

    public override void OnUpdate()
    {
        MouseInput();
        UIControl();
        MovementInput();
    }

    private void UIControl()
    {
        if (Input.GetKeyDown(inputData.escape))
        {
            var element=UIElements.Find(obj => obj.activeSelf == true);
            if (element!=null)
            {
                element.SetActive(false);
            }
        }
        if (Input.GetKeyDown(inputData.inventory))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        if (Input.GetKeyDown(inputData.buildScreen))
        {
            buildUI.SetActive(!buildUI.activeSelf);
        }
    }

    private void MouseInput()
    {
        if (Input.GetKey(inputData.interactionLeft))
        {
            OnClickInteractionLeft();
        }
        if (Input.GetKey(inputData.interactionRight))
        {
            OnClickInteractionRight();
        }

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
