﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemController : MonoBehaviour, IInteractable
{
    public ItemObject itemObject;
    [SerializeField] private float pickUpRange=5.0f;
    public int amount = 5;


    private void PickUp(PlayerController playerController)
    {
        if (playerController.inventory.AddItem(itemObject, amount))
        {
            Destroy(gameObject); 
        }
        else
            Debug.Log("Brak miejsca");
    }

    public void OnLeftClickObject(PlayerController playerController) 
    {
        
    }

    public void OnRightClickObject(PlayerController playerController) //zbieranie
    {
        if (Vector3.Distance(transform.position, playerController.transform.position) < pickUpRange)
        {
            PickUp(playerController);
        }        
    }

    public void OnCursor() { }
    public void OnExitCursor() { }
}
