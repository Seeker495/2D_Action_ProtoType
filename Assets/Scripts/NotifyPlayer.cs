using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyPlayer : MonoBehaviour
{
    [SerializeField]
    private Enemy Enemy;
    private Rigidbody2D m_rigidBody2D;
    private Player m_player;
    void Start()
    {
        m_player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(Enemy.GetComponent<Rigidbody2D>().position, m_player.GetComponent<Rigidbody2D>().position) <= 10.0f)
            Enemy.AttachNotify();
        else
            Enemy.DetachNotify();
        if (Enemy.IsNotified())
            Enemy.Chasing(m_player.gameObject);
    }

    private void FixedUpdate()
    {
        m_rigidBody2D.position = Enemy.GetComponent<Rigidbody2D>().position;
    }

}
