using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  アイテムのパラメータを保存しておくクラス。
 *******************************************************************/
[CreateAssetMenu(fileName = "TreasureData", menuName = "CreateTreasureData")]
public class TreasureParameter : ScriptableObject
{
    [SerializeField]
    private string m_name;
    [SerializeField]
    private Sprite m_sprite;
    [SerializeField]
    private long m_score;

    public string Name { get { return m_name; } set { m_name = value; } }
    public Sprite Sprite { get { return m_sprite; } set { m_sprite = value; } }
    public long Score { get { return m_score; } set { m_score = value; } }
}