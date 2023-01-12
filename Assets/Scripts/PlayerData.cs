using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    private static PlayerStatus m_status;
    private static SkillInfo[] m_skill = new SkillInfo[50];
    public static void SetStatus(PlayerStatus status)
    {
        m_status = status;
    }

    public static PlayerStatus GetStatus()
    {
        return m_status;
    }

    public static void AddSkill(SkillInfo skill)
    {
        m_skill[skill.LibraryIndex - 1] = skill;
    }

    public static SkillInfo[] GetSkillList()
    {
        return m_skill;
    }
}
