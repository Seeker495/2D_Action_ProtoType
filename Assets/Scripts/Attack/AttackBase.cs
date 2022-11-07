using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <�T�v>
 *  ������U���̊��N���X�B
 *******************************************************************/
public abstract class AttackBase : MonoBehaviour
{
    public abstract void Execute();
    public abstract void Attack();

    public abstract Sprite GetSprite();


    public abstract eAttackType GetAttackType();

    public abstract void SetTarget(in GameObject target = null);
}
