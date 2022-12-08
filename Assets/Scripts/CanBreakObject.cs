using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanBreakObject : MonoBehaviour
{
    [SerializeField]
    private CanBreakInfo m_info;
    private int m_hp;

    private void Awake()
    {
        m_hp = m_info.HP;
        GetComponent<SpriteRenderer>().sprite = m_info.Sprite;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_hp <= 0)
            Break();
    }


    public void Break()
    {
        float chance = Random.Range(0.01f, 1.0f);
        GameObject dropObject = m_info.DropItems.Find(dropItem => dropItem.startChance <= chance && chance <= dropItem.endChance).dropItem;
        if(dropObject != null)
            Instantiate(dropObject, transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Weapon"))
        {
            Damage(Mathf.RoundToInt(collision.collider.GetComponentInParent<IActor>().GetBaseStatus().attack));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Arrow"))
        {
            Damage(Mathf.RoundToInt(collision.GetComponentInParent<IActor>().GetBaseStatus().attack));
        }
    }


    void Damage(int damage)
    {
        m_hp -= damage;
    }
}
