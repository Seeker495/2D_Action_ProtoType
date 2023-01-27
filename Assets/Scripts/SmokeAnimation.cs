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

    private void FixedUpdate()
    {
        m_displayTime += Time.deltaTime;
    }

    private IEnumerator Play()
    {
        m_displayTime = 0.0f;
        transform.DOScale(0.1f, 0.3f).SetLoops(-1, LoopType.Yoyo);
        while (m_displayTime < CHANGE_TIME)
        {
            int spriteIndex = Random.Range(0, m_smokeSprites.Count);
            GetComponent<SpriteRenderer>().sprite = m_smokeSprites[spriteIndex];
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }

    public void ResetTime()
    {
        m_displayTime = 0.0f;
    }
}
