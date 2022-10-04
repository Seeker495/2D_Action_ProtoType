using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    public void Attack();

    public Sprite GetSprite();

    public eAttackType GetAttackType();
}
