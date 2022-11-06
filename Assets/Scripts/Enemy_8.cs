using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <ŠT—v>
 *  “G‚Ì”h¶ƒNƒ‰ƒXB
 *******************************************************************/
public class Enemy_8 : EnemyBase
{
    private eEnemyAction m_enemyAction = eEnemyAction.STOP;

    // Start is called before the first frame update
    void Start()
    {
        PatternFactory.CreateMovePattern(ref m_status.movePattern, out m_normalMovePattern, out m_findMovePattern, transform);
        PatternFactory.CreateAttackPattern(ref m_status.attackPattern, out m_attackList);
    }

    public override void Execute()
    {
        AddMoveTime();
        AddActionTime();

        if (base.GetActionTime() > 1.0f && Vector2.Distance(GameObject.FindWithTag("Player").transform.position, transform.position) <= 10.0f)
        {
            Attack();
            ResetActionTime();
        }

        if (m_isNotified)
        {
            if (m_findMovePattern != null)
                m_findMovePattern.Execute();
        }
        else
        {
            if (GetMoveTime() > 0.5f)
            {
                if (m_normalMovePattern != null)
                {
                    m_normalMovePattern.Execute();
                    ResetMoveTime();
                }
            }
        }
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public override async void Attack()
    {
        var magic = await Addressables.LoadAssetAsync<GameObject>("Fire").Task;
        Debug.Log(magic);
        GameObject magicObject = Instantiate(magic, transform.position, Quaternion.identity, transform);
        AttackBase homing = magicObject.GetComponent<Homing>();
        homing.SetTarget(GameObject.FindWithTag("Player"));
        homing.Attack();
    }

}
