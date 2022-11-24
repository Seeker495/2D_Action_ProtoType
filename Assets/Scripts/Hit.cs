using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_hitValueText;
    [SerializeField]
    private Animator m_animator;
    private Player m_player;
    private Coroutine m_coroutine;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHitCombo(in int hitCombo)
    {
        if(hitCombo <= 0 || m_hitValueText.text == hitCombo.ToString()) return;

        m_hitValueText.text = hitCombo.ToString();
        if(m_coroutine != null)
            StopCoroutine(m_coroutine);
        m_coroutine = StartCoroutine(DisplayHit(hitCombo));
    }

    private IEnumerator DisplayHit(int hitCombo, float duration = 0.0f)
    {
        float t = 0.0f;
        m_animator.PlayInFixedTime("ScalingHit", 0, 0);
        while (t < 3.0f)
        {
            t += Time.deltaTime;
            yield return null;
        }
        m_player.ResetCombo();
    }
}
