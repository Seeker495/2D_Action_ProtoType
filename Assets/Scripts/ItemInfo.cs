using System;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  アイテム情報を示す構造体。
 *  <情報>
 *  名前&確率
 *******************************************************************/

[Serializable]
public struct ItemInfo
{
    public string name;
    [Range(0.0f, 1.0f)]
    public float chance;
}
