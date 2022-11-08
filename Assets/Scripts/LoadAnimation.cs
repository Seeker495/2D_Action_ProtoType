using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;

public class LoadAnimation : MonoBehaviour
{
    private string m_displayText;
    private TextMeshProUGUI m_textMeshProUGUI;
    private const float TEXT_INTERVAL = 1.5f;
    private const float CHAR_INTERVAL = 0.1f;
    private float m_textTime = 0.0f;
    private IEnumerator m_coroutine = null;
    private bool m_isFullText = false;
    private void Awake()
    {
        m_textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        m_displayText = m_textMeshProUGUI.text;
        m_coroutine = DisplayText();
        StartCoroutine(m_coroutine);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_textTime += Time.deltaTime;
    }

    private IEnumerator DisplayText()
    {
        yield return new WaitForSeconds(0.5f);
        m_textMeshProUGUI.text = string.Empty;
        while (true)
        {
            foreach (var charString in m_displayText)
            {
                m_textMeshProUGUI.text += charString;
                yield return new WaitForSeconds(CHAR_INTERVAL);
            }
            m_textTime = 0.0f;
            m_textMeshProUGUI.text = string.Empty;
        }
    }
}
