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
        var isStart = Animator.StringToHash("IsStart");
        var isFinish = Animator.StringToHash("IsFinish");

        m_animator.SetBool(isStart, true);
        m_animator.SetBool(isFinish, false);
    }

    public void AddScore()
    {
        StartCoroutine(AddScoreAnimation(1000));
    }

    IEnumerator AddScoreAnimation(long unit = 1)
    {
        var isStart = Animator.StringToHash("IsStart");
        var isFinish = Animator.StringToHash("IsFinish");
        while (m_addScore != 0)
        {
            m_score += unit;
            m_addScore -= unit;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        m_animator.SetBool(isStart, false);
        m_animator.SetBool(isFinish, true);
    }

    public void SetScore(long score, long addScore = 0)
    {
        if(m_score == 0)
            m_score = score;
        if (m_addScore == 0)
            m_addScore = addScore;
        else
            m_addScore += addScore;
        StartScoreAnimation();
    }
}
