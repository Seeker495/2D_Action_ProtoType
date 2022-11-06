using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

/*******************************************************************
 *  <概要>
 *  ユーザーが扱うプレイヤーのクラス。
 *  <やれる事>
 *  移動や攻撃など通常のゲームで出来る事が出来る。
 *******************************************************************/
public class Player : MonoBehaviour, IActor
{
    // 衝突判定
    Rigidbody2D m_rigidbody2D;

    [SerializeField]
    private Vector2 m_startPosition;
    private Vector2 m_velocity;
    private Vector2 m_direction;
    private Dictionary<Vector2, Tile> Sprites;
    private PlayerStatus m_status;
    private bool m_isDamaged = false;
    private List<AttackBase> m_weapons;
    private float m_noWaterTime = 0.0f;
    private bool m_haveFood = true;
    private int WeaponIndex = 0;
    private float[] m_stageTime = new float[2];
    private const float DAMAGE_INTERVAL = 2.0f;
    private const float GAUGE_DECREASE_INTERVAL = 3.0f;
    // Start is called before the first frame update

    // サウンドマネージャー
    public SoundManager_2 soundManager_2;

    // ピンチの時に流すseのフラグ
    bool pinchiFlag = false;

    async void Awake()
    {
        // ステータスごとの初期化
        m_status.actorStatus.hp = m_status.maxHP = Parameter.PLAYER_MAX_HP;
        m_status.actorStatus.attack = m_status.maxAttack = Parameter.PLAYER_INIT_ATTACK;
        m_status.actorStatus.defence = m_status.maxdefence = Parameter.PLAYER_INIT_DEFENCE;
        m_status.actorStatus.speed = Parameter.PLAYER_NORMAL_VELOCITY;
        m_status.exp = m_status.money = 0;
        m_status.foodGauge = Parameter.FOOD_GAUGE_MAX;
        m_status.waterGauge = Parameter.WATER_GAUGE_MAX;
        m_rigidbody2D = GetComponent<Rigidbody2D>();

        // 武器リスト
        m_weapons = new List<AttackBase>(2)
        {
            GetComponentInChildren<Blade>(),
            GetComponentInChildren<Bow>(),
        };
    }



    // Update is called once per frame
    void Update()
    {
        if (!HaveWater())
            Debuff_HP(Parameter.WATER_GAUGE_DECREASE_HP_INTERVAL);
        m_haveFood = HaveFood();
        Debuff_Status(m_haveFood);
        if (!IsArrive())
            Dead();
        m_status.foodGauge = (int)Mathf.Clamp(m_status.foodGauge, 0, Parameter.FOOD_GAUGE_MAX);
        m_status.waterGauge = (int)Mathf.Clamp(m_status.waterGauge, 0, Parameter.WATER_GAUGE_MAX);

        // 危険の音を鳴らす
        if (!pinchiFlag && m_status.actorStatus.hp < 30)
        {
            pinchiFlag = true;
            soundManager_2.PlaySe(8);
        }
        else if(pinchiFlag && m_status.actorStatus.hp > 31)
        {
            pinchiFlag = false;
        }


    }

    private void FixedUpdate()
    {
        DecreaseGauge();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        m_velocity = move * m_status.actorStatus.speed;
        m_rigidbody2D.velocity = m_velocity;
        m_direction = m_rigidbody2D.velocity.normalized;
    }

    // 移動終わり
    public void MoveEnd(InputAction.CallbackContext context)
    {
        m_velocity = Vector2.zero;
        m_rigidbody2D.velocity = m_velocity;
    }

