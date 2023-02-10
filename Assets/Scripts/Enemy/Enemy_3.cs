using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/*******************************************************************
 *  <äTóv>
 *  ìGÇÃîhê∂ÉNÉâÉXÅB
 *******************************************************************/
public class Enemy_3 : EnemyBase
{
    private eEnemyAction m_enemyAction = eEnemyAction.STOP;
    [SerializeField] private Sprite m_explosion;
    // Start is called before the first frame update
    void Start()
    {
        PatternFactory.CreateMovePattern(ref m_status.movePattern, out m_normalMovePattern, out m_findMovePattern, transform);
        PatternFactory.CreateAttackPattern(ref m_status.attackPattern, out m_attackList);
    }

    public override void Execute()
    {
        float speed = GetComponent<IActor>().GetBaseStatus().speed;
        AddMoveTime();
        if (m_isNotified)
        {
            speed = 17.0f;
            if (m_findMovePattern != null)
                m_findMovePattern.Execute();
        }
        else
        {
            speed = 10.0f;
            if (GetComponent<SpriteRenderer>().color != Color.white)
                GetComponent<SpriteRenderer>().color = Color.white;
            if (GetMoveTime() > 0.5f)
            {
                if (m_normalMovePattern != null)
                {
                    m_normalMovePattern.Execute();
                    ResetMoveTime();
                }
            }
        }
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public override async void Attack()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(Explosion());
        }
    }

    private IEnumerator Explosion()
    {
        var circleCollider = gameObject.AddComponent<CircleCollider2D>();
        var sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.sprite = m_explosion;
        sprite.transform.DOScale(0.1f, 1.0f).SetEase(Ease.OutExpo).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        circleCollider.radius = 0.7f;
        float explosionTime = 0.0f;
        while (explosionTime < 2.0f)
        {
            explosionTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}