using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <�T�v>
 *  �����A�C�e���̊��N���X
 *  <�d�g��>
 *  �A�C�e�����X�e�[�^�X�ɗ^��������\���̂��Ƃɕ����邽��,�W�F�l���b�N�����Ă���
 *******************************************************************/
public abstract class ImmediateItemBase<ItemInfo> : MonoBehaviour
{
    public abstract ItemInfo GetInfo();
}
