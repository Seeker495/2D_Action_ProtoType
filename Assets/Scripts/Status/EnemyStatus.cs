using System;
using System.Numerics;

/*******************************************************************
 *  <�T�v>
 *  �G�̃X�e�[�^�X�������\���́B
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
