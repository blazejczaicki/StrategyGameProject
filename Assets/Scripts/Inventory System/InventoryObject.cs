﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Inventory")]
public class InventoryObject : ScriptableObject, IInventoryOperation
{
    public List<InventorySlot> slots;
    /**
    *   limit pola w inwentarzu
    */
    [SerializeField] private int itemStackConstraint = 50;      

    private void Awake()
    {
        slots = new List<InventorySlot>();
    }

    public bool AddItem(ItemObject _item, int _amount)
    {
        var target = slots.Find(slot => slot.item.type == _item.type && slot.amount!=itemStackConstraint);

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

    public bool AddToTargetSlot(ItemObject _item, int _amount, int _slotID)
    {
        if (slots[_slotID].item.type==ItemType.Default)
        {
            slots[_slotID].item = _item;
            slots[_slotID].amount = _amount;
            return true;
        }
        else if(slots[_slotID].item.type==_item.type)
        {
            return AddItem(_item, _amount);        
        }
            return false;
    }

    public void ResetInventoryObject()
    {
        slots.Clear();
    }

    public void OnChangedUpdateUI(List<Image> slotImages)
    {
        foreach (var slot in slots)
        {
            slot.UpdateSlotInUI(slotImages[slot.slotID]);
        }
    }


    
    public void ResetInventorySlot(int slotID) 
    {
        slots[slotID].Reset();
    }
    
    public InventorySlot GetInventorySlot(int slotID)
    {
        return slots[slotID];
    }

    public int GetItemAmount(ItemType itemType)
    {
        int amount = 0;
        foreach (var slot in slots)
        {
            if (slot.item.type== itemType)
            {
                amount += slot.amount;
            }
        }
        return amount;
    }

    public void DecreaseItemAmount(ItemType itemType, int amountToDecrease)
    {
        int rest = amountToDecrease;
        foreach (var slot in slots)
        {
            if (slot.item.type == itemType)
            {
                rest = rest - slot.amount;
                if (rest<0)
                {
                    slot.amount = -rest;
                    break;
                }
                slot.Reset();
            }
        }
    }
}


[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public ItemObject defaultObject;
    public int amount=0;
    public int slotID=0;

    public InventorySlot(int _id, ItemObject _defaultObject)
    {
        item = defaultObject = _defaultObject;
        slotID = _id;
    }

    public void UpdateSlotInUI(Image image)
    {
        image.sprite = item.sprite;
        var color = image.color;
        color.a = (item.type == ItemType.Default) ? 0.0f : 1.0f;
        image.color = color;

        var num = image.GetComponentInChildren<Text>();
        if (item.type != ItemType.Default)
        {
            num.text = amount.ToString();
        }
        else
        {
            num.text = "";
        }
    }

    public void Reset()
    {
        item = defaultObject;
        amount = 0;
    }
}