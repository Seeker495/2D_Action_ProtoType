using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void Attack(Vector2 startPosition, Vector2 direction);
    public Sprite GetSprite();

    public string GetTagName();

}
