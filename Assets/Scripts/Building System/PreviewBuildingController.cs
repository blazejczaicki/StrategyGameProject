using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class PreviewBuildingController : MonoBehaviour
{
    private List<bool> isCollidedList=new List<bool>();
    [SerializeField] private Transform building;
    [SerializeField] private PlayerInputController input;
    [SerializeField] private InventoryController inventory;
    [SerializeField] private BuildingRequirements buildingRequirements;
    private SpriteRenderer sprite;
    private Color color;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isCollidedList.Add(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollidedList.Remove(true);
    }

    private void OnEnable()
    {
        input.OnClickInteractionLeft += TryPlaceObject;
        input.OnClickInteractionRight += CancelDraggingBuilding;
    }

    private void OnDisable()
    {
        input.OnClickInteractionLeft -= TryPlaceObject;
        input.OnClickInteractionRight -= CancelDraggingBuilding;
    }

    private void Update()
    {
        OnUpdate();
    }

    public void TryPlaceObject()
    {
        if (!isCollidedList.Any() && buildingRequirements.IsRequirementsFulFilled(inventory.inventoryObject)) // check req po stronie req wymagające amount z inv
        {
            PlaceBuilding();
        }
    }

    private void CancelDraggingBuilding()
    {
        gameObject.SetActive(false);
    }

    public void OnUpdate()
    {
        ChangePatternPosition();
    }

    private void PlaceBuilding()
    {
        foreach (var require in buildingRequirements.requirements)
        {
            inventory.inventoryObject.DecreaseItemAmount(require.itemType, require.amount);
        }
        inventory.UpdateInventoryUI();
        Instantiate(building, transform.position, Quaternion.identity).gameObject.SetActive(true);
        //chunk
    }

    private void ChangePatternPosition()
    {
        if (!isCollidedList.Any())
        {
            color = Color.green;
            color.a = 0.5f;
            sprite.color = color;
        }
        else
        {
            color = Color.red;
            color.a = 0.5f;
            sprite.color = color;
        }
        transform.position = GridCellCalculator.GetMousePosToCell();
    }
}
