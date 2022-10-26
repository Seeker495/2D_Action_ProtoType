using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  あらゆる攻撃の基底クラス。
 *******************************************************************/
public abstract class AttackBase : MonoBehaviour
{
    public abstract void Attack();

    public Sprite GetSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }

    public abstract eAttackType GetAttackType();

    public abstract void SetTarget(in GameObject target = null);
}
