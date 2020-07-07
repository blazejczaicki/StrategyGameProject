using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected InventoryController inventory;
    [SerializeField] protected TransferItemController transfer;
    [SerializeField] protected int slotID=0;

    public virtual void OnPointerClick(PointerEventData eventData) // prawy i środkowy przycisk
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            /**
             *  gdy przedmiot trzyma się kursora
             */ 
            if (transfer.itemOnCursor && inventory.inventoryObject.AddToTargetSlot(transfer.item, transfer.amount, slotID)) 
            {
                transfer.ResetTransfer();
                inventory.UpdateInventoryUI();
            }
            else if (transfer.itemOnCursor==false && inventory.GetInventorySlot(slotID).item.type != ItemType.Default)      //
            {
                transfer.LoadToTransfer(this, Instantiate(this.transform.GetChild(0).GetComponent<RectTransform>()));
            }
        }
    }

    public InventorySlot GetInventorySlot()
    {
        return inventory.GetInventorySlot(slotID);
    }

    public void ResetSlot()
    {
        inventory.ResetSlot(slotID);
    }
}
