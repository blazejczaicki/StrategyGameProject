using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera camer;
    [SerializeField] private List<Controller> controllers;
    [SerializeField] private PlayerInputController inputCon;

    private IInteractable interactableObject=null;

    private void Start()
    {
        inputCon.OnClickInteractionLeft += TryActivateObjectLeft;
        inputCon.OnClickInteractionRight += TryActivateObjectRight;
    }

    void Update()
    {
        foreach (var controller in controllers)
        {
            controller.OnUpdate();
        }
    }

    private IInteractable GetInteractableObject()
    {
        RaycastHit2D hit=Physics2D.Raycast(camer.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        return hit.collider != null ? hit.collider.GetComponent<IInteractable>() : null;
    }
    
    public void TryActivateObjectLeft()
    {
        interactableObject = GetInteractableObject();
        if(interactableObject!=null)
        {
            interactableObject.OnLeftClickObject();
        }
    }

    public void TryActivateObjectRight()
    {
        interactableObject = GetInteractableObject();
        if (interactableObject != null)
        {
            interactableObject.OnRightClickObject();
        }
    }
    // na odznaczenie, na zniszczenie, na oddalenie
    //funkcja do najeżdżania kursorem na obiekt
}
