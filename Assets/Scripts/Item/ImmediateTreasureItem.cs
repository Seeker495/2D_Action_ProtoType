using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  喉カレに関連するアイテムの基底クラス"及び"即時アイテムの派生クラス。
 *******************************************************************/
public class ImmediateTreasureItem : ImmediateItemBase<TreasureInfo>
{
    [SerializeField]
    private TreasureParameter m_itemParamter;
    private TreasureInfo m_treasureInfo;

    private void Awake()
    {
        m_treasureInfo.name = m_itemParamter.Name;
        m_treasureInfo.score = m_itemParamter.Score;
        GetComponent<SpriteRenderer>().sprite = m_itemParamter.Sprite;
    }
    public override TreasureInfo GetInfo()
    {
        return m_treasureInfo;
    }
}
