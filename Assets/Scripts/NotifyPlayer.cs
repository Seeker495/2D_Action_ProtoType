using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyPlayer : MonoBehaviour
{
    [SerializeField]
    private Enemy Enemy;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            Enemy.AttachNotify();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Enemy.Chasing(collision.gameObject);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Enemy.DetachNotify();
    }

}
