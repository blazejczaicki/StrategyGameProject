using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterObjectController
{
    [SerializeField] private Camera camer;
    [SerializeField] private List<Controller> playerToUpdateControllers;
    [SerializeField] private PlayerInputController inputCon;

    [SerializeField] private float extractionTool = 1.0f; //potem item z inventory
    public InventoryController inventory;
    private IInteractable interactableObjectFocus = null;
    private IInteractable interactableObjectOnCoursor = null;

    protected override void Awake()
    {
        base.Awake();
        _type = CharacterTypes.player;
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        inputCon.OnClickInteractionLeft += TryActivateObjectLeft;
        inputCon.OnClickInteractionRight += TryActivateObjectRight;
    }

    private void OnCursorAboveObject()
    {
        var interactableObject = GetInteractableObject();

        if ( interactableObjectOnCoursor!=interactableObject && interactableObjectOnCoursor!=null)
        {
            interactableObjectOnCoursor.OnExitCursor();
            interactableObjectOnCoursor = null;
        }

        if (interactableObject!=null)
        {
            interactableObjectOnCoursor = interactableObject;
            interactableObject.OnCursor();
        }
    }

    private IInteractable GetInteractableObject()
    {
        int layy = ~(1 << (LayerMask.NameToLayer("Camera")));
        RaycastHit2D hit = Physics2D.GetRayIntersection(camer.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, layy);
        return hit.collider != null ? hit.collider.GetComponent<IInteractable>() : null;
    }

    public void TryActivateObjectLeft()
    {
        interactableObjectFocus = GetInteractableObject();

        if (interactableObjectFocus != null)
        {
            interactableObjectFocus.OnLeftClickObject(this);
        }
    }

    public void TryActivateObjectRight()
    {
        interactableObjectFocus = GetInteractableObject();
        if (interactableObjectFocus != null)
        {
            interactableObjectFocus.OnRightClickObject(this);
        }
    }

    public bool ExtractObject(ref float health)
    {
        health -= extractionTool * Time.deltaTime;
       // Debug.Log(health);
        return health <= 0;
    }

    public void TryAttack(CharacterObjectController target)
    {
        if (Vector2.Distance(target.transform.position, transform.position) < weapon.range)
        {
            weapon.OnAttack(target, combatController);
          //  combatController.Attack(target.stats, weapon);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.inventoryObject.ResetInventoryObject();
    }

    public override void OnUpdate()
    {
        _currentChunk = CheckOnWhichChunkYouStayed(transform.position);
        foreach (var controller in playerToUpdateControllers)
        {
            controller.OnUpdate();
        }
        OnCursorAboveObject();
        movement.Move(inputCon.MovementDirection, currentChunk);
        OnDead();
    }

    protected override void OnDead()
    {
        if (stats.health <= 0)
        {
            transform.position = Vector3.zero;
            stats.health = 20;
        }
    }

    // na odznaczenie, na zniszczenie, na oddalenie
    //funkcja do najeżdżania kursorem na obiekt
}
