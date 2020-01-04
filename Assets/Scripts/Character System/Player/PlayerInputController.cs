using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputController : Controller
{
    [SerializeField] private InputData inputData;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private List<GameObject> UIElements;
    [SerializeField] private GameObject buildUI;
    [SerializeField] private GameObject inventoryUI;

    //List<RectTransform>

    public Action OnClickInteractionLeft;
    public Action OnClickInteractionRight;

    private Vector2 _movementDirection= Vector2.zero;
    public Vector2 MovementDirection { get => _movementDirection; }
    public List<GameObject> UIElements1 { get => UIElements; set => UIElements = value; }

    public override void OnUpdate()
    {
        CameraSize();
        InteractionInput();
        UIControl();
        MovementInput();
    }

    private void UIControl()
    {
        if (Input.GetKeyDown(inputData.escape))
        {
            var element=UIElements1.Find(obj => obj.activeSelf == true);
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

    private void InteractionInput()
    {
        if (Input.GetKey(inputData.interactionLeft))
        {
            OnClickInteractionLeft();
        }
        if (Input.GetKey(inputData.interactionRight))
        {
            OnClickInteractionRight();
        }
    }

    private void CameraSize()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            cameraController.UnZoom();
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            cameraController.Zoom();
        }
    }

    private void MovementInput()
    {
        _movementDirection = Vector3.zero;
        if (Input.GetKey(inputData.moveUp))
        {
            _movementDirection.y = 1;
        }
        if (Input.GetKey(inputData.moveDown))
        {
            _movementDirection.y = -1;
        }
        if (Input.GetKey(inputData.moveRight))
        {
            _movementDirection.x = 1;
        }
        if (Input.GetKey(inputData.moveLeft))
        {
            _movementDirection.x = -1;
        }
    }
}
