using System;
using System.Numerics;

/*******************************************************************
 *  <概要>
 *  敵のステータスを示す構造体。
 *******************************************************************/
[Serializable]
public struct EnemyStatus
{
    public int id;
    public string name;
    public ActorStatus actorStatus;
    public int exp;
    public int money;
    public int movePattern;
    public int attackPattern;
    public UnityEngine.Vector3 position;
}
