using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    private SkillInfo m_skillInfo;
    [SerializeField]
    private TextMeshProUGUI m_nameText;
    [SerializeField]
    private TextMeshProUGUI m_rareText;
    [SerializeField]
    private TextMeshProUGUI m_descriptionText;
    [SerializeField]
    private TextMeshProUGUI m_libraryIndexText;

    private void Awake()
    {
        m_nameText.text = m_skillInfo.Name;
        for (int i = 0; i < m_skillInfo.Rare; ++i)
            m_rareText.text += $"š";
        m_descriptionText.text = m_skillInfo.Description;
        m_libraryIndexText.text = m_skillInfo.LibraryIndex.ToString();
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
