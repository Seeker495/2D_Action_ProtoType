using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  攻撃を追尾するクラス。
 *******************************************************************/

public class Attack_EightDirection_Homing : AttackBase
{
    [SerializeField]
    Rigidbody2D Rigidbody2D;
    GameObject m_target;
    const float ATTACK_SPEED = 6.0f;
    const float START_HOMING = 1.0f;
    float time = 0.0f;
    // Start is called before the first frame update
    void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }



    public override void Attack()
    {
        Rigidbody2D.position = transform.parent.GetComponent<Rigidbody2D>().position;
        var direction = Vector2.up;
        Rigidbody2D.velocity = transform.rotation * direction * ATTACK_SPEED;
        Debug.Log(Rigidbody2D.velocity);
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
        if (!Rigidbody2D || !m_target) return;
        time += Time.deltaTime;
        if(time > START_HOMING)
            Rigidbody2D.velocity = (m_target.GetComponent<Rigidbody2D>().position - Rigidbody2D.position).normalized * ATTACK_SPEED;
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

