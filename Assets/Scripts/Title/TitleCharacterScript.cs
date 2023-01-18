using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*******************************************************************
 *  <�T�v>
 *  �^�C�g���̃L�����N�^�[�N���X�B
 *******************************************************************/
public class TitleCharacterScript : MonoBehaviour
{
    public SpriteRenderer sampleImage;
    // �摜�؂�ւ�
    [SerializeField] private List<Sprite> leftSprite;
    [SerializeField] private List<Sprite> rightSprite;

    // �摜�؂�ւ�����
    float changeImageTime = 0.3f;

    // �摜�̎��
    int imageType = 0;

    // �����X�s�[�h
    public int walkSpeed;

    Sequence m_toRightSequence;
    Sequence m_toLeftSequence;
    bool m_isTurn = false;
    int m_spriteIndex = 0;
    float m_time = 0.0f;

    private void Start()
    {
        sampleImage = GetComponent<SpriteRenderer>();
        m_toRightSequence = DOTween.Sequence();
        m_toLeftSequence = DOTween.Sequence();
        m_toRightSequence.Append(transform.DOMoveX(5.0f, 10.0f / walkSpeed).OnComplete(() => { m_isTurn = !m_isTurn; m_time = 0.0f; sampleImage.sprite = leftSprite[m_spriteIndex]; }));
        m_toRightSequence.Append(transform.DOMoveX(-5.0f, 10.0f / walkSpeed).OnComplete(() => { m_isTurn = !m_isTurn; m_time = 0.0f; sampleImage.sprite = rightSprite[m_spriteIndex]; }));
        m_toRightSequence.SetLoops(-1, LoopType.Restart).SetEase(Ease.Flash);
        m_toRightSequence.Play();
        changeImageTime /= walkSpeed;
    }

    private void FixedUpdate()
    {
        m_time += Time.deltaTime;
        if (m_time > changeImageTime)
        {
            m_spriteIndex = ++m_spriteIndex % 3;
            if (m_isTurn)
                sampleImage.sprite = leftSprite[m_spriteIndex];
            else
                sampleImage.sprite = rightSprite[m_spriteIndex];
            m_time = 0.0f;

        }
    }
}
