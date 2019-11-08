using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InventoryController inventory;
    [SerializeField] private int slotID=0;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {

        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {

        }
        if (eventData.button == PointerEventData.InputButton.Middle)
        {

        }
    }

    public void OnChangedSlotUpdateUI()
    {
        var slot = inventory.GetInventorySlot(slotID);        
    }
}
