using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Inventory")]
public class InventoryObject : ScriptableObject, IInventoryOperation
{
    public List<InventorySlot> slots;

    [SerializeField] private int itemStackConstraint = 50;



    public bool AddItem(ItemObject _item, int _amount)
    {
        var target = slots.Find(slot => slot.item.type == _item.type && slot.amount!=itemStackConstraint);
        Debug.Log(_item.type);
        if (target != null)
        {
            if (target.amount + _amount<=itemStackConstraint)
            {
                target.amount += _amount;
                target.changed = true;
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
            target.changed = true;
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
            slots[_slotID].changed = true;
            return true;
        }
        else
        {
            //inne przypadki
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

    public InventorySlot GetInventorySlot(int slotID)
    {
        return slots[slotID];
    }
    
    public void ResetInventorySlot(Image image, int slotID)
    {
        slots[slotID].Reset();
        slots[slotID].UpdateSlotInUI(image);
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
    public bool changed=false;

    public InventorySlot(int _id, ItemObject _defaultObject)
    {
        item = defaultObject = _defaultObject;
        slotID = _id;
    }

    public void UpdateSlotInUI(Image image)
    {
        if (changed)
        {
            image.sprite = item.sprite;
            var color = image.color;
            color.a = (item.type == ItemType.Default) ? 0.0f : 1.0f;
            image.color = color;
            changed = false;
        }
    }

    public void Reset()
    {
        item = defaultObject;
        amount = 0;
        changed = true;
    }
}