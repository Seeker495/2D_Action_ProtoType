using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Level
{
    public int nowLevel;
    public int needExp;
}

[CreateAssetMenu(fileName = "LevelInfo", menuName = "CreateLevelInfo")]
public class LevelInfo : ScriptableObject
{
    [SerializeField]
    private List<Level> m_levelList;

    public List<Level> LevelList => m_levelList;
}
