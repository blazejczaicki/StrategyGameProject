using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : Building, IInteractable
{
    [SerializeField] protected InventoryObject inventoryObject;
    [SerializeField] protected InventoryController inventoryController;

    protected virtual void Awake()
    {
        inventoryObject = new InventoryObject();
    }

    public virtual void OnLeftClickObject(PlayerController controller)
    {
        inventoryController.inventoryObject = inventoryObject;
        inventoryController.transform.parent.gameObject.SetActive(true); 
    }

    public void OnRightClickObject(PlayerController controller)
    {
        DeleteBuilding();
    }

}
