using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpecializedSlotController : SlotController
{
    //[SerializeField] private List<ItemType> allowedItems;
    [SerializeField] private ItemType allowedItem;
    public ItemType AllowedItem { get { return allowedItem; } }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {//allowedItems.Exists(type => type == transfer.item.type)
            /**
             * gdy przedmiot trzyma się kursora
             */
            if (transfer.itemOnCursor && allowedItem==transfer.item.type && inventory.inventoryObject.AddToTargetSlot(transfer.item, transfer.amount, slotID))   
            {
                transfer.ResetTransfer();
                inventory.UpdateInventoryUI();
            }
            else if (transfer.itemOnCursor == false && inventory.GetInventorySlot(slotID).item.type != ItemType.Default)
            {
                transfer.LoadToTransfer(this, Instantiate(this.transform.GetChild(0).GetComponent<RectTransform>()));
            }
        }
    }
}
