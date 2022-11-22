using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    private GameObject m_scoreObject;

    private void Awake()
    {
        m_scoreObject = GameObject.FindWithTag("Score");
        m_scoreObject.GetComponent<TextMeshProUGUI>().text = $"{Parameter.CURRENT_SCORE.ToString("#,##0")}";
    }
}
