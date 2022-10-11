using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour, IDropObject
{
    private Rigidbody2D m_rigidBody2D;
    private eDropSize m_dropSize;
    private Player m_player;
    private float m_arriveTime;

    private void Awake()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = Color.cyan;
        m_player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_arriveTime = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (transform.parent.parent == null)
            m_arriveTime += Time.deltaTime;
        if(m_arriveTime > 1.5f)
            m_rigidBody2D.velocity = (m_player.GetComponent<Rigidbody2D>().position - m_rigidBody2D.position).normalized * 4.0f;

        if (Vector2.Distance(m_player.GetComponent<Rigidbody2D>().position, m_rigidBody2D.position) <= 2.0f)
            m_rigidBody2D.velocity = (m_player.GetComponent<Rigidbody2D>().position - m_rigidBody2D.position).normalized * 2.8f;

    }

    int IDropObject.Get()
    {
        return (int)m_dropSize;
    }

    Vector2 IDropObject.GetPosition()
    {
        return m_rigidBody2D.position;
    }

    Vector2 IDropObject.GetVelocity()
    {
        return m_rigidBody2D.velocity;
    }

    void IDropObject.SetSize(in eDropSize size)
    {
        m_dropSize = size;
    }

}
