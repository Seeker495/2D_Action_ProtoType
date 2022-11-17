using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <ŠT—v>
 *  “G‚Ì”h¶ƒNƒ‰ƒXB
 *******************************************************************/
public class Enemy_5 : EnemyBase
{
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
        //m_time += Time.deltaTime;
        //if (m_time > 0.5f)
        //{
        //    m_normalMovePattern.Execute();
        //    m_time = 0.0f;
        //}
        var player = GameObject.FindWithTag("Player");

        AddMoveTime();
        AddActionTime();


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

        if (!player) return;

        if (base.GetActionTime() > 1.0f && Vector2.Distance(player.transform.position, transform.position) <= 10.0f)
        {
            Attack();
            ResetActionTime();
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
        AttackBase homing = magicObject.AddComponent<AttackWithDirection>();
        homing.SetTarget(GameObject.FindWithTag("Player"));
        homing.Attack();
    }

}
