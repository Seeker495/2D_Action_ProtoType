using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomPattern : IMovePattern
{
    // 敵の現在の行動
    private eEnemyAction m_enemyAction = eEnemyAction.STOP;
    private eEnemyAction m_prevEnemyAction;
    // 対象の位置情報
    private Transform m_transform;
    // 行動間隔
    private const float ACTION_INTERVAL = 0.5f;
    // 連続した回数
    private int m_consecutiveCount = 0;
    // 現在の行動確率を保存する配列
    private Dictionary<eEnemyAction, float> m_enemyActionChance = new Dictionary<eEnemyAction, float>(6)
    {
        {eEnemyAction.CONTINUE,0.9f },
        {eEnemyAction.MOVE,0.1f },
        {eEnemyAction.STOP,0.0f },
    };



    public MoveRandomPattern(Transform transform)
    {
        m_transform = transform;
    }

    void IMovePattern.Execute()
    {
        float nextMove = Random.Range(0.0f, 1.0f);
        bool isContinue =
            nextMove <= m_enemyActionChance[eEnemyAction.CONTINUE] &&
            m_enemyActionChance[eEnemyAction.CONTINUE] != 0.0f;

        bool isMove =
            nextMove <= m_enemyActionChance[eEnemyAction.CONTINUE] + m_enemyActionChance[eEnemyAction.MOVE] &&
            m_enemyActionChance[eEnemyAction.MOVE] != 0.0f;

        bool isStop =
            nextMove < m_enemyActionChance[eEnemyAction.CONTINUE] + m_enemyActionChance[eEnemyAction.MOVE] + m_enemyActionChance[eEnemyAction.STOP] &&
            m_enemyActionChance[eEnemyAction.STOP] != 0.0f;
        if (m_enemyAction != eEnemyAction.CONTINUE)
            m_prevEnemyAction = m_enemyAction;

        //if (isContinue)
        //{
        //    m_enemyAction = eEnemyAction.CONTINUE;
        //}
        //else if (isMove)
        //{
        //    m_enemyAction = eEnemyAction.MOVE;
        //}
        //else if (isStop)
        //{
        //    m_enemyAction = eEnemyAction.STOP;
        //}

        m_enemyAction =
            isContinue ? eEnemyAction.CONTINUE :
            isMove ? eEnemyAction.MOVE :
            isStop ? eEnemyAction.STOP : eEnemyAction.NONE;

        switch (m_enemyAction)
        {
            case eEnemyAction.CONTINUE:
                switch (m_prevEnemyAction)
                {
                    case eEnemyAction.MOVE:
                        float angleAgain = Random.Range(0.0f, 360.0f);
                        m_transform.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, angleAgain) * Vector3.up * m_transform.GetComponent<IActor>().GetBaseStatus().speed * Parameter.ENEMY_VELOCITY_MULTIPLY;
                        break;
                    case eEnemyAction.STOP:
                        m_transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        break;
                }
                break;
            case eEnemyAction.MOVE:
                float angle = Random.Range(0.0f, 360.0f);
                m_transform.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, angle) * Vector3.up * m_transform.GetComponent<IActor>().GetBaseStatus().speed * Parameter.ENEMY_VELOCITY_MULTIPLY;
                break;
            case eEnemyAction.STOP:
                m_transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            default:
                break;
        }

        m_consecutiveCount = m_prevEnemyAction == m_enemyAction ? ++m_consecutiveCount : 0;

        if (m_enemyAction == eEnemyAction.MOVE && m_consecutiveCount % 4 == 0 && m_consecutiveCount != 0)
        {
            m_enemyActionChance[eEnemyAction.CONTINUE] += -0.1f;
            m_enemyActionChance[eEnemyAction.MOVE] += 0.03f;
            m_enemyActionChance[eEnemyAction.STOP] += 0.07f;
        }
        else if (m_enemyAction == eEnemyAction.STOP && m_consecutiveCount % 2 == 0 && m_consecutiveCount != 0)
        {
            m_enemyActionChance[eEnemyAction.CONTINUE] += -0.1f;
            m_enemyActionChance[eEnemyAction.MOVE] += 0.1f;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
