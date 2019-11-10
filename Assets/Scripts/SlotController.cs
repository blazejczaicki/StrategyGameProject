using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InventoryController inventory;
    [SerializeField] private int slotID=0;
    //[SerializeField] private RectTransform originalPosition;
    private RectTransform rectTransform;
   // private bool secondClick = false;

    [SerializeField] private TransferItemController transfer;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //if (secondClick)
            //{
            //    inventory.Ondragging -= ImagePositionUpdate;
            //    rectTransform.localPosition = Vector3.zero;
            //    secondClick = false;
            //}
            //else
            //{
            //    inventory.Ondragging += ImagePositionUpdate;
            //    secondClick = true;
            //}

            if (transfer.itemOnCursor)
            {
                Debug.Log("xd");
                if(inventory.inventory.AddToTargetSlot(transfer.item, transfer.amount, slotID))
                {
                    transfer.ResetTransfer();
                    transfer.OnDragging -= ImagePositionUpdate;
                }
            }
            else
            {
                rectTransform= transfer.LoadToTransfer(this, Instantiate(this.transform.GetChild(0).GetComponent<RectTransform>()));
                transfer.OnDragging += ImagePositionUpdate;
            }
        }
        //if (eventData.button == PointerEventData.InputButton.Right)
        //{

        //}
        //if (eventData.button == PointerEventData.InputButton.Middle)
        //{

        //}
    }

    public void ImagePositionUpdate()
    {
        rectTransform.position = Input.mousePosition;
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
