using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerStatus
{
    public ActorStatus actorStatus;
    public int maxHP;
    public float maxAttack;
    public float maxDefense;
    public int exp;
    public int money;
    public int foodGauge;
    public int waterGauge;
}
