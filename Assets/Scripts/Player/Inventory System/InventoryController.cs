using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : Controller, IInventoryOperation
{
    [SerializeField] private InventoryObject inventoryObj;
    public InventoryObject inventoryObject { get { return inventoryObj; } }
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
        bool executed = inventoryObj.AddItem(item, amount);
        UpdateInventoryUI();
        return executed;
    }

    public void ResetSlot(int slotID)
    {
        inventoryObject.ResetInventorySlot(slotImages[slotID], slotID);
    }

    public InventorySlot GetInventorySlot(int slotID)
    {
        return inventoryObj.GetInventorySlot(slotID);
    }

    public void UpdateInventoryUI()
    {
        inventoryObj.OnChangedUpdateUI(slotImages);
    }



    public override void OnUpdate()
    {

    }
}
