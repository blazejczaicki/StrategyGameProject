using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : Controller, IInventoryOperation
{
    [SerializeField] private InventoryObject inventoryObject;
    public InventoryObject inventory { get { return inventoryObject; } }
    [SerializeField] private List<Image> slotImages;
    [SerializeField] private DefaultObject defa;
    //private Dictionary<int, Image> slotImages= new Dictionary<int, Image>();

    private void Awake()
    {
        //var defaultObject = ScriptableObject.CreateInstance<DefaultObject>();
        int i = 0;
        foreach (Transform slot in transform)
        {
            //slotImages.Add(i, slot.GetComponentInChildren<Image>());
            inventory.slots.Add(new InventorySlot(i,  defa));
            i++;
        }
    }

    public bool AddItem(ItemObject item, int amount)
    {
        bool executed = inventoryObject.AddItem(item, amount);
        inventoryObject.OnChangedUpdateUI(slotImages);
        return executed;
    }

    public override void OnUpdate()
    {
        
    }

    public InventorySlot GetInventorySlot(int slotID)
    {
        return inventoryObject.GetInventorySlot(slotID);
    }

    public void ResetSlot(int slotID)
    {
        inventory.ResetInventorySlot(slotImages[slotID], slotID);
    }

}
