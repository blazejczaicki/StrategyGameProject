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
    public float burningSpeed = 0;
    [TextArea(15,20)] public string description;
}
