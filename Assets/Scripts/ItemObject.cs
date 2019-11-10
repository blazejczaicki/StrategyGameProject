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
    public Sprite sprite;
    public ItemType type;
    [TextArea(15,20)] public string description;
}
