using System;
using UnityEngine;

[Serializable]
public struct CanBreakObjectInfo
{
    public float startChance;
    public float endChance;
    public GameObject dropItem;
}
