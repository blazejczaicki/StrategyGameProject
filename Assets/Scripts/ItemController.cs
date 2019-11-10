using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemController : MonoBehaviour, IInteractable
{
    public ItemObject itemObject;
    [SerializeField] private float pickUpRange=5.0f;
    [SerializeField] private int amount = 5;

    private void PickUp(PlayerController playerController)
    {
        if (playerController.inventory.AddItem(itemObject, amount))
        {
            Destroy(gameObject); // może pull kiedyś
        }
        else
            Debug.Log("Brak miejsca");
    }

    public void OnLeftClickObject(PlayerController playerController) //przytrzymanie i przerzucanie po ziemi
    {
        
    }

    public void OnRightClickObject(PlayerController playerController) //zbieranie
    {
        if (Vector3.Distance(transform.position, playerController.transform.position) < pickUpRange)
        {
            PickUp(playerController);
        }        
    }
    
}
