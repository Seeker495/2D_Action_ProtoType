using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    private static PlayerStatus m_status;

    public static void SetStatus(PlayerStatus status)
    {
        m_status = status;
    }

    public static PlayerStatus GetStatus()
    {
        return m_status;
    }
}
