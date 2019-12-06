using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : Building, IInteractable
{
    [SerializeField] protected InventoryObject inventoryObject;
    [SerializeField] protected InventoryController inventoryBuildingController;
    [SerializeField] private PlayerInputController player;
    [SerializeField] private Transform canvas;

    protected virtual void Awake()
    {
        inventoryObject = new InventoryObject();
        inventoryBuildingController = Instantiate(inventoryBuildingController.gameObject.transform.parent, Vector3.zero,Quaternion.identity,canvas ).GetComponentInChildren<InventoryController>();
        player.UIElements1.Add(inventoryBuildingController.gameObject.transform.parent.gameObject);
       // inventoryBuildingController.gameObject.transform.parent.SetParent(canvas);
    }

    public virtual void OnLeftClickObject(PlayerController controller)
    {
        inventoryBuildingController.inventoryObject = inventoryObject;
        inventoryBuildingController.transform.parent.gameObject.SetActive(true); 
    }

    public void OnRightClickObject(PlayerController controller)
    {
        DeleteBuilding();
    }

}
