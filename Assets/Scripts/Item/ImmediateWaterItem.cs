using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  喉カレに関連するアイテムの基底クラス"及び"即時アイテムの派生クラス。
 *******************************************************************/
public class ImmediateWaterItem : ImmediateItemBase<WaterInfo>
{
    [SerializeField]
    private ItemParameter m_itemParamter;
    private WaterInfo m_waterInfo;

    private void Awake()
    {
        m_waterInfo.name = m_itemParamter.Name;
        m_waterInfo.healRatio = m_itemParamter.HealRatio;
        GetComponent<SpriteRenderer>().sprite = m_itemParamter.Sprite;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override WaterInfo GetInfo()
    {
        return m_waterInfo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Destroy(gameObject);
    }

}
