using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Linq;

public class TransferItemController : Controller
{
    [SerializeField] private PlayerInputController input;
    public ItemObject item { get; set; }
    public RectTransform image { get; set; }
    public int amount { get; set; }
    public bool itemOnCursor{ get; set; }
    private RectTransform rectTransform;

    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;
    List<RaycastResult> results = new List<RaycastResult>();

    public Action OnDragging = delegate { };

    private void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

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
        input.OnClickInteractionLeft += DropItem;
        rectTransform = image;
    }

    public void ResetTransfer()
    {
        OnDragging -= ImagePositionUpdate;
        item = null;
        Destroy(image.gameObject);
        amount = 0;
        input.OnClickInteractionLeft -= DropItem;
        itemOnCursor = false;
    }

    public void ImagePositionUpdate()
    {
        rectTransform.position =Input.mousePosition;
    }

    public void DropItem()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        raycaster.Raycast(pointerEventData, results);
        if (item != null && !results.Any())
        {
            var itemController=Instantiate(item.gameObject, GridCellCalculator.GetMousePosToCell(), Quaternion.identity).GetComponent<ItemController>();
            itemController.amount = amount;
            ResetTransfer();
        }
        results.Clear();
    }
}

