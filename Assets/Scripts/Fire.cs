using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour,IAttack
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

    public void Attack()
    {
        //float angle = (Mathf.Atan2(Rigidbody2D.velocity.y, Rigidbody2D.velocity.x) + Mathf.PI / 2) * Mathf.Rad2Deg;
        //transform.localEulerAngles = new Vector3(0, 0, angle + Mathf.PI * Mathf.Rad2Deg);
    }

    void IAttack.Attack()
    {
        var direction = transform.parent.parent.GetComponent<IActor>().GetDirection();
        Rigidbody2D.position = transform.parent.parent.GetComponent<Rigidbody2D>().position;
        transform.rotation = Quaternion.Euler(direction.x, direction.y, 0.0f);
        Rigidbody2D.velocity = transform.rotation * direction * ATTACK_SPEED;
    }

    Sprite IAttack.GetSprite()
    {
        return null;
    }

    eAttackType IAttack.GetAttackType()
    {
        return eAttackType.FIRE;
    }
}
