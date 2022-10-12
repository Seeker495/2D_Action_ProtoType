using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : AttackBase
{
    [SerializeField]
    Rigidbody2D Rigidbody2D;
    const float ATTACK_SPEED = 6.0f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public override void Attack()
    {
        var direction = transform.parent.parent.GetComponent<IActor>().GetDirection();
        Rigidbody2D.position = transform.parent.parent.GetComponent<Rigidbody2D>().position;
        transform.rotation = Quaternion.Euler(direction.x, direction.y, 0.0f);
        Rigidbody2D.velocity = transform.rotation * direction * ATTACK_SPEED;
    }

    public override eAttackType GetAttackType()
    {
        return eAttackType.FIRE;
    }
}
