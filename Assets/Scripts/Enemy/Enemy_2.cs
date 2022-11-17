using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <ŠT—v>
 *  “G‚Ì”h¶ƒNƒ‰ƒXB
 *******************************************************************/
public class Enemy_2 : EnemyBase
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
    }

}
