using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  腹ペコに関連するアイテムの基底クラス"及び"即時アイテムの派生クラス。
 *******************************************************************/
public class ImmediateFoodItem : ImmediateItemBase<FoodInfo>
{
    [SerializeField]
    private ItemParameter m_itemParamter;
    private FoodInfo m_foodInfo;

    private void Awake()
    {
        m_foodInfo.name = m_itemParamter.Name;
        m_foodInfo.healRatio = m_itemParamter.HealRatio;
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
    public override FoodInfo GetInfo()
    {
        return m_foodInfo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Destroy(gameObject);
    }

}
