using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  攻撃を追尾するクラス。
 *******************************************************************/

public class AttackWithDirection : AttackBase
{
    [SerializeField]
    Rigidbody2D Rigidbody2D;
    GameObject m_target;
    const float ATTACK_SPEED = 6.0f;
    // Start is called before the first frame update
    void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }



    public override void Attack()
    {

        Rigidbody2D.position = transform.parent.GetComponent<Rigidbody2D>().position;
        var direction = (m_target.GetComponent<Rigidbody2D>().position - Rigidbody2D.position).normalized;
        transform.rotation = Quaternion.Euler(direction.x, direction.y, 0.0f);
        Rigidbody2D.velocity = transform.rotation * direction * ATTACK_SPEED;

    }

    public override void SetTarget(in GameObject target)
    {
        if (target == null) return;
        m_target = target;
    }

    public override eAttackType GetAttackType()
    {
        return eAttackType.HOMING;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blade") || collision.CompareTag("Arrow") || collision.CompareTag("Player"))
            Destroy(gameObject);
    }

    public override void Execute()
    {
        //Rigidbody2D.velocity = (m_target.GetComponent<Rigidbody2D>().position - Rigidbody2D.position).normalized * ATTACK_SPEED;
    }

    public override Sprite GetSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
            Destroy(gameObject);
    }

}

