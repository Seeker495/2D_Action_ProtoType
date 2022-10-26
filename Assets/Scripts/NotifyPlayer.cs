using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  プレイヤーを感知するクラス。(敵に持たせている)
 *  <仕組み>
 *  一定範囲内にプレイヤーが入った場合,攻撃をしてきたり,追尾してきたりする
 *******************************************************************/
public class NotifyPlayer : MonoBehaviour
{
    [SerializeField]
    private EnemyBase m_enemy;
    private Rigidbody2D m_rigidBody2D;
    private Player m_player;
    void Awake()
    {
        m_enemy = transform.parent.GetComponent<EnemyBase>();
        m_player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(m_enemy.GetComponent<Rigidbody2D>().position, m_player.GetComponent<Rigidbody2D>().position) <= 10.0f)
            m_enemy.AttachNotify();
        else
            m_enemy.DetachNotify();
        if (m_enemy.IsNotified())
            m_enemy.Chasing(m_player.gameObject);
    }

    private void FixedUpdate()
    {
        m_rigidBody2D.position = m_enemy.GetComponent<Rigidbody2D>().position;
    }

}
