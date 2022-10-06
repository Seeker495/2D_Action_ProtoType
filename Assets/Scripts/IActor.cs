using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActor
{
    public Vector2 GetDirection();

    public ActorStatus GetBaseStatus();
}
