using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData",menuName ="CreateEnemyData")]
public class EnemyParameter : ScriptableObject
{

    [SerializeField]
    private int m_maxHP;
    [SerializeField]
    private float m_attack;
    [SerializeField]
    private float m_defence;
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private int m_exp;
    [SerializeField]
    private int m_money;
    [SerializeField]
    private List<ItemInfo> m_items;

    public int MaxHP => m_maxHP;
    public float Attack => m_attack;
    public float Defence => m_defence;
    public float Speed => m_speed;
    public int Exp => m_exp;
    public int Money => m_money;
    public List<ItemInfo> Items => m_items;

}
