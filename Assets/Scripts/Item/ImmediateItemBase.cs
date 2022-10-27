using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  即時アイテムの基底クラス
 *  <仕組み>
 *  アイテムがステータスに与える情報を構造体ごとに分けるため,ジェネリック化している
 *******************************************************************/
public abstract class ImmediateItemBase<ItemInfo> : MonoBehaviour
{
    public abstract ItemInfo GetInfo();
}
