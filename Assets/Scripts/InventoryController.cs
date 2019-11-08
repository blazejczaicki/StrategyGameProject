using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryController : Controller, IInventoryOperation
{
    [SerializeField] private InventoryObject inventoryObject;
    public InventoryObject inventory { get { return inventoryObject; } }


    [SerializeField] private DefaultObject defa;

    private void Awake()
    {
        //var defaultObject = ScriptableObject.CreateInstance<DefaultObject>();
        int i = 0;
        foreach (Transform slot in transform)
        {
            inventory.slots.Add(new InventorySlot(i,  defa, slot.GetComponentInChildren<Image>()));
            i++;
        }
    }

    public bool AddItem(ItemObject item, int amount)
    {
        bool executed = inventoryObject.AddItem(item, amount);
        inventoryObject.OnChangedUpdateUI();
        return executed;
    }





    public override void OnUpdate()
    {
        
    }

    public InventorySlot GetInventorySlot(int slotID)
    {
        return inventoryObject.GetInventorySlot(slotID);
    }

}
