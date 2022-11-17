using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseWithExplositonPattern : IMovePattern
{
    // ‘ÎÛ‚ÌˆÊ’uî•ñ
    private Transform m_transform;
    // ’ÇÕ‘ÎÛ‚ÌˆÊ’uî•ñ
    private Transform m_targetTransform;

    private float m_changeTime = 0.0f;
    private bool m_changeFlag = false;

    public ChaseWithExplositonPattern(Transform transform, string targetName)
    {
        m_transform = transform;
        m_targetTransform = GameObject.FindWithTag(targetName).transform;
    }

    void IMovePattern.Execute()
    {
        m_changeTime += Time.deltaTime;
        if(m_changeTime > 0.1f)
        {
            m_changeFlag = !m_changeFlag;
            m_changeTime = 0.0f;
        }

        m_transform.GetComponent<SpriteRenderer>().color = m_changeFlag ? Color.red : Color.white;
        Vector2 distance =
            m_targetTransform.GetComponent<Rigidbody2D>().position - m_transform.GetComponent<Rigidbody2D>().position;
        m_transform.GetComponent<Rigidbody2D>().velocity =
            distance.normalized * (m_transform.GetComponent<IActor>().GetBaseStatus().speed * Parameter.ENEMY_VELOCITY_MULTIPLY);

    }
}
