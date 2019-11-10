using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TransferItemController : MonoBehaviour
{
    public ItemObject item { get; set; }
    public RectTransform image { get; set; }
    public int amount { get; set; }
    public bool itemOnCursor{ get; set; }

    public Action OnDragging = delegate { };

    private void Update()
    {
        OnDragging();
    }

    public RectTransform LoadToTransfer(SlotController slot, RectTransform _image)
    {
        var slotData = slot.GetInventorySlot();
        item = slotData.item;
        image = _image;
        amount = slotData.amount;
        itemOnCursor = true;
        slot.ResetSlot();
        image.transform.SetParent(transform);
        var imageCanvasGroup=image.gameObject.AddComponent<CanvasGroup>();
        imageCanvasGroup.blocksRaycasts = false;
        
        return image;
    }

    public void ResetTransfer()
    {
        item = null;
        Destroy(image);
        image = null;
        amount = 0;
        itemOnCursor = false;
    }


}

