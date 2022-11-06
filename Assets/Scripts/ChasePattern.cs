using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePattern : IMovePattern
{
    // ‘ÎÛ‚ÌˆÊ’uî•ñ
    private Transform m_transform;
    // ’ÇÕ‘ÎÛ‚ÌˆÊ’uî•ñ
    private Transform m_targetTransform;

    public ChasePattern(Transform transform, string targetName)
    {
        m_transform = transform;
        m_targetTransform = GameObject.FindWithTag(targetName).transform;
    }

    void IMovePattern.Execute()
    {
        Vector2 distance =
            m_targetTransform.GetComponent<Rigidbody2D>().position - m_transform.GetComponent<Rigidbody2D>().position;
        m_transform.GetComponent<Rigidbody2D>().velocity =
            distance.normalized * m_transform.GetComponent<IActor>().GetBaseStatus().speed;
    }
}
