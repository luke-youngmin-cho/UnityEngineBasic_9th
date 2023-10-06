using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public ItemID id;
    public Sprite icon;
    public string description;
    public int numMax;
    public uint buyPrice;
    public uint sellPrice;
    public GameObject model;
}
