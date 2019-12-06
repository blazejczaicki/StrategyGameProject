using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : Controller, IInventoryOperation
{
    public InventoryObject inventoryObject;
    [SerializeField] private List<Image> slotImages;
    [SerializeField] private DefaultObject defaultObject;

    private void Awake()
    {
        int id = 0;
        foreach (Transform slot in transform)// od biedy get component od slotController i ze slotImages
        {
            inventoryObject.slots.Add(new InventorySlot(id, defaultObject));
            id++;
        }
    }

    public bool AddItem(ItemObject item, int amount)
    {
        bool executed = inventoryObject.AddItem(item, amount);
        UpdateInventoryUI();
        return executed;
    }

    public void UpdateInventoryUI()
    {
        inventoryObject.OnChangedUpdateUI(slotImages);
    }




    public void ResetSlot(int slotID)
    {
        inventoryObject.ResetInventorySlot(slotID);
        UpdateInventoryUI();
    }

    public InventorySlot GetInventorySlot(int slotID)
    {
        return inventoryObject.GetInventorySlot(slotID);
    }

    public override void OnUpdate()
    {

    }
}
