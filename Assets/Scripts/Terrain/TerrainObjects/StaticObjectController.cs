using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StaticObjectController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject ItemToDrop;
    [SerializeField] private float health = 10.0f;
    [SerializeField] private float interactRange = 5.0f;

    //public Func<int, bool> OnExtractObject; //do ref własny delegat

    public void DropItem(PlayerController playerController)
    {
        if (playerController.ExtractObject(ref health))
        {
            Instantiate(ItemToDrop, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void OnLeftClickObject(PlayerController playerController)
    {

    }

    public void OnRightClickObject(PlayerController playerController)
    {
        if (Vector3.Distance(transform.position, playerController.transform.position)<interactRange)
        {
            DropItem(playerController);
        }
    }
}