    // ダッシュ
    public void Dash(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                m_velocity *= Parameter.PLAYER_DASH_MULTIPLY;

                // 早い移動の音
                soundManager_2.PlaySe(6);
                break;
            case InputActionPhase.Canceled:
                m_velocity *= 1.0f / Parameter.PLAYER_DASH_MULTIPLY;
                break;
        }
        m_rigidbody2D.velocity = m_velocity;

    }


    public void Attack(InputAction.CallbackContext context)
    {
        if (m_rigidbody2D.velocity != Vector2.zero && m_weapons[WeaponIndex].GetAttackType().Equals(eAttackType.BLADE)) return;
        m_weapons[WeaponIndex].Attack();

        // WeaponIndex に応じた武器の音を出す
        if(WeaponIndex == 0)
        {
            soundManager_2.PlaySe(0);
        }
        if(WeaponIndex == 1)
        {
            soundManager_2.PlaySe(1);
        }
    }

    public Sprite GetWeaponSprite()
    {
        return m_weapons[WeaponIndex].GetSprite();
    }

    public void SelectWeaponToLeft(InputAction.CallbackContext context)
    {
        WeaponIndex = System.Math.Abs(--WeaponIndex) % m_weapons.Count;

        // 武器チェンジの音
        soundManager_2.PlaySe(2);
    }

    public void SelectWeaponToRight(InputAction.CallbackContext context)
    {
        WeaponIndex = System.Math.Abs(++WeaponIndex) % m_weapons.Count;

        // 武器チェンジの音
        soundManager_2.PlaySe(2);
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



    }



    private void OnCollisionStay2D(Collision2D collision)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_isDamaged && !collision.CompareTag("Wall") && !collision.CompareTag("NormalObstacle") && !collision.CompareTag("WaterObstacle")) return;
        if (collision.CompareTag("Magic"))
            Damage(collision.transform.parent.GetComponent<IActor>().GetBaseStatus().attack * 3);

        if (collision.CompareTag("FoodItem"))
        {
            m_status.foodGauge +=
                (int)Mathf.Clamp(
                    Parameter.FOOD_GAUGE_MAX * collision.GetComponent<ImmediateFoodItem>().GetInfo().healRatio,
                    -Parameter.FOOD_GAUGE_MAX,
                    Parameter.FOOD_GAUGE_MAX);
            m_stageTime[1] = 0.0f;

        }

        if (collision.CompareTag("WaterItem"))
        {
            m_status.waterGauge +=
                (int)Mathf.Clamp(
                    Parameter.WATER_GAUGE_MAX * collision.GetComponent<ImmediateWaterItem>().GetInfo().healRatio,
                    -Parameter.WATER_GAUGE_MAX,
                    Parameter.WATER_GAUGE_MAX);
            m_stageTime[0] = 0.0f;

        }

    }

    public bool IsArrive()
    {
        return 0 < m_status.actorStatus.hp;
    }

    public bool HaveWater()
    {
        return 0.0f < m_status.waterGauge;
    }

    private void Debuff_HP(in float interval)
    {
        m_noWaterTime += Time.deltaTime;
        if (m_noWaterTime > interval)
        {
            m_status.actorStatus.hp -= (int)System.Math.Round(m_status.maxHP * Parameter.WATER_GAUGE_DECREASE_RATIO_HP);
            m_noWaterTime = 0.0f;
        }

    }

    private void DecreaseGauge()
    {
        if ((m_stageTime[0] += Time.deltaTime) > Parameter.WATER_GAUGE_DECREASE_INTERVAL)
        {
            m_status.waterGauge -= Parameter.WATER_GAUGE_DECREASE;
            m_stageTime[0] = 0.0f;
        }

        if ((m_stageTime[1] += Time.deltaTime) > Parameter.FOOD_GAUGE_DECREASE_INTERVAL)
        {
            m_status.foodGauge -= Parameter.FOOD_GAUGE_DECREASE;
            m_stageTime[1] = 0.0f;
        }
    }

    private void Debuff_Status(bool haveFood)
    {
        float attack = m_status.maxAttack;
        float defence = m_status.maxdefence;


        if (!haveFood)
        {
            attack *= (1f - Parameter.FOOD_GAUGE_DECREASE_RATIO_ATTACK);
            defence *= (1f - Parameter.FOOD_GAUGE_DECREASE_RATIO_DEFENCE);
        }
        else
        {
            attack = m_status.maxAttack;
            defence = m_status.maxdefence;
        }
        m_status.actorStatus.attack = Mathf.Clamp(attack, m_status.maxAttack * (1f - Parameter.FOOD_GAUGE_DECREASE_RATIO_ATTACK), m_status.maxAttack);
        m_status.actorStatus.defence = Mathf.Clamp(defence, m_status.maxdefence * (1f - Parameter.FOOD_GAUGE_DECREASE_RATIO_DEFENCE), m_status.maxdefence);
    }

    public bool HaveFood()
    {
        return 0.0f < m_status.foodGauge;
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
        // ダメージを受ける音
        soundManager_2.PlaySe(3);

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

    public PlayerStatus GetStatus()
    {
        return m_status;
    }

    public void AddExp(in int exp)
    {
        // 経験値取得
        soundManager_2.PlaySe(4);

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

    public int GetWaterGauge()
    {
        return m_status.waterGauge;
    }

    public int GetFoodGauge()
    {
        return m_status.foodGauge;
    }

    public void HighSpeedMove(InputAction.CallbackContext context)
    {

        if (!GetComponent<HighSpeedMove>()) return;
        StartCoroutine(GetComponent<HighSpeedMove>().Move(gameObject, false));
    }
}