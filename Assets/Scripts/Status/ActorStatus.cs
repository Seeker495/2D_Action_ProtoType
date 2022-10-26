using System;

/*******************************************************************
 *  <概要>
 *  アクタのステータスを示す構造体。
 *******************************************************************/
[Serializable]
public struct ActorStatus
{
    public int hp;
    public float attack;
    public float defense;
    public float speed;
    public int level;
    public float touchPower;
}
