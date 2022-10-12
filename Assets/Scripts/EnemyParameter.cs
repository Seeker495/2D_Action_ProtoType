using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData",menuName ="CreateEnemyData")]
public class EnemyParameter : ScriptableObject
{
    [SerializeField]
    private int m_id;
    [SerializeField]
    private string m_name;
    [SerializeField]
    private int m_exp;
    [SerializeField]
    private int m_money;
    [SerializeField]
    private int m_hp;
    [SerializeField]
    private float m_attack;
    [SerializeField]
    private float m_defence;
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private float m_touchPower;
    [SerializeField]
    private int m_movePattern;

    public int ID => m_id;
    public string Name => m_name;
    public int Exp => m_exp;
    public int Money => m_money;
    public int HP => m_hp;
    public float Attack => m_attack;
    public float Defence => m_defence;
    public float Speed => m_speed;
    public float TouchPower => m_touchPower;
    public int MovePattern => m_movePattern;
}
