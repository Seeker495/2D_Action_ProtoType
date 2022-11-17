using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  アクタのインターフェイスクラス。
 *******************************************************************/
public interface IActor
{
    public Vector2 GetDirection();

    public ActorStatus GetBaseStatus();
}
