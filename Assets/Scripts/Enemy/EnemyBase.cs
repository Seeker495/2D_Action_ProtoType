using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  敵の基底クラス。
 *  <仕組み>
 *  敵の派生クラスを作る際にSerializeFieldやpublicを用いて
 *  インスペクタからデータベースの情報を読み取る。
 *******************************************************************/
public abstract class EnemyBase : MonoBehaviour, IActor
{
    // 敵のステータス
    protected EnemyStatus m_status;
    // データベースから読み込む敵のパラメータ
    [SerializeField]
    protected EnemyParameter m_enemyParameter;
    // 物理演算2D
    [SerializeField]
    protected Rigidbody2D m_rigidBody2D;
    // プレイヤーに気づいているか
    protected bool m_isNotified = false;
    // 魔法の管理者
    MagicManager m_magicManager;
    protected float m_actionTime = 0.0f;
    // 方向ベクトル
    protected Vector2 m_direction;
    // 通常時の行動パターン (nullの場合は移動無しとする)
    protected IMovePattern m_normalMovePattern = null;
    // プレイヤー発見時の行動パターン (nullの場合は移動無しとする)
    protected IMovePattern m_findMovePattern = null;
    // 攻撃手段
    protected List<AttackBase> m_attackList = null;
    protected float m_moveTime = 0.0f;



    public void Awake()
    {
        SetParameter();
        m_magicManager = GetComponentInChildren<MagicManager>();
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        //if (m_status.actorStatus.speed <= 0.0f)
        //    m_rigidBody2D.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    /*******************************************************************
     *  ScriptableObjectから取得したデータを設定する
     *******************************************************************/
    private void SetParameter()
    {
        m_status.id = m_enemyParameter.ID;
        m_status.name = m_enemyParameter.Name;
        m_status.actorStatus.hp = m_enemyParameter.HP;
        m_status.actorStatus.attack = m_enemyParameter.Attack;
        m_status.actorStatus.defence = m_enemyParameter.defence;
        m_status.actorStatus.speed = m_enemyParameter.Speed;
        m_status.actorStatus.touchPower = m_enemyParameter.TouchPower;
        m_status.exp = m_enemyParameter.Exp;
        m_status.money = m_enemyParameter.Money;
        m_status.movePattern = m_enemyParameter.MovePattern;
        m_status.attackPattern = m_enemyParameter.AttackPattern;
        m_status.position = transform.position;
    }


    public abstract void Execute();

    public abstract void Attack();
    public void FixedUpdate()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Notify")) return;

        if (collision.gameObject.CompareTag("Weapon") || collision.gameObject.CompareTag("Blade"))
            Damage(1);
        if (!IsArrive())
            Dead();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.CompareTag("Arrow"))
        {
            Damage(collision.transform.parent.parent.GetComponent<IActor>().GetBaseStatus().attack * 2);
            collision.transform.parent.parent.GetComponent<Player>().AddCombo();
        }
        if (collision.gameObject.CompareTag("Blade"))
        {
            Damage(collision.transform.parent.GetComponent<IActor>().GetBaseStatus().attack * 3);
            collision.transform.parent.GetComponent<Player>().AddCombo();

        }
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
        int damage = Mathf.RoundToInt(attack - m_status.actorStatus.defence);
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
        transform.DOScale(Vector3.one, 3.0f);
        transform.DOShakePosition(3.0f, 0.1f);

        m_rigidBody2D.velocity = Vector2.zero;

        var dropManager = GetComponentInChildren<DropManager>();



        while (duration > 0.0f)
        {
            duration -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return null;
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        GetComponent<BoxCollider2D>().enabled = false;

        var diffusion = dropManager.Diffusion();

        if (dropManager != null)
        {
            dropManager.enabled = true;
            StartCoroutine(diffusion);
            dropManager.transform.position = transform.position;
            dropManager.transform.SetParent(null, true);
        }
        if(diffusion.Current is string)
            gameObject.SetActive(false);


        Destroy(gameObject);
    }

    /*******************************************************************
     *  位置の設定
     *******************************************************************/
    public void SetPosition(ref float minX, ref float maxX, ref float minY, ref float maxY)
    {
        m_rigidBody2D.position = new Vector2(Mathf.Clamp(m_rigidBody2D.position.x, minX, maxX), Mathf.Clamp(m_rigidBody2D.position.y, minY, maxY));
    }

    /*******************************************************************
     *  検知状態をONにする
     *******************************************************************/
    public void AttachNotify()
    {
        m_isNotified = true;
    }

    /*******************************************************************
     *  追跡中(引数に追いかける対象のオブジェクトを設定)
     *******************************************************************/
    public void Chasing(in GameObject chaseTarget)
    {
        m_rigidBody2D.velocity = (chaseTarget.GetComponent<Rigidbody2D>().position - m_rigidBody2D.position).normalized * m_status.actorStatus.speed;
        if (!chaseTarget.GetComponent<Player>().IsArrive())
            StopChasing();
    }

    /*******************************************************************
     *  追跡を停止する
     *******************************************************************/
    public void StopChasing()
    {
        DetachNotify();
    }

    /*******************************************************************
     *  検知状態をOFFにする
     *******************************************************************/

    public void DetachNotify()
    {
        m_isNotified = false;
    }

    /*******************************************************************
     *  プレイヤーに気づいているか
     *******************************************************************/
    public bool IsNotified()
    {
        return m_isNotified;
    }

    /*******************************************************************
     *  方向ベクトルの取得
     *******************************************************************/
    Vector2 IActor.GetDirection()
    {
        return m_direction;
    }

    /*******************************************************************
     *  基礎ステータスの取得
     *******************************************************************/
    ActorStatus IActor.GetBaseStatus()
    {
        return m_status.actorStatus;
    }

    /*******************************************************************
     *  経験値の取得
     *******************************************************************/
    public int GetExp()
    {
        return m_status.exp;
    }

    /*******************************************************************
     *  お金の取得
     *******************************************************************/
    public int GetMoney()
    {
        return m_status.money;
    }

    /*******************************************************************
     *  高速移動を行う
     *******************************************************************/
    public void HighSpeedMove()
    {
        // 高速移動のスクリプトが存在しなければ実行しない
        if (!GetComponent<HighSpeedMove>()) return;
        StartCoroutine(GetComponent<HighSpeedMove>().Move(gameObject, false));
    }

    public float GetActionTime()
    {
        return m_actionTime;
    }

    public void AddActionTime()
    {
        m_actionTime += Time.deltaTime;
    }

    public void ResetActionTime()
    {
        m_actionTime = 0.0f;
    }
    public float GetMoveTime()
    {
        return m_moveTime;
    }

    public void AddMoveTime()
    {
        m_moveTime += Time.deltaTime;
    }

    public void ResetMoveTime()
    {
        m_moveTime = 0.0f;
    }

    private void OnBecameInvisible()
    {
        if (Camera.main)
        {
            transform.position = m_status.position;
        }
    }

    private void OnBecameVisible()
    {
        if (Camera.main)
        {
        }
    }
}

//if (m_isNotified)
//    time += Time.deltaTime;
//else
//    time = 0.0f;

//if (m_isNotified && time >= INTERVAL && IsArrive())
//{
//    m_direction = m_rigidBody2D.velocity.normalized;
//    m_magicManager.Attack();
//    time = 0.0f;
//}