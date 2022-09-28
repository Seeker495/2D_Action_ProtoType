using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int HP = 2;
    public Rigidbody2D Rigidbody;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        GetComponentInChildren<ParticleSystem>().Stop();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Blade"))
        //    Damage(1);
        //if (!IsArrive())
        //    Dead();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        while(duration > 0.0f)
        {
            duration -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(Time.deltaTime);
            GetComponent<SpriteRenderer>().color = Color.white;
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
        if (particleSystem.isStopped)
            Destroy(gameObject);

    }


    public void SetPosition(ref float minX, ref float maxX, ref float minY, ref float maxY)
    {
        Rigidbody.position = new Vector2(Mathf.Clamp(Rigidbody.position.x, minX, maxX), Mathf.Clamp(Rigidbody.position.y, minY, maxY));
    }

}
