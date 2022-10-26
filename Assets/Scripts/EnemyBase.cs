using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IActor
{
    private EnemyStatus m_status;
    public EnemyParameter m_enemyParameter;
    public Rigidbody2D m_rigidBody2D;
    private bool m_isNotified = false;
    MagicManager m_magicManager;
    private const float INTERVAL = 1.0f;
    private float time = 0.0f;
    private Vector2 m_direction;

    public void Awake()
    {
        m_status.id = m_enemyParameter.ID;
        m_status.name = m_enemyParameter.Name;
        m_status.actorStatus.hp = m_enemyParameter.HP;
        m_status.actorStatus.attack = m_enemyParameter.Attack;
        m_status.actorStatus.defense = m_enemyParameter.Defense;
        m_status.actorStatus.speed = m_enemyParameter.Speed;
        m_status.actorStatus.touchPower = m_enemyParameter.TouchPower;
        m_status.exp = m_enemyParameter.Exp;
        m_status.money = m_enemyParameter.Money;
        m_magicManager = GetComponentInChildren<MagicManager>();
        m_rigidBody2D = GetComponent<Rigidbody2D>();

    }

    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (m_isNotified)
            time += Time.deltaTime;
        else
            time = 0.0f;

        if (m_isNotified && time >= INTERVAL && IsArrive())
        {
            m_direction = m_rigidBody2D.velocity.normalized;
            m_magicManager.Attack();
            time = 0.0f;
        }
    }

    public void FixedUpdate()
    {
        if (!m_isNotified)
            m_rigidBody2D.velocity = Vector2.zero;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Notify")) return;

        //if (collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Blade"))
        //    Damage(1);
        //if (!IsArrive())
        //    Dead();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
            Damage(collision.transform.parent.parent.GetComponent<IActor>().GetBaseStatus().attack * 2);
        if (collision.gameObject.CompareTag("Blade"))
            Damage(collision.transform.parent.GetComponent<IActor>().GetBaseStatus().attack * 3);
        if (!IsArrive())
            Dead();

    }


    private bool IsArrive()
    {
        return 0 < m_status.actorStatus.hp;
    }

    private void Dead()
    {
        StartCoroutine(OnDead(1.0f, 0.3f));
    }

    void Damage(in float attack = 0.0f)
    {
        int damage = Mathf.RoundToInt(attack - m_status.actorStatus.defense);
        m_status.actorStatus.hp -= damage;
        StartCoroutine(OnDamage(0.1f, 0.3f));
    }

    IEnumerator OnDamage(float duration, float interval)
    {
        bool changed = false;
        int inter = 0;
        while (duration > 0.0f)
        {
            inter++;
            duration -= Time.deltaTime;
            if (inter % 30 == 0)
                changed = !changed;
            if (changed)
                GetComponent<SpriteRenderer>().color = Color.red;
            else
                GetComponent<SpriteRenderer>().color = Color.white;
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator OnDead(float duration, float interval)
    {
        m_rigidBody2D.velocity = Vector2.zero;
        var dropManager = GetComponentInChildren<DropManager>();
        dropManager.enabled = true;
        StartCoroutine(dropManager.Diffusion());
        dropManager.transform.position = transform.position;
        dropManager.transform.SetParent(null, true);

        while (duration > 0.0f)
        {
            duration -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(Time.deltaTime);
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        GetComponent<BoxCollider2D>().enabled = false;


        gameObject.SetActive(false);
        Destroy(gameObject);
    }


    public void SetPosition(ref float minX, ref float maxX, ref float minY, ref float maxY)
    {
        m_rigidBody2D.position = new Vector2(Mathf.Clamp(m_rigidBody2D.position.x, minX, maxX), Mathf.Clamp(m_rigidBody2D.position.y, minY, maxY));
    }

    public void AttachNotify()
    {
        m_isNotified = true;
    }
    public void Chasing(in GameObject chaseTarget)
    {
        m_rigidBody2D.velocity = (chaseTarget.GetComponent<Rigidbody2D>().position - m_rigidBody2D.position).normalized * m_status.actorStatus.speed;
        if (!chaseTarget.GetComponent<Player>().IsArrive())
            StopChasing();
    }

    public void StopChasing()
    {
        m_rigidBody2D.velocity = Vector2.zero;
        DetachNotify();
    }

    public void DetachNotify()
    {
        m_isNotified = false;
    }

    public bool IsNotified()
    {
        return m_isNotified;
    }

    Vector2 IActor.GetDirection()
    {
        return m_direction;
    }

    ActorStatus IActor.GetBaseStatus()
    {
        return m_status.actorStatus;
    }

    public int GetExp()
    {
        return m_status.exp;
    }

    public int GetMoney()
    {
        return m_status.money;
    }

    public void HighSpeedMove()
    {
        if (!GetComponent<HighSpeedMove>()) return;
        StartCoroutine(GetComponent<HighSpeedMove>().Move(gameObject, false));
    }

}
