using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  アイテムのパラメータを保存しておくクラス。
 *******************************************************************/
[CreateAssetMenu(fileName = "ItemData", menuName = "CreateItemData")]
public class ItemParameter : ScriptableObject
{
    [SerializeField]
    private string m_name;
    [SerializeField]
    private Sprite m_sprite;
    [SerializeField]
    private float m_healRatio;

    public string Name { get { return m_name; } set { m_name = value; } }
    public Sprite Sprite { get { return m_sprite; } set { m_sprite = value; } }
    public float HealRatio { get { return m_healRatio; } set { m_healRatio = value; } }
}