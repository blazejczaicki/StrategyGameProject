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

    public void ResetInventorySlot(Image _image, int slotID)
    {
        slots[slotID].Reset(_image);
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
            color.a = 1.0f;
            image.color = color;
            changed = false;
        }
    }

    public void Reset(Image image)
    {
        item = defaultObject;
        amount = 0;
        image.sprite = item.sprite;
        var color = image.color;
        color.a = 0.0f;
        image.color = color;
    }
}