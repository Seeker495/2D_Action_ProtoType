using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Parameter
{
    // �Q�[���f�[�^�֘A
#region GAME_DATA_CONSTANTS
    // �J�����̋���(����) //

    // ������
    public const float CAMERA_FAR_DISTANCE = 25.0f;
    // ������
    public const float CAMERA_MIDDLE_DISTANCE = 15.0f;
    // �ߋ���
    public const float CAMERA_NEAR_DISTANCE = 5.0f;
    // �J�����ړ����x
    public const float CAMERA_CHANGE_VELOCITY = 10.0f;
    #endregion

    // �v���C���[�֘A
#region PLAYER_CONSTANTS
#region ABOUT_MOVING

    // �ړ��֘A�̏����l

    // �ʏ�ړ����x
    public const float PLAYER_NORMAL_VELOCITY = 10.0f;
    // �_�b�V���̈ړ����x�{��
    public const float PLAYER_DASH_MULTIPLY = 2.0f;
    // �����ړ����̈ړ����x
    public const float PLAYER_HIGH_SPEED_VELOCITY = 60.0f;
    // �����ړ����̈ړ�����
    public const float PLAYER_HIGH_SPEED_DISTANCE = 4.0f;
    // �����ړ����̎g�p��
    public const int PLAYER_HIGH_SPEED_USE_LIMIT = 5;
    // �����ړ��̎g�p�Ԋu
    public const float PLAYER_HIGH_SPEED_USE_INTERVAL = 3.0f;
    // �����ړ��̉񕜂܂ł̎���
    public const float PLAYER_HIGH_SPEED_RECOVER_TIME = 6.0f;
    // �����ړ����̖��G�̗L��
    public const bool PLAYER_HIGH_SPEED_INVINCIBLE_FLAG = false;
    #endregion

#region ABOUT_OTHERS

    // �U����
    public const float PLAYER_INIT_ATTACK = 5.0f;
    // �h���
    public const float PLAYER_INIT_DEFENCE = 3.0f;
    // �ő�HP
    public const int PLAYER_MAX_HP = 100;

    #endregion

    // �A�J���Q�[�W�֘A
#region ABOUT_WATER_GAUGE

    // �ő�l
    public const int WATER_GAUGE_MAX= 100;
    // �����l
    public const int WATER_GAUGE_DECREASE = 1;
    // �����Ԋu
    public const float WATER_GAUGE_DECREASE_INTERVAL = 3.0f;
    // HP��������
    [Range(0.0f,1.0f)]
    public const float WATER_GAUGE_DECREASE_RATIO_HP = 0.02f;
    // HP�����Ԋu
    public const float WATER_GAUGE_DECREASE_HP_INTERVAL = 3.0f;
    #endregion

    // ���y�R�Q�[�W�֘A
#region ABOUT_FOOD_GAUGE

    // �ő�l
    public const int FOOD_GAUGE_MAX = 100;
    // �����l
    public const int FOOD_GAUGE_DECREASE = 1;
    // �����Ԋu
    public const float FOOD_GAUGE_DECREASE_INTERVAL = 3.0f;
    // �U���͌�������
    [Range(0.0f, 1.0f)]
    public const float FOOD_GAUGE_DECREASE_RATIO_ATTACK = 0.02f;
    // �h��͌�������
    [Range(0.0f, 1.0f)]
    public const float FOOD_GAUGE_DECREASE_RATIO_DEFENCE = 0.02f;
    #endregion

    // �U���֘A
#region ABOUT_ATTACK

    // ���̍U���З�
    public const float ATTACK_BLADE_POWER = 5.0f;
    // ���̍U�����x
    public const float ATTACK_BLADE_SPEED = 5.0f;
    // �|�̍U���З�
    public const float ATTACK_BOW_POWER = 5.0f;
    // �|�̈ړ����x
    public const float ATTACK_BOW_SPEED = 5.0f;
    // �{���̍U���З�
    public const float ATTACK_BOMB_POWER = 5.0f;
    // �{���̓����鋗��
    public const float ATTACK_BOMB_THROW_DISTANCE = 10.0f;
    // �{���̔����͈�(���a)
    public const float ATTACK_BOMB_RANGE = 10.0f;
    // �{���̔����܂ł̎���
    public const float ATTACK_BOMB_TIME_LIMIT = 5.0f;

    #endregion
    #endregion

    #region ABOUT_ENEMY

    #region ABOUT_DROP_EFFECT
    // �h���b�v�G�t�F�N�g�̈ړ����x
    public const float DROP_EFFECT_SPEED = 6.0f;
    // �h���b�v�G�t�F�N�g�̃T�C�Y�{��
    public const float DROP_EFFECT_SIZE_MULTIPLY = 0.15f;
    #endregion
    #endregion
}