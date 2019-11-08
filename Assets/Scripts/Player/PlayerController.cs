using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera camer;
    [SerializeField] private List<Controller> playerToUpdateControllers;
    [SerializeField] private PlayerInputController inputCon;

    [SerializeField] private float extractionTool = 1.0f; //potem item z inventory

    public InventoryController inventory;

    private IInteractable interactableObjectFocus=null;

    private void Start()
    {
        inputCon.OnClickInteractionLeft += TryActivateObjectLeft;
        inputCon.OnClickInteractionRight += TryActivateObjectRight;
    }

    void Update()
    {
        foreach (var controller in playerToUpdateControllers)
        {
            controller.OnUpdate();
        }
    }

    private IInteractable GetInteractableObject()
    {
        int layy = ~(1<<(LayerMask.NameToLayer("Camera")));
        RaycastHit2D hit = Physics2D.GetRayIntersection(camer.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, layy);
        return hit.collider != null ? hit.collider.GetComponent<IInteractable>() : null;
    }
    
    public void TryActivateObjectLeft()
    {
        interactableObjectFocus = GetInteractableObject();
        
        if(interactableObjectFocus!=null)
        {
            interactableObjectFocus.OnLeftClickObject(this);
        }
    }

    public void TryActivateObjectRight()
    {
        interactableObjectFocus = GetInteractableObject();
        if (interactableObjectFocus != null)
        {
         //   Debug.Log(interactableObjectFocus);
            interactableObjectFocus.OnRightClickObject(this);
        }
    }

    public bool ExtractObject(ref float health)
    {
        //odległość

        health -= extractionTool*Time.deltaTime;
        Debug.Log(health);
        return health<=0;
    }

    private void OnApplicationQuit()
    {
        inventory.inventory.ResetInventoryObject();
    }

    // na odznaczenie, na zniszczenie, na oddalenie
    //funkcja do najeżdżania kursorem na obiekt
}
