using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerStatus
{
    public ActorStatus actorStatus;
    public int maxHP;
    public int exp;
    public int money;
    public float foodGauge;
    public float waterGauge;
}
