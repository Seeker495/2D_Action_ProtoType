using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : AttackBase
{
    private Rigidbody2D m_rigidBody2D;
    private CircleCollider2D m_circleCollider2D;

    public override void Attack()
    {
        StartCoroutine(Attacking());
    }

    public override void Execute()
    {
    }

    public override eAttackType GetAttackType()
    {
        return eAttackType.GRENADE;
    }

    public override Sprite GetSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }

    public override void SetTarget(in GameObject target = null)
    {
    }

    private IEnumerator Attacking()
    {
        var direction = GameObject.FindWithTag("Player").GetComponent<IActor>().GetDirection();
        var startPosition = m_rigidBody2D.position;

        while(true)
        {
            var afterPosition = Vector2.MoveTowards(startPosition, startPosition + direction * Parameter.ATTACK_BOMB_THROW_DISTANCE, 1.0f);
            m_rigidBody2D.position = afterPosition;
            if (Vector2.Distance(m_rigidBody2D.position,startPosition + direction * Parameter.ATTACK_BOMB_THROW_DISTANCE) <= 0.0f)
                break;
            yield return null;
        }
    }

    private void Awake()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_circleCollider2D = GetComponent<CircleCollider2D>();
        m_circleCollider2D.radius = Parameter.ATTACK_BOMB_RANGE * 0.5f;
        m_circleCollider2D.enabled = false;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
