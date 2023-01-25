using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <ŠT—v>
 *  “G‚Ì”h¶ƒNƒ‰ƒXB
 *******************************************************************/
public class Enemy_9 : EnemyBase
{
    private eEnemyAction m_enemyAction = eEnemyAction.STOP;
    private bool m_switchFlag = false;
    private float m_time = 0.0f;
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        PatternFactory.CreateMovePattern(ref m_status.movePattern, out m_normalMovePattern, out m_findMovePattern, transform);
        PatternFactory.CreateAttackPattern(ref m_status.attackPattern, out m_attackList);

    }

    public override void Execute()
    {

        //if (m_normalMovePattern.Equals(null)) return;
        //if (m_findMovePattern.Equals(null)) return;
        AddActionTime();
        AddMoveTime();
        if (m_isNotified)
        {
            //if (base.GetActionTime() > 1.0f)
            //{
            //    m_switchFlag = !m_switchFlag;
            //    Attack();
            //    ResetActionTime();
            //}
            m_findMovePattern.Execute();
        }
        else
        {
            if (GetMoveTime() > 0.5f)
            {
                m_normalMovePattern.Execute();
                ResetMoveTime();
            }
        }

    }

    public override async void Attack()
    {
        var magic = await Addressables.LoadAssetAsync<GameObject>("Fire").Task;

        if (m_switchFlag)
        {
            for (int i = 0; i < 8; ++i)
            {
                float angleZ = Mathf.PI * 0.25f * Mathf.Rad2Deg * i;
                GameObject magicObject = Instantiate(magic, transform.position, Quaternion.Euler(0, 0, angleZ), null);
                AttackBase homing = magicObject.AddComponent(m_attackList[0].GetType()) as Attack_EightDirection;
                homing.SetTarget(GameObject.FindWithTag("Player"));
                homing.GetComponent<MagicStatus>().Attack = GetComponent<IActor>().GetBaseStatus().attack;
                homing.Attack();
            }
        }
        else
        {
            GameObject magicObject = Instantiate(magic, transform.position, Quaternion.identity, null);
            AttackBase homing = magicObject.AddComponent(m_attackList[1].GetType()) as Homing;
            homing.SetTarget(GameObject.FindWithTag("Player"));
            homing.GetComponent<MagicStatus>().Attack = GetComponent<IActor>().GetBaseStatus().attack;
            homing.GetComponent<MagicStatus>().Position = GetComponent<Rigidbody2D>().position;

            homing.Attack();
        }
    }


}
