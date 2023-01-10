using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillInfo", menuName = "CreateSkillInfo")]
public class SkillInfo : ScriptableObject
{
    [SerializeField]
    private string m_name;
    [SerializeField]
    private int m_rare;
    [SerializeField]
    private string m_description;
    [SerializeField]
    private int m_libraryIndex;

    public string Name => m_name;
    public int Rare => m_rare;
    public string Description => m_description;
    public int LibraryIndex => m_libraryIndex;

}
