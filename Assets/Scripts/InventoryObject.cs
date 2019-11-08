﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="New Inventory")]
public class InventoryObject : ScriptableObject, IInventoryOperation
{
    public List<InventorySlot> slots;

    [SerializeField] private int itemStackConstraint = 50;



    public bool AddItem(ItemObject _item, int _amount)
    {
        var target = slots.Find(slot => slot.item == _item && slot.item.amount!=itemStackConstraint);
        
        if (target != null)
        {
            if (target.amount + _amount<=itemStackConstraint)
            {
                target.amount += _amount;
                return true;
            }
            else
            {
                int rest = itemStackConstraint - target.amount;
                target.amount = itemStackConstraint;
                return AddToNewSlot(_item, _amount-rest);
            }
        }
        return AddToNewSlot(_item, _amount);
    }

    private bool AddToNewSlot(ItemObject _item, int _amount)
    {
        var target = slots.Find(slot => slot.item.type == ItemType.Default);
        if (target != null)
        {
            target.item = _item;
            target.amount = _amount;
            return true;
        }
        return false;
    }

    public void ResetInventoryObject()
    {
        slots.Clear();
    }

    public void OnChangedUpdateUI()
    {
        foreach (var slot in slots)
        {
            slot.UpdateSlotInUI();
        }
    }

    public InventorySlot GetInventorySlot(int slotID)
    {
        return slots[slotID];
    }

}


[System.Serializable]
public class InventorySlot
{//stack size
    public ItemObject item;
    public ItemObject defaultObject;
    public int amount=0;
    public int id=0;
    public Vector3 position;

    public InventorySlot(int _id, Vector3 _position, ItemObject _defaultObject)
    {
        item = defaultObject = _defaultObject;
        position = _position;
        id = _id;
    }

    public void UpdateSlotInUI()
    {
        item.icon.transform.position = position;
        item.icon.SetActive(true);
    }
}