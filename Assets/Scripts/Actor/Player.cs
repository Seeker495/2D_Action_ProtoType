using Mono.Cecil;
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
    private Vector2 m_direction = Vector2.right;
    private PlayerStatus m_status;
    private bool m_isDamaged = false;
    private List<AttackBase> m_weapons;
    private float m_noWaterTime = 0.0f;
    private bool m_haveFood = true;
    private int WeaponIndex = 0;
    private float[] m_stageTime = new float[2];
    private long m_score = 0;
    private long m_increaseScore = 0;
    private int m_hitCombo = 0;
    private bool m_isDashing = false;
    private SkillInfo[] m_haveSkillList = new SkillInfo[50];

    // 最大HPアップ
    int skill_MaxHpUp = 0;
    // 最大攻撃力アップ
    float skill_MaxAttackUp = 0;
    // 最大防御力アップ
    float skill_MaxDefenceUp = 0;
    // 最大お腹ゲージアップ
    int skill_MaxFoodGaugeUp = 0;
    // 最大喉ゲージアップ
    int skill_MaxWaterGaugeUp = 0;
    // 最大速度アップ
    float skill_SpeedUp = 0;
    // 入手コインアップ
    int skill_MoneyUp = 0;
    // 入手経験値アップ
    int skill_ExpUp = 0;

    // 煙アニメーションのオブジェクト
    [SerializeField]
    private GameObject m_smokeObject;

    // ピンチの時に流すseのフラグ
    bool pinchiFlag = false;

    private void OnEnable()
    {
        PlayerController.Controller.Play.Move.performed += Move;
        PlayerController.Controller.Play.Move.canceled += MoveEnd;
        PlayerController.Controller.Play.Attack.started += Attack;
        PlayerController.Controller.Play.Dash.started += DashStart;
        PlayerController.Controller.Play.Dash.performed += Dashing;
        PlayerController.Controller.Play.Dash.canceled += DashEnd;
        PlayerController.Controller.Play.ChangeWeaponToLeft.started += SelectWeaponToLeft;
        PlayerController.Controller.Play.ChangeWeaponToRight.started += SelectWeaponToRight;
        PlayerController.Controller.Play.Resurrection.started += Resurrection;
        PlayerController.Controller.Play.HighSpeedMove.started += HighSpeedMove;
#if UNITY_EDITOR
        PlayerController.Controller.Play.FullHeal.started += FullHeal;
        PlayerController.Controller.Play.FullFood.started += FullFood;
        PlayerController.Controller.Play.FullWater.started += FullWater;

#endif
    }

    private void OnDisable()
    {
        PlayerController.Controller.Play.Move.performed -= Move;
        PlayerController.Controller.Play.Move.canceled -= MoveEnd;
        PlayerController.Controller.Play.Attack.started -= Attack;
        PlayerController.Controller.Play.Dash.started -= DashStart;
        PlayerController.Controller.Play.Dash.performed -= Dashing;
        PlayerController.Controller.Play.Dash.canceled -= DashEnd;
        PlayerController.Controller.Play.ChangeWeaponToLeft.started -= SelectWeaponToLeft;
        PlayerController.Controller.Play.ChangeWeaponToRight.started -= SelectWeaponToRight;
        PlayerController.Controller.Play.Resurrection.started -= Resurrection;
        PlayerController.Controller.Play.HighSpeedMove.started -= HighSpeedMove;
#if UNITY_EDITOR
        PlayerController.Controller.Play.FullHeal.started -= FullHeal;
        PlayerController.Controller.Play.FullFood.started -= FullFood;
        PlayerController.Controller.Play.FullWater.started -= FullWater;
#endif
    }


    void Awake()
    {

        m_rigidbody2D = GetComponent<Rigidbody2D>();

        // 最大HP +10%
        if (IsGotSkill(0))
        {
            skill_MaxHpUp = Parameter.PLAYER_MAX_HP + (Parameter.PLAYER_MAX_HP / 10);
        }
        // 最大HP +25%
        if (IsGotSkill(1))
        {
            skill_MaxHpUp = Parameter.PLAYER_MAX_HP + (Parameter.PLAYER_MAX_HP / 4);
        }
        // 最大HP +50%
        if (IsGotSkill(2))
        {
            skill_MaxHpUp = Parameter.PLAYER_MAX_HP + (Parameter.PLAYER_MAX_HP / 2);
        }
        // 最大攻撃力 +5%
        if (IsGotSkill(3))
        {
            skill_MaxAttackUp = Parameter.PLAYER_INIT_ATTACK + (Parameter.PLAYER_INIT_ATTACK / 20.0f);
        }
        // 最大攻撃力 +10%
        if (IsGotSkill(4))
        {
            skill_MaxAttackUp = Parameter.PLAYER_INIT_ATTACK + (Parameter.PLAYER_INIT_ATTACK / 10.0f);
        }
        // 最大攻撃力 +20%
        if (IsGotSkill(5))
        {
            skill_MaxAttackUp = Parameter.PLAYER_INIT_ATTACK + (Parameter.PLAYER_INIT_ATTACK / 5.0f);
        }
        // 最大防御力 +5%
        if (IsGotSkill(6))
        {
            skill_MaxDefenceUp = Parameter.PLAYER_INIT_DEFENCE + (Parameter.PLAYER_INIT_DEFENCE / 20.0f);
        }
        // 最大防御力 +10%
        if (IsGotSkill(7))
        {
            skill_MaxDefenceUp = Parameter.PLAYER_INIT_DEFENCE + (Parameter.PLAYER_INIT_DEFENCE / 10.0f);
        }
        // 最大防御力 +20%
        if (IsGotSkill(8))
        {
            skill_MaxDefenceUp = Parameter.PLAYER_INIT_DEFENCE + (Parameter.PLAYER_INIT_DEFENCE / 5.0f);
        }
        // 最大お腹ゲージ +10%
        if (IsGotSkill(9))
        {
            skill_MaxFoodGaugeUp = Parameter.FOOD_GAUGE_MAX + (Parameter.FOOD_GAUGE_MAX / 10);
        }
        // 最大お腹ゲージ +25%
        if (IsGotSkill(10))
        {
            skill_MaxFoodGaugeUp = Parameter.FOOD_GAUGE_MAX + (Parameter.FOOD_GAUGE_MAX / 4);
        }
        // 最大お腹ゲージ +50%
        if (IsGotSkill(11))
        {
            skill_MaxFoodGaugeUp = Parameter.FOOD_GAUGE_MAX + (Parameter.FOOD_GAUGE_MAX / 2);
        }
        // 最大喉ゲージ +10%
        if (IsGotSkill(12))
        {
            skill_MaxWaterGaugeUp = Parameter.WATER_GAUGE_MAX + (Parameter.WATER_GAUGE_MAX / 10);
        }
        // 最大喉ゲージ +25%
        if (IsGotSkill(13))
        {
            skill_MaxWaterGaugeUp = Parameter.WATER_GAUGE_MAX + (Parameter.WATER_GAUGE_MAX / 4);
        }
        // 最大喉ゲージ +50%
        if (IsGotSkill(14))
        {
            skill_MaxWaterGaugeUp = Parameter.WATER_GAUGE_MAX + (Parameter.WATER_GAUGE_MAX / 2);
        }
        // 最大スピード +5%
        if (IsGotSkill(15))
        {
            skill_SpeedUp = Parameter.PLAYER_NORMAL_VELOCITY + (Parameter.PLAYER_NORMAL_VELOCITY / 20.0f);
        }
        // 入手コイン +5%
        if (IsGotSkill(16))
        {
            skill_MoneyUp = m_status.money + (m_status.money / 20);
        }
        // 入手経験値 +5%
        if (IsGotSkill(17))
        {
            skill_ExpUp = m_status.exp + (m_status.exp / 20);
        }
        // 星３出現率アップ
        if (IsGotSkill(18))
        {
        }
        // レアお宝出現率アップ
        if (IsGotSkill(19))
        {
        }
        // お宝ドロップ率アップ
        if (IsGotSkill(20))
        {
        }
        // 攻撃力2倍,喉ゲージ減少量2倍
        if (IsGotSkill(21))
        {
        }
        // 回復アイテムの回復量2倍
        if (IsGotSkill(22))
        {
        }
        // 喉ゲージ減少量半減
        if (IsGotSkill(23))
        {
        }
        // お腹がすくと攻撃量2倍
        if (IsGotSkill(24))
        {
        }



        m_status.actorStatus.hp = Parameter.PLAYER_MAX_HP ;
        m_status.actorStatus.speed = Parameter.PLAYER_NORMAL_VELOCITY;
        m_status.actorStatus.attack = Parameter.PLAYER_INIT_ATTACK ;
        m_status.actorStatus.defence = Parameter.PLAYER_INIT_DEFENCE ;
        m_status.exp = 0;
        m_status.money = 0;
        m_status.foodGauge = Parameter.FOOD_GAUGE_MAX ;
        m_status.waterGauge = Parameter.WATER_GAUGE_MAX;
        m_status.maxHP = Parameter.PLAYER_MAX_HP ;
        m_status.maxAttack = Parameter.PLAYER_INIT_ATTACK ;
        m_status.maxdefence = Parameter.PLAYER_INIT_DEFENCE ;

        // 武器リスト
        m_weapons = new List<AttackBase>(3)
        {
            GetComponentInChildren<Blade>(),
            GetComponentInChildren<Bow>(),
            GetComponentInChildren<GrenadeManager>(),
        };
    }

    public void SetParameter(PlayerStatus status)
    {
        m_status = status;
    }

    public void SetSkill(SkillInfo[] skills)
    {
        m_haveSkillList = skills;
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
        if (!pinchiFlag && (float)m_status.actorStatus.hp / (float)m_status.maxHP <= 0.3f)
        {
            pinchiFlag = true;
            SoundPlayer.PlaySFX(eSFX.PINCHI, true);
        }
        else
        {
            pinchiFlag = false;
            SoundPlayer.StopSFX();
        }


    }

