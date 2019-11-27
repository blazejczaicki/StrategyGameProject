using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : Building, IInteractable
{
    [SerializeField] protected InventoryObject inventoryObject;
    [SerializeField] protected InventoryController inventoryController;

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
