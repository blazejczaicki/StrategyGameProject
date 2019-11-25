using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : Building, IInteractable
{
    [SerializeField] private InventoryObject inventoryObject;
    [SerializeField] private InventoryController inventoryController;

    private void Awake()
    {
        inventoryObject = new InventoryObject();
    }

    public void OnLeftClickObject(PlayerController controller)
    {
        inventoryController.inventoryObject = inventoryObject;
        inventoryController.transform.parent.gameObject.SetActive(true); 
    }

    public void OnRightClickObject(PlayerController controller)
    {
        DeleteBuilding();
    }

}
