using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    const float ARROW_SPEED = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot(Vector2 startPosition, float degree, Vector2 direction)
    {
        rb.position = startPosition;
        transform.rotation = Quaternion.Euler(0, 0, -degree);
        rb.velocity = transform.rotation * direction * ARROW_SPEED;
        float angle = (Mathf.Atan2(rb.velocity.y, rb.velocity.x) + Mathf.PI / 2) * Mathf.Rad2Deg;
        transform.localEulerAngles = new Vector3(0, 0, angle + Mathf.PI * Mathf.Rad2Deg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("NormalObstacle"))
            Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
            Destroy(gameObject);
    }

}
