using Effekseer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

/*******************************************************************
 *  <�T�v>
 *  ���N���X�B
 *******************************************************************/
public class Blade : AttackBase
{
    [SerializeField]
    Rigidbody2D m_rigidBody2D;
    [SerializeField]
    EffekseerEffectAsset m_effect;

    IEnumerator m_enumrator;
    // ���̐U��X�s�[�h
    const float BLADE_SPEED = 6.0f;
    // Start is called before the first frame update
    void Awake()
    {
        m_rigidBody2D = GetComponentInParent<Rigidbody2D>();
        // �����}�X�N�ɂ��ĉB��
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        GetComponent<EdgeCollider2D>().enabled = false;
    }

    /*
     * �U���֐�
     * �T�v: �U�����s��
     * ����: �J�n�ʒu,����
     */

    public override void Attack()
    {
        if (m_enumrator != null) StopCoroutine(m_enumrator);
        m_enumrator = Attacking(120.0f);
        // �U���֐����J�n����
        StartCoroutine(m_enumrator);

    }


    public override eAttackType GetAttackType()
    {
        return eAttackType.BLADE;
    }

    public override void SetTarget(in GameObject target)
    {
    }

    private void FixedUpdate()
    {
        m_rigidBody2D.position = GetComponentInParent<Rigidbody2D>().position;
    }

    /*
     * �U�����֐�
     * ����:
     */
    public IEnumerator Attacking(float degree)
    {
        GetComponent<EdgeCollider2D>().enabled = true;
        Quaternion q = transform.rotation;
        var direction = GetComponentInParent<IActor>().GetDirection();
        var handle = EffekseerSystem.PlayEffect(m_effect, transform.position);

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        handle.SetRotation(transform.rotation);
        while(handle.exists)
        {
            yield return null;
        }
        transform.rotation = q;
        GetComponent<EdgeCollider2D>().enabled = false;

    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override Sprite GetSprite()
    {
        return GetComponentInChildren<SpriteRenderer>().sprite;
    }
}