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

    public void Shoot(Vector2 startPosition, float degree,Vector2 direction)
    {
        rb.position = startPosition;
        transform.rotation = Quaternion.Euler(0, 0, -degree);
        rb.velocity = transform.rotation * direction * ARROW_SPEED;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        Destroy(gameObject);
    }

}
