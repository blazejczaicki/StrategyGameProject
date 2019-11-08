using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resources,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public GameObject icon;
    public Sprite sprite;
    public ItemType type;
    public int amount;
    [TextArea(15,20)] public string description;
}
