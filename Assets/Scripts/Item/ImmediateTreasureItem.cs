using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <�T�v>
 *  �A�J���Ɋ֘A����A�C�e���̊��N���X"�y��"�����A�C�e���̔h���N���X�B
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override TreasureInfo GetInfo()
    {
        return m_treasureInfo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Destroy(gameObject);
    }

}