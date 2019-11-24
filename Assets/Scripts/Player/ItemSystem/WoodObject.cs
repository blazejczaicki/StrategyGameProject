using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wood")]
public class WoodObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Wood;
    }
}
