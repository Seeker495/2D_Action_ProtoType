using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour, IActor
{
    Rigidbody2D m_rigidbody2D;

    [SerializeField]
    private Vector2 m_startPosition;
    private Vector2 m_velocity;
    private Vector2 m_direction;
    private Dictionary<Vector2, Tile> Sprites;
    private PlayerStatus m_status;
    private bool m_isDamaged = false;
    private List<AttackBase> m_weapons;

    private int WeaponIndex = 0;
    // Start is called before the first frame update
    async void Start()
    {
        m_status.actorStatus.hp = 7;
        m_status.actorStatus.attack = 1;
        m_status.actorStatus.defence = 1;
        m_status.actorStatus.speed = 3.0f;
        m_status.exp = m_status.money = 0;
        m_rigidbody2D = GetComponent<Rigidbody2D>();


        Sprites = new Dictionary<Vector2, Tile>(4)
        {
            {Vector2.up,await Addressables.LoadAssetAsync<Tile>("Characters_36").Task },
            {Vector2.right,await Addressables.LoadAssetAsync<Tile>("Characters_24").Task },
            {Vector2.left,await Addressables.LoadAssetAsync<Tile>("Characters_12").Task },
            {Vector2.down,await Addressables.LoadAssetAsync<Tile>("Characters_0").Task },
        };

        m_weapons = new List<AttackBase>(2)
        {
            GetComponentInChildren<Blade>(),
            GetComponentInChildren<Bow>(),
        };
    }



    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        m_velocity = move * m_status.actorStatus.speed;
        m_rigidbody2D.velocity = m_velocity;
        m_direction = m_rigidbody2D.velocity.normalized;
    }

    public void MoveEnd(InputAction.CallbackContext context)
    {
        m_velocity = Vector2.zero;
        m_rigidbody2D.velocity = m_velocity;
    }


    public void Dash(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                m_velocity *= 2;
                break;
            case InputActionPhase.Canceled:
                m_velocity /= 2;
                break;
        }
        m_rigidbody2D.velocity = m_velocity;

    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (m_rigidbody2D.velocity != Vector2.zero && m_weapons[WeaponIndex].GetAttackType().Equals(eAttackType.BLADE)) return;
        m_weapons[WeaponIndex].Attack();
    }

    public Sprite GetWeaponSprite()
    {
        return m_weapons[WeaponIndex].GetSprite();
    }

    public void SelectWeaponToLeft(InputAction.CallbackContext context)
    {
        WeaponIndex = System.Math.Abs(--WeaponIndex) % m_weapons.Count;
    }

    public void SelectWeaponToRight(InputAction.CallbackContext context)
    {
        WeaponIndex = System.Math.Abs(++WeaponIndex) % m_weapons.Count;
    }

    public void Resurrection(InputAction.CallbackContext context)
    {
        gameObject.SetActive(true);
        if (IsArrive()) return;
        gameObject.tag = "Player";
        m_status.actorStatus.hp = 10;
        m_isDamaged = false;
        GameObject.Find("PlayerController").GetComponent<PlayerController>().Player_Controller.Enable();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_isDamaged && !collision.gameObject.CompareTag("Wall") && !collision.gameObject.CompareTag("NormalObstacle") && !collision.gameObject.CompareTag("WaterObstacle")) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage(collision.transform.GetComponent<IActor>().GetBaseStatus().attack * 2);
            m_rigidbody2D.AddForce(collision.transform.GetComponent<Rigidbody2D>().velocity.normalized * Time.deltaTime);
        }
        if (!IsArrive())
            Dead();


    }

    IEnumerator KnockBack()
    {
        while (m_isDamaged && m_rigidbody2D.velocity.magnitude >= 0.2f)
        {
            m_rigidbody2D.velocity *= 0.8f;
            yield return null;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (m_isDamaged && !collision.gameObject.CompareTag("Wall") && !collision.gameObject.CompareTag("NormalObstacle") && !collision.gameObject.CompareTag("WaterObstacle")) return;

        if (collision.gameObject.CompareTag("Enemy"))
            m_rigidbody2D.velocity = Vector2.zero;

        if (!IsArrive())
            Dead();


    }



    private void OnCollisionStay2D(Collision2D collision)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_isDamaged && !collision.CompareTag("Wall") && !collision.CompareTag("NormalObstacle") && !collision.CompareTag("WaterObstacle")) return;
        if (collision.CompareTag("Magic"))
            Damage(collision.transform.parent.parent.GetComponent<IActor>().GetBaseStatus().attack * 3);

        if (!IsArrive())
            Dead();

    }



    public bool IsArrive()
    {
        return 0 < m_status.actorStatus.hp;
    }

    private void Dead()
    {
        gameObject.tag = "Untagged";
        GameObject.Find("PlayerController").GetComponent<PlayerController>().Player_Controller.Disable();
        gameObject.SetActive(false);

        //StartCoroutine(OnDead(0.1f, 0.3f));
    }

    public void SetMoveRange(ref Map map)
    {
        var range = map.GetEdgeRect();
        m_rigidbody2D.position = new Vector2(Mathf.Clamp(m_rigidbody2D.position.x, range.left, range.right), Mathf.Clamp(m_rigidbody2D.position.y, range.bottom, range.top));
    }

    public void SetSpawnPosition(ref Map map)
    {
        var data = map.GetMapData();
        m_startPosition = new Vector2(Random.Range(0, data.width), Random.Range(-data.height, 0));
        m_rigidbody2D.position = m_startPosition;
    }

    void Damage(in float attack = 0.0f)
    {
        int damage = Mathf.RoundToInt(attack - m_status.actorStatus.defence);
        m_status.actorStatus.hp -= damage;
        StartCoroutine(OnDamage(2.0f, 0.3f));
    }

    IEnumerator OnDamage(float duration, float interval)
    {
        m_isDamaged = true;
        StartCoroutine(KnockBack());
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
        m_isDamaged = false;
    }

    Vector2 IActor.GetDirection()
    {
        return m_direction;
    }

    ActorStatus IActor.GetBaseStatus()
    {
        return m_status.actorStatus;
    }

    public void AddExp(in int exp)
    {
        m_status.exp += exp * 2;
    }


    public void AddMoney(in int money)
    {
        m_status.money += money * 1;
    }

    public int GetMoney()
    {
        return m_status.money;
    }

    public int GetExp()
    {
        return m_status.exp;
    }

    public void HighSpeedMove(InputAction.CallbackContext context)
    {
        if (!GetComponent<HighSpeedMove>()) return;
        StartCoroutine(GetComponent<HighSpeedMove>().Move(gameObject, false));
    }
}
