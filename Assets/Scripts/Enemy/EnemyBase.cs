using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <�T�v>
 *  �G�̊��N���X�B
 *  <�d�g��>
 *  �G�̔h���N���X�����ۂ�SerializeField��public��p����
 *  �C���X�y�N�^����f�[�^�x�[�X�̏���ǂݎ��B
 *******************************************************************/
public abstract class EnemyBase : MonoBehaviour, IActor
{
    // �G�̃X�e�[�^�X
    protected EnemyStatus m_status;
    // �f�[�^�x�[�X����ǂݍ��ޓG�̃p�����[�^
    [SerializeField]
    protected EnemyParameter m_enemyParameter;
    // �������Z2D
    [SerializeField]
    protected Rigidbody2D m_rigidBody2D;
    // �v���C���[�ɋC�Â��Ă��邩
    protected bool m_isNotified = false;
    // ���@�̊Ǘ���
    MagicManager m_magicManager;
    protected float m_actionTime = 0.0f;
    // �����x�N�g��
    protected Vector2 m_direction;
    // �ʏ펞�̍s���p�^�[�� (null�̏ꍇ�͈ړ������Ƃ���)
    protected IMovePattern m_normalMovePattern = null;
    // �v���C���[�������̍s���p�^�[�� (null�̏ꍇ�͈ړ������Ƃ���)
    protected IMovePattern m_findMovePattern = null;
    // �U����i
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
     *  ScriptableObject����擾�����f�[�^��ݒ肷��
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
     *  �ʒu�̐ݒ�
     *******************************************************************/
    public void SetPosition(ref float minX, ref float maxX, ref float minY, ref float maxY)
    {
        m_rigidBody2D.position = new Vector2(Mathf.Clamp(m_rigidBody2D.position.x, minX, maxX), Mathf.Clamp(m_rigidBody2D.position.y, minY, maxY));
    }

    /*******************************************************************
     *  ���m��Ԃ�ON�ɂ���
     *******************************************************************/
    public void AttachNotify()
    {
        m_isNotified = true;
    }

    /*******************************************************************
     *  �ǐՒ�(�����ɒǂ�������Ώۂ̃I�u�W�F�N�g��ݒ�)
     *******************************************************************/
    public void Chasing(in GameObject chaseTarget)
    {
        m_rigidBody2D.velocity = (chaseTarget.GetComponent<Rigidbody2D>().position - m_rigidBody2D.position).normalized * m_status.actorStatus.speed;
        if (!chaseTarget.GetComponent<Player>().IsArrive())
            StopChasing();
    }

    /*******************************************************************
     *  �ǐՂ��~����
     *******************************************************************/
    public void StopChasing()
    {
        DetachNotify();
    }

    /*******************************************************************
     *  ���m��Ԃ�OFF�ɂ���
     *******************************************************************/

    public void DetachNotify()
    {
        m_isNotified = false;
    }

    /*******************************************************************
     *  �v���C���[�ɋC�Â��Ă��邩
     *******************************************************************/
    public bool IsNotified()
    {
        return m_isNotified;
    }

    /*******************************************************************
     *  �����x�N�g���̎擾
     *******************************************************************/
    Vector2 IActor.GetDirection()
    {
        return m_direction;
    }

    /*******************************************************************
     *  ��b�X�e�[�^�X�̎擾
     *******************************************************************/
    ActorStatus IActor.GetBaseStatus()
    {
        return m_status.actorStatus;
    }

    /*******************************************************************
     *  �o���l�̎擾
     *******************************************************************/
    public int GetExp()
    {
        return m_status.exp;
    }

    /*******************************************************************
     *  �����̎擾
     *******************************************************************/
    public int GetMoney()
    {
        return m_status.money;
    }

    /*******************************************************************
     *  �����ړ����s��
     *******************************************************************/
    public void HighSpeedMove()
    {
        // �����ړ��̃X�N���v�g�����݂��Ȃ���Ύ��s���Ȃ�
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