#if UNITY_EDITOR
    private void FullHeal(InputAction.CallbackContext context)
    {


        m_status.actorStatus.hp = Parameter.PLAYER_MAX_HP;
    }

    private void FullFood(InputAction.CallbackContext context)
    {
        m_status.foodGauge = Parameter.FOOD_GAUGE_MAX;
    }

    private void FullWater(InputAction.CallbackContext context)
    {
        m_status.waterGauge = Parameter.WATER_GAUGE_MAX;
    }

#endif
    private void FixedUpdate()
    {
        DecreaseGauge();
        GetComponent<BoxCollider2D>().isTrigger = m_isDamaged;
        Debug.Log($"Velocity = {m_rigidbody2D.velocity}");
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        m_velocity = move * m_status.actorStatus.speed * Parameter.PLAYER_VELOCITY_MULTIPLY;
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
    public void DashStart(InputAction.CallbackContext context)
    {
        //m_isDashing = true;
        // 早い移動の音
        SoundPlayer.PlaySFX(eSFX.HIGH_SPEED);
        m_rigidbody2D.velocity = m_velocity * m_rigidbody2D.velocity.normalized *Parameter.PLAYER_DASH_MULTIPLY;

    }

    public void Dashing(InputAction.CallbackContext context)
    {
        m_rigidbody2D.velocity = m_velocity * m_rigidbody2D.velocity.normalized * Parameter.PLAYER_DASH_MULTIPLY;
    }

    public void DashEnd(InputAction.CallbackContext context)
    {
        m_rigidbody2D.velocity = m_velocity;
        //m_isDashing = false;
    }

    public void Attack(InputAction.CallbackContext context)
    {

        m_weapons[WeaponIndex].Attack();
        eSFX[] sfx = new eSFX[]
        {
            eSFX.BLADE,eSFX.BOW,
        };

        // WeaponIndex に応じた武器の音を出す
        SoundPlayer.PlaySFX(sfx[WeaponIndex]);
    }

    public Sprite GetWeaponSprite()
    {
        return m_weapons[WeaponIndex].GetSprite();
    }

    public void SelectWeaponToLeft(InputAction.CallbackContext context)
    {
        WeaponIndex = System.Math.Abs(--WeaponIndex + m_weapons.Count) % m_weapons.Count;

        // 武器チェンジの音
        SoundPlayer.PlaySFX(eSFX.CHANGE_WEAPON);

        if (!GetComponentInChildren<SmokeAnimation>())
        {
            Instantiate(m_smokeObject, transform);
        }
        else
        {
            GetComponentInChildren<SmokeAnimation>().ResetTime();
        }
    }

    public void SelectWeaponToRight(InputAction.CallbackContext context)
    {
        WeaponIndex = System.Math.Abs(++WeaponIndex) % m_weapons.Count;

        // 武器チェンジの音
        SoundPlayer.PlaySFX(eSFX.CHANGE_WEAPON);

        if (!GetComponentInChildren<SmokeAnimation>())
        {
            Instantiate(m_smokeObject, transform);
        }
        else
        {
            GetComponentInChildren<SmokeAnimation>().ResetTime();
        }

    }

    public void Resurrection(InputAction.CallbackContext context)
    {
        gameObject.SetActive(true);
        if (IsArrive()) return;
        gameObject.tag = "Player";
        m_status.actorStatus.hp = 10;
        m_isDamaged = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NormalObstacle"))
        {

        }

        if (m_isDamaged && !collision.gameObject.CompareTag("Wall") && !collision.gameObject.CompareTag("NormalObstacle") && !collision.gameObject.CompareTag("WaterObstacle")) return;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage(collision.transform.GetComponent<IActor>().GetBaseStatus().attack);
            m_rigidbody2D.AddForce(collision.transform.GetComponent<Rigidbody2D>().velocity.normalized * Time.deltaTime);
        }
    }

    private void OnCollisionExit(Collision collision)
    {

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




    }



    private void OnCollisionStay2D(Collision2D collision)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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

        if (collision.CompareTag("TreasureItem"))
        {
            var playUI = GameObject.FindWithTag("PlayUI");
            long score = collision.GetComponent<ImmediateTreasureItem>().GetInfo().score;
            m_increaseScore = score;
            playUI.SendMessage("SetScore");
        }

        if (m_isDamaged && !collision.CompareTag("Wall") && !collision.CompareTag("NormalObstacle") && !collision.CompareTag("WaterObstacle")) return;
        if (collision.CompareTag("Magic"))
            Damage(collision.transform.parent.GetComponent<IActor>().GetBaseStatus().attack);


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
        float attack;
        float defence;


        if (!haveFood)
        {
            attack = (1f - Parameter.FOOD_GAUGE_DECREASE_RATIO_ATTACK);
            defence = (1f - Parameter.FOOD_GAUGE_DECREASE_RATIO_DEFENCE);
        }
        else
        {
            attack = 1f;
            defence = 1f;
        }
        float powerUp;
        // お腹がすくと攻撃量2倍
        if (IsGotSkill(24) && !HaveFood())
        {
           powerUp = 2f;
        }
        else
        {
            powerUp = 1f;
        }
        m_status.actorStatus.attack = m_status.maxAttack * attack * powerUp;
        m_status.actorStatus.defence = m_status.maxdefence * defence;
    }

    public bool HaveFood()
    {
        return 0.0f < m_status.foodGauge;
    }

    private void Dead()
    {
        gameObject.tag = "Untagged";
        gameObject.SetActive(false);
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
        var playUI = GameObject.FindWithTag("PlayUI");
        playUI.SendMessage("Damage");
        // ダメージを受ける音
        SoundPlayer.PlaySFX(eSFX.DAMAGE);

        int damage = Mathf.RoundToInt(attack - m_status.actorStatus.defence);
        m_status.actorStatus.hp -= damage;
        StartCoroutine(OnDamage(2.0f, 0.3f));
    }

    IEnumerator OnDamage(float duration, float interval)
    {
        m_isDamaged = true;
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
        SoundPlayer.PlaySFX(eSFX.GET_EXP);

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

    public long GetScore()
    {
        return m_status.score;
    }

    public void AddScore(in long score)
    {
        m_status.score += score;
    }

    public long GetAddScore()
    {
        return m_increaseScore;
    }


    public void AddCombo()
    {
        m_hitCombo++;
    }

    public void ResetCombo()
    {
        m_hitCombo = 0;
    }

    public int GetCombo()
    {
        return m_hitCombo;
    }

    public bool IsOutOfRange(Rigidbody2D rigidbody)
    {
        bool isOutX = Mathf.Abs(rigidbody.position.x - m_rigidbody2D.position.x) > 15.0f;
        bool isOutY = Mathf.Abs(rigidbody.position.x - m_rigidbody2D.position.x) > 10.0f;
        return isOutX && isOutY;
    }

    private bool IsGotSkill(in int index)
    {
        return m_haveSkillList[index] != null;
    }

}