using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  攻撃を追尾するクラス。
 *******************************************************************/
public class Homing : AttackBase
{
    [SerializeField]
    Rigidbody2D Rigidbody2D;
    GameObject m_target;
    const float ATTACK_SPEED = 6.0f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = (m_target.GetComponent<Rigidbody2D>().position - Rigidbody2D.position).normalized * ATTACK_SPEED;
    }


    public override void Attack()
    {

        var direction = m_target.GetComponent<IActor>().GetDirection();
        Rigidbody2D.position = transform.parent.parent.GetComponent<Rigidbody2D>().position;
        //transform.rotation = Quaternion.Euler(direction.x, direction.y, 0.0f);
        //Rigidbody2D.velocity = transform.rotation * direction * ATTACK_SPEED;

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
}
