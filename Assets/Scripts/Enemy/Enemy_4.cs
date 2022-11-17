using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <ŠT—v>
 *  “G‚Ì”h¶ƒNƒ‰ƒXB
 *******************************************************************/
public class Enemy_4 : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        PatternFactory.CreateMovePattern(ref m_status.movePattern, out m_normalMovePattern, out m_findMovePattern, transform);
        PatternFactory.CreateAttackPattern(ref m_status.attackPattern, out m_attackList);


    }

    public override void Execute()
    {
        //if (m_normalMovePattern.Equals(null)) return;
        //if (m_findMovePattern.Equals(null)) return;
        if (!GameObject.FindWithTag("Player")) return;
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
            if (m_normalMovePattern != null)
                m_normalMovePattern.Execute();
        }

    }

    public override async void Attack()
    {
        var magic = await Addressables.LoadAssetAsync<GameObject>("Fire").Task;
        Debug.Log(magic);
        for (int i = 0; i < 8; ++i)
        {
            float angleZ = Mathf.PI * 0.25f * Mathf.Rad2Deg * i;
            GameObject magicObject = Instantiate(magic, transform.position, Quaternion.identity, transform);
            magicObject.transform.rotation = Quaternion.Euler(0,0,angleZ);
            AttackBase homing = magicObject.AddComponent<Attack_EightDirection>();
            homing.Attack();
        }
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
