using System;
using UnityEngine;

[Serializable]
public struct ItemInfo
{
    public string name;
    [Range(0.0f, 1.0f)]
    public float chance;
}
