using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelController : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    private Vector2 distanceBetweenCursorPanel = Vector2.zero;

    public void OnBeginDrag(PointerEventData eventData)
    {
        distanceBetweenCursorPanel.x = transform.position.x- eventData.position.x;
        distanceBetweenCursorPanel.y = transform.position.y - eventData.position.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
       transform.position = eventData.position+distanceBetweenCursorPanel;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
