using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <ŠT—v>
 *  “G‚Ì”h¶ƒNƒ‰ƒXB
 *******************************************************************/
public class Enemy_1 : EnemyBase
{
    private eEnemyAction m_enemyAction = eEnemyAction.STOP;

    private void Awake()
    {
        base.Awake();
        PatternFactory.CreateMovePattern(ref m_status.movePattern, out m_normalMovePattern, out m_findMovePattern, transform);
        PatternFactory.CreateAttackPattern(ref m_status.attackPattern, out m_attackList);
    }

    public override void Execute()
    {
        AddMoveTime();

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

    // Start is called before the first frame update
    //void Start()
    //{
    //    base.Start();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public override void Attack()
    {
    }

}
