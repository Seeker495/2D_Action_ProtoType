using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour,IWeapon
{
    [SerializeField]
    Rigidbody2D Rigidbody2D;
    const float ATTACK_SPEED = 6.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(Vector2 startPosition, Vector2 direction)
    {
        Rigidbody2D.position = startPosition;
        transform.rotation = Quaternion.Euler(direction.x, direction.y, 0.0f);
        Rigidbody2D.velocity = transform.rotation * direction * ATTACK_SPEED;
        //float angle = (Mathf.Atan2(Rigidbody2D.velocity.y, Rigidbody2D.velocity.x) + Mathf.PI / 2) * Mathf.Rad2Deg;
        //transform.localEulerAngles = new Vector3(0, 0, angle + Mathf.PI * Mathf.Rad2Deg);
    }

    public Sprite GetSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }

    public string GetTagName()
    {
        return gameObject.tag;
    }
}
