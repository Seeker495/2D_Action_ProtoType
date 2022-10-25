using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : AttackBase
{
    [SerializeField]
    Rigidbody2D m_rigidBody2D;

    // ���̐U��X�s�[�h
    const float BLADE_SPEED = 6.0f;
    // Start is called before the first frame update
    void Awake()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        // �����}�X�N�ɂ��ĉB��
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * �U���֐�
     * �T�v: �U�����s��
     * ����: �J�n�ʒu,����
     */

    public override void Attack()
    {
        // �U���֐����J�n����
        StartCoroutine(Attacking(120.0f));
    }


    public override eAttackType GetAttackType()
    {
        return eAttackType.BLADE;
    }

    public override void SetTarget(in GameObject target)
    {
    }


    /*
     * �U�����֐�
     * ����:
     */
    public IEnumerator Attacking(float degree)
    {
        var direction = transform.parent.GetComponent<IActor>().GetDirection();
        // �}�X�N�𖳂����ĕ\��������
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        // �J�n�ʒu�̑��
        m_rigidBody2D.position = transform.parent.GetComponent<Rigidbody2D>().position;
        // Z���̊p�x(degree�Ɣ�ׂ邽��)
        float angleZ = 0.0f;
        // �x�N�g���̊p�x���擾���邽�߂̂���
        float angle;
        // �p�x���K��̒l�ȉ��̊�
        while(angleZ * BLADE_SPEED <= degree)
        {
            //  �ʒu�����炵�ĕ\��������
            transform.localPosition = direction * 0.3f;
            // Z���̊p�x�����Z���Ă���,���̂��тɑ��
            transform.rotation = Quaternion.Euler(0, 0, -angleZ * BLADE_SPEED);
            // ���x����
            m_rigidBody2D.velocity = transform.rotation * direction;
            // �x�N�g���̊p�x���Z�o����
            angle = (Mathf.Atan2(m_rigidBody2D.velocity.y, m_rigidBody2D.velocity.x) + Mathf.PI / 2) * Mathf.Rad2Deg;
            // ���̊p�x�̒���
            transform.localEulerAngles = new Vector3(0, 0, angle + Mathf.PI * Mathf.Rad2Deg);
            //�p�x����肸���Z
            angleZ += 0.3f;
            yield return null;
        }
        // �����}�X�N�ɂ��ĉB��
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

    }
}
