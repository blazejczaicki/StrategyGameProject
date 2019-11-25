using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resources,
    Wood,
    Stone,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public GameObject gameObject;
    public Sprite sprite;
    public ItemType type;
    [TextArea(15,20)] public string description;
}
