using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <�T�v>
 *  �A�N�^�̃C���^�[�t�F�C�X�N���X�B
 *******************************************************************/
public interface IActor
{
    public Vector2 GetDirection();

    public ActorStatus GetBaseStatus();
}
