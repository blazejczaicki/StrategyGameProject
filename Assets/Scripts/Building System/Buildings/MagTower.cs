using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagTower : Building, IInteractable
{
    [SerializeField] private GameObject towerMenu;
    [SerializeField] private PlayerInputController player;

    private void Awake()
    {
        player.UIElements1.Add(towerMenu);
    }

    public void OnLeftClickObject(PlayerController controller)
    {
       towerMenu.SetActive(true);
    }

    public void OnRightClickObject(PlayerController controller)
    {    }
}
