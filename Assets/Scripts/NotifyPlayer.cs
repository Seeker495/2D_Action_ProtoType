using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyPlayer : MonoBehaviour
{
    [SerializeField]
    private Enemy Enemy;
    private Rigidbody2D m_rigidBody2D;
    void Start()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        m_rigidBody2D.position = Enemy.GetComponent<Rigidbody2D>().position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            Enemy.AttachNotify();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Enemy.IsNotified() && collision.CompareTag("Player"))
            Enemy.Chasing(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Enemy.DetachNotify();
    }

}
