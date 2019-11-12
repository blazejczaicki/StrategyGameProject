using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TransferItemController : Controller
{
    public ItemObject item { get; set; }
    public RectTransform image { get; set; }
    public int amount { get; set; }
    public bool itemOnCursor{ get; set; }
    private RectTransform rectTransform;

    public Action OnDragging = delegate { };

    private void Update()
    {
        OnUpdate();
    }

    public override void OnUpdate()
    {
        OnDragging();
    }

    public void LoadToTransfer(SlotController slot, RectTransform _image)
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
        OnDragging += ImagePositionUpdate;
        rectTransform = image;
    }

    public void ResetTransfer()
    {
        OnDragging -= ImagePositionUpdate;
        item = null;
        Destroy(image.gameObject);
        amount = 0;
        itemOnCursor = false;
    }

    public void ImagePositionUpdate()
    {
        rectTransform.position = Input.mousePosition;
    }
}

