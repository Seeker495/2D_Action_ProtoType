using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <äTóv>
 *  ìGÇÃîhê∂ÉNÉâÉXÅB
 *******************************************************************/
public class Enemy_10 : EnemyBase
{
    private eEnemyAction m_enemyAction = eEnemyAction.STOP;
    private bool m_switchFlag = false;
    private float m_time = 0.0f;
    private float m_time2 = 0.0f;
    private static float ADJUST_ANGLE = 10.0f;
    private bool m_isHalf = false;
    private float m_maxHP = 0.0f;
    [SerializeField] private GameObject m_attackObject;
    // Start is called before the first frame update
    async void Awake()
    {
        base.Awake();
        PatternFactory.CreateMovePattern(ref m_status.movePattern, out m_normalMovePattern, out m_findMovePattern, transform);
        PatternFactory.CreateAttackPattern(ref m_status.attackPattern, out m_attackList);
        m_maxHP = m_status.actorStatus.hp;

    }

    public override void Execute()
    {
        m_time += Time.deltaTime;
        m_time2 += Time.deltaTime;

        m_isHalf = m_status.actorStatus.hp <= m_maxHP * 0.5f;
        if (m_isHalf)
        {
            if (m_time > 3.0f)
            {
                StartCoroutine(Attack3(m_attackObject));
                m_time = 0.0f;
            }

            if (m_time2 > 2.0f)
            {
                StartCoroutine(Attack4(m_attackObject));
                m_time2 = 0.0f;
            }
        }
        else
        {
            if (m_time > 5.0f)
            {
                StartCoroutine(Attack1(m_attackObject));
                m_time = 0.0f;
            }

            if (m_time2 > 2.0f)
            {
                StartCoroutine(Attack2(m_attackObject));
                m_time2 = 0.0f;
            }
        }
        //if (m_normalMovePattern.Equals(null)) return;
        //if (m_findMovePattern.Equals(null)) return;
        //AddActionTime();
        //AddMoveTime();
        //if (m_isNotified)
        //{

        //    m_findMovePattern.Execute();
        //}
        //else
        //{
        //    if (GetMoveTime() > 0.5f)
        //    {
        //        m_normalMovePattern.Execute();
        //        ResetMoveTime();
        //    }
        //}

    }

    private IEnumerator Attack1(GameObject obj)
    {

        System.Action attack = () =>
        {
            for (int i = 0; i < 8; ++i)
            {
                float angleZ = Mathf.PI * 0.25f * Mathf.Rad2Deg * i;
                GameObject magicObject = Instantiate(obj, transform.position, Quaternion.Euler(0, 0, angleZ + ADJUST_ANGLE), null);
                AttackBase homing = magicObject.AddComponent<Attack_EightDirection>();
                homing.SetTarget(GameObject.FindWithTag("Player"));
                homing.GetComponent<MagicStatus>().Attack = GetComponent<IActor>().GetBaseStatus().attack;
                homing.GetComponent<MagicStatus>().Position = GetComponent<Rigidbody2D>().position;
                homing.Attack();
            }
            ADJUST_ANGLE += 10.0f;
            if (ADJUST_ANGLE <= 360.0f)
                ADJUST_ANGLE = 0.0f;
        };
        attack();
        yield return new WaitForSeconds(0.5f);
        attack();
    }

    private IEnumerator Attack2(GameObject obj)
    {
        GameObject magicObject = Instantiate(obj, transform.position, Quaternion.identity, null);
        AttackBase homing = magicObject.AddComponent<Homing>();
        homing.SetTarget(GameObject.FindWithTag("Player"));
        homing.GetComponent<MagicStatus>().Attack = GetComponent<IActor>().GetBaseStatus().attack;
        homing.GetComponent<MagicStatus>().Position = GetComponent<Rigidbody2D>().position;
        homing.Attack();
        yield return null;
    }

    private IEnumerator Attack3(GameObject obj)
    {

        System.Action attack = () =>
        {
            for (int i = 0; i < 8; ++i)
            {
                float angleZ = Mathf.PI * 0.25f * Mathf.Rad2Deg * i;
                GameObject magicObject = Instantiate(obj, transform.position, Quaternion.Euler(0, 0, angleZ + ADJUST_ANGLE), null);
                AttackBase homing = magicObject.AddComponent<Attack_EightDirection>();
                homing.SetTarget(GameObject.FindWithTag("Player"));
                homing.GetComponent<MagicStatus>().Attack = GetComponent<IActor>().GetBaseStatus().attack;
                homing.GetComponent<MagicStatus>().Position = GetComponent<Rigidbody2D>().position;
                homing.Attack();
            }
            ADJUST_ANGLE += 10.0f;
            if (ADJUST_ANGLE <= 360.0f)
                ADJUST_ANGLE = 0.0f;
        };
        attack();
        yield return new WaitForSeconds(0.5f);
        attack();
        yield return new WaitForSeconds(0.5f);
        attack();
    }

    private IEnumerator Attack4(GameObject obj)
    {
        System.Action attack = () =>
        {
            GameObject magicObject = Instantiate(obj, transform.position, Quaternion.identity, null);
            AttackBase homing = magicObject.AddComponent<Homing>();
            homing.SetTarget(GameObject.FindWithTag("Player"));
            homing.GetComponent<MagicStatus>().Position = GetComponent<Rigidbody2D>().position;
            homing.Attack();
        };
        attack();
        attack();
        yield return null;
    }

    public override void Attack()
    {

        //if (m_switchFlag)
        //{
        //    for (int i = 0; i < 8; ++i)
        //    {
        //        float angleZ = Mathf.PI * 0.25f * Mathf.Rad2Deg * i;
        //        GameObject magicObject = Instantiate(magic, transform.position, Quaternion.Euler(0, 0, angleZ + ADJUST_ANGLE), transform);
        //        AttackBase homing = magicObject.AddComponent(m_attackList[0].GetType()) as Attack_EightDirection;
        //        homing.SetTarget(GameObject.FindWithTag("Player"));
        //        homing.Attack();
        //    }
        //}
        //else
        //{
        //    GameObject magicObject = Instantiate(magic, transform.position, Quaternion.identity, transform);
        //    AttackBase homing = magicObject.AddComponent(m_attackList[1].GetType()) as Homing;
        //    homing.SetTarget(GameObject.FindWithTag("Player"));
        //    homing.Attack();
        //}
    }


}
