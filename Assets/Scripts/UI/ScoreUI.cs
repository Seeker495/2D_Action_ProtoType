using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private Animator m_animator;
    private long m_score = 0;
    private long m_addScore = 0;
    [SerializeField]
    GameObject m_scoreObject;
    TextMeshProUGUI m_scoreText;
    private bool m_playingAnimation = false;

    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_scoreText = m_scoreObject.GetComponent<TextMeshProUGUI>();
        Execute();

    }

    // Update is called once per frame
    public void Execute()
    {
        m_scoreText.text = $"{m_score.ToString("#,#")}";
    }

    public void StartScoreAnimation()
    {

        StartCoroutine(AddScoreAnimation(5000));
    }

    public void AddScore()
    {
    }
    IEnumerator AddScoreAnimation(long unit = 1)
    {
        if (m_playingAnimation) yield break;

        var isStart = Animator.StringToHash("IsStart");
        var isFinish = Animator.StringToHash("IsFinish");

        while (m_addScore != 0)
        {
            m_score += unit;
            m_addScore -= unit;
            yield return new WaitForFixedUpdate();
            if (!GameObject.FindWithTag("Player").GetComponent<Player>().IsArrive()) yield break;
        }
        yield return new WaitForSeconds(0.5f);
        m_animator.SetBool(isStart, false);
        m_animator.SetBool(isFinish, true);
        Parameter.CURRENT_SCORE = m_score;
        m_playingAnimation = false;

    }

    public void SettingScore(long addScore = 0)
    {
        m_addScore += addScore;
        var isStart = Animator.StringToHash("IsStart");
        var isFinish = Animator.StringToHash("IsFinish");

        m_animator.SetBool(isStart, true);
        m_animator.SetBool(isFinish, false);

    }
}
