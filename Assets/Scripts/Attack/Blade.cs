using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : AttackBase
{
    [SerializeField]
    Rigidbody2D m_rigidBody2D;

    // 剣の振るスピード
    const float BLADE_SPEED = 6.0f;
    // Start is called before the first frame update
    void Awake()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        // 内部マスクにして隠す
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * 攻撃関数
     * 概要: 攻撃を行う
     * 引数: 開始位置,方向
     */

    public override void Attack()
    {
        // 攻撃関数を開始する
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
     * 攻撃中関数
     * 引数:
     */
    public IEnumerator Attacking(float degree)
    {
        var direction = transform.parent.GetComponent<IActor>().GetDirection();
        // マスクを無くして表示させる
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        // 開始位置の代入
        m_rigidBody2D.position = transform.parent.GetComponent<Rigidbody2D>().position;
        // Z軸の角度(degreeと比べるため)
        float angleZ = 0.0f;
        // ベクトルの角度を取得するためのもの
        float angle;
        // 角度が規定の値以下の間
        while(angleZ * BLADE_SPEED <= degree)
        {
            //  位置をずらして表示させる
            transform.localPosition = direction * 0.3f;
            // Z軸の角度を加算していき,そのたびに代入
            transform.rotation = Quaternion.Euler(0, 0, -angleZ * BLADE_SPEED);
            // 速度を代入
            m_rigidBody2D.velocity = transform.rotation * direction;
            // ベクトルの角度を算出する
            angle = (Mathf.Atan2(m_rigidBody2D.velocity.y, m_rigidBody2D.velocity.x) + Mathf.PI / 2) * Mathf.Rad2Deg;
            // 剣の角度の調整
            transform.localEulerAngles = new Vector3(0, 0, angle + Mathf.PI * Mathf.Rad2Deg);
            //角度を一定ずつ加算
            angleZ += 0.3f;
            yield return null;
        }
        // 内部マスクにして隠す
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

    }
}
