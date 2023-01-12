using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeAnimation : MonoBehaviour
{
    private float m_displayTime = 0.0f;
    private const float CHANGE_TIME = 0.6f;
    [SerializeField]
    private List<Sprite> m_smokeSprites;


    private void Awake()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        transform.DOScale(0.1f, 0.3f).SetLoops(-1, LoopType.Yoyo);
        while (m_displayTime < CHANGE_TIME)
        {
            int spriteIndex = Random.Range(0, m_smokeSprites.Count);
            GetComponent<SpriteRenderer>().sprite = m_smokeSprites[spriteIndex];
            m_displayTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    public void ResetTime()
    {
        m_displayTime = 0.0f;
    }
}
