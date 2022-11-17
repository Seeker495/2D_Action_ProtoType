using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  敵のパラメータを保存しておくクラス。
 *  (ScriptableObjectはUnity特有のものでデータベースとして保存する際に重宝する)
 *******************************************************************/
[CreateAssetMenu(fileName = "EnemyData", menuName = "CreateEnemyData")]
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
    [SerializeField]
    private int m_attackPattern;

    public int ID { get { return m_id; } set { m_id = value; } }
    public string Name { get { return m_name; } set { m_name = value; } }
    public int Exp { get { return m_exp; } set { m_exp = value; } }
    public int Money { get { return m_money; } set { m_money = value; } }
    public int HP { get { return m_hp; } set { m_hp = value; } }
    public float Attack { get { return m_attack; } set { m_attack = value; } }
    public float defence { get { return m_defence; } set { m_defence = value; } }
    public float Speed { get { return m_speed; } set { m_speed = value; } }
    public float TouchPower { get { return m_touchPower; } set { m_touchPower = value; } }
    public int MovePattern { get { return m_movePattern; } set { m_movePattern = value; } }

    public int AttackPattern { get { return m_attackPattern; } set { m_attackPattern = value; } }

}