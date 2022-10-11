using System;

[Serializable]
public struct EnemyStatus
{
    public int id;
    public string name;
    public ActorStatus actorStatus;
    public int exp;
    public int money;
}
