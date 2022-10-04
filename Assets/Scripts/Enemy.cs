using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IActor
{
    private int HP = 2;
    public Rigidbody2D Rigidbody;
    private bool m_isNotified = false;
    private const float ENEMY_SPEED = 2.5f;
    MagicManager MagicManager;
    private const float INTERVAL = 1.0f;
    private float time = 0.0f;
    private Vector2 m_direction;
    void Start()
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        MagicManager = GetComponentInChildren<MagicManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (m_isNotified)
            time += Time.deltaTime;
        else
            time = 0.0f;
        if(m_isNotified && time >= INTERVAL)
        {
            m_direction = Rigidbody.velocity.normalized;
            MagicManager.Attack();
            time = 0.0f;
        }
    }

    public void FixedUpdate()
    {
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
        if (collision.gameObject.CompareTag("Notify")) return;
        if (collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Blade"))
            Damage(1);
        if (!IsArrive())
            Dead();

    }


    private bool IsArrive()
    {
        return 0 < HP;
    }

    private void Dead()
    {
        StartCoroutine(OnDead(0.1f, 0.3f));
    }

    void Damage(in int damage = 0)
    {
        HP -= damage;
        StartCoroutine(OnDamage(0.1f, 0.3f));
    }

    IEnumerator OnDamage(float duration,float interval)
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
        var particleSystem = GetComponentInChildren<ParticleSystem>();
        while (duration > 0.0f)
        {
            duration -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(Time.deltaTime);
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        particleSystem.Play();
        if (particleSystem.isPlaying)
        {
            particleSystem.transform.SetParent(transform.parent, false);
            particleSystem.transform.position = transform.position;
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        gameObject.SetActive(false);
        if (particleSystem.isStopped)
            Destroy(gameObject);
    }


    public void SetPosition(ref float minX, ref float maxX, ref float minY, ref float maxY)
    {
        Rigidbody.position = new Vector2(Mathf.Clamp(Rigidbody.position.x, minX, maxX), Mathf.Clamp(Rigidbody.position.y, minY, maxY));
    }

    public void AttachNotify()
    {
        m_isNotified = true;
    }
    public void Chasing(in GameObject chaseTarget)
    {
        Rigidbody.velocity = (chaseTarget.GetComponent<Rigidbody2D>().position - Rigidbody.position).normalized * ENEMY_SPEED;
        if (!chaseTarget.GetComponent<Player>().IsArrive())
            StopChasing();
    }

    public void StopChasing()
    {
        Rigidbody.velocity = Vector2.zero;
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
}
