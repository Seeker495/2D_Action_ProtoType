using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStatus : MonoBehaviour
{
    private float m_attack;
    private Vector2 m_position;

    public float Attack { get { return m_attack; } set { m_attack = value; } }

    public Vector2 Position { get { return m_position; } set { m_position = value; } }
}
