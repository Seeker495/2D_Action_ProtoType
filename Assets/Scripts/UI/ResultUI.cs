using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static menu_Script;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_totalMoney;
    [SerializeField]
    private TextMeshProUGUI m_totalExp;
    private IEnumerator m_moneyEnumerator;
    private IEnumerator m_expEnumerator;
    private IEnumerator m_levelEnumerator;

    private Coroutine m_moneyCoroutine;
    private Coroutine m_expCoroutine;
    private Coroutine m_levelCoroutine;

    [SerializeField]
    private Slider m_levelGauge;
    [SerializeField]
    private TextMeshProUGUI m_levelText;

    [SerializeField]
    private LevelInfo m_levelInfo;

    [SerializeField]
    private List<GameObject> m_SkillObjects;
    [SerializeField]
    private GameObject m_endWindowObject;

    private int m_skillIndex = 0;
    private int m_endIndex = 0;
    [SerializeField]
    private GameObject m_skillFocus;
    [SerializeField]
    private GameObject m_endFocus;

    private DG.Tweening.Sequence m_sequence;

    private void Awake()
    {
        m_SkillObjects.ForEach(skillObject => skillObject.SetActive(false));
        m_skillFocus.SetActive(false);
        m_endWindowObject.gameObject.SetActive(false);
        m_moneyEnumerator = AddScore.Start((int)Parameter.CURRENT_SCORE, "‰~‘Š“–", m_totalMoney, 10000);
        m_expEnumerator = AddScore.Start(PlayerData.GetStatus().exp, "XP", m_totalExp, 20);
        m_levelEnumerator = AddScore.Start(this, 1, PlayerData.GetStatus().exp, m_levelText, m_levelGauge, m_levelInfo.LevelList);
        var sequence = DOTween.Sequence();
        sequence.onComplete += () => m_moneyCoroutine = StartCoroutine(m_moneyEnumerator);
        sequence.Append(transform.DOMoveY(2000, 0).SetUpdate(false));
        sequence.Append(transform.DOMoveY(540, 2).SetUpdate(false));
        sequence.Play();
    }

    private void Update()
    {

        if (m_moneyEnumerator.Current is string && m_moneyCoroutine != null)
        {
            m_moneyCoroutine = null;
            m_expCoroutine = StartCoroutine(m_expEnumerator);
        }


        if (m_expEnumerator.Current is string && m_expCoroutine != null)
        {
            m_expCoroutine = null;
            m_endWindowObject.gameObject.SetActive(true);
        }

        //if (m_levelCoroutine != null && m_levelEnumerator.Current is IEnumerator)
        //{
        //    m_levelEnumerator = m_levelEnumerator.Current as IEnumerator;
        //}

        //if (m_levelEnumerator.Current is string)
        //{
        //     m_endWindowObject.gameObject.SetActive(true);
        //}

        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    AddScore.IS_LEVEL_UP = false;
        //}

        //if (AddScore.IS_LEVEL_UP)
        //{
        //    m_SkillObjects.ForEach(skillObject => skillObject.SetActive(true));
        //    m_skillFocus.SetActive(true);
        //}
        //else
        //{
        //    m_SkillObjects.ForEach(skillObject => skillObject.SetActive(false));
        //    m_skillFocus.SetActive(false);
        //}

        //if (AddScore.IS_LEVEL_UP && Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    --m_skillIndex;
        //}

        //if (AddScore.IS_LEVEL_UP && Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    ++m_skillIndex;
        //}
    }

    private void SelectLeft(InputAction.CallbackContext context)
    {
        m_skillIndex = System.Math.Abs(--m_skillIndex + 3) % 3;
    }

    private void SelectRight(InputAction.CallbackContext context)
    {
        m_skillIndex = System.Math.Abs(++m_skillIndex) % 3;
    }


    private void SelectUp(InputAction.CallbackContext context)
    {
        m_endIndex = System.Math.Abs(--m_endIndex + 2) % 2;
    }

    private void SelectDown(InputAction.CallbackContext context)
    {
        m_endIndex = System.Math.Abs(++m_endIndex) % 2;
    }
    private void FixedUpdate()
    {

    }
}
