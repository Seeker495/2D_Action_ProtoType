using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************
 *  <概要>
 *  ゲーム内で使う定数を定義するクラス。
 *******************************************************************/
public static class Parameter
{
    // ゲームデータ関連
#region GAME_DATA_CONSTANTS
    // カメラの距離(視野) //

    // 遠距離
    public const float CAMERA_FAR_DISTANCE = 12.0f;
    // 中距離
    public const float CAMERA_MIDDLE_DISTANCE = 10.0f;
    // 近距離
    public const float CAMERA_NEAR_DISTANCE = 8.0f;
    // カメラ移動速度
    public const float CAMERA_CHANGE_VELOCITY = 10.0f;
    #endregion

    // プレイヤー関連
#region PLAYER_CONSTANTS
#region ABOUT_MOVING

    // 移動関連の初期値

    // 通常移動速度
    public const float PLAYER_NORMAL_VELOCITY = 10.0f;
    // ダッシュの移動速度倍率
    public const float PLAYER_DASH_MULTIPLY = 2.0f;
    // 高速移動時の移動速度
    public const float PLAYER_HIGH_SPEED_VELOCITY = 60.0f;
    // 高速移動時の移動距離
    public const float PLAYER_HIGH_SPEED_DISTANCE = 4.0f;
    // 高速移動時の使用回数
    public const int PLAYER_HIGH_SPEED_USE_LIMIT = 5;
    // 高速移動の使用間隔
    public const float PLAYER_HIGH_SPEED_USE_INTERVAL = 3.0f;
    // 高速移動の回復までの時間
    public const float PLAYER_HIGH_SPEED_RECOVER_TIME = 6.0f;
    // 高速移動時の無敵の有無
    public const bool PLAYER_HIGH_SPEED_INVINCIBLE_FLAG = false;
    // プレイヤーの速度調整の倍率
    public const float PLAYER_VELOCITY_MULTIPLY = 0.7f;

    #endregion

#region ABOUT_OTHERS

    // 攻撃力
    public const float PLAYER_INIT_ATTACK = 5.0f;
    // 防御力
    public const float PLAYER_INIT_DEFENCE = 3.0f;
    // 最大HP
    public const int PLAYER_MAX_HP = 100;

    #endregion

    // 喉カレゲージ関連
#region ABOUT_WATER_GAUGE

    // 最大値
    public const int WATER_GAUGE_MAX= 100;
    // 減少値
    public const int WATER_GAUGE_DECREASE = 1;
    // 減少間隔
    public const float WATER_GAUGE_DECREASE_INTERVAL = 1.0f;
    // HP減少割合
    [Range(0.0f,1.0f)]
    public const float WATER_GAUGE_DECREASE_RATIO_HP = 0.04f;
    // HP減少間隔
    public const float WATER_GAUGE_DECREASE_HP_INTERVAL = 2.0f;
    #endregion

    // 腹ペコゲージ関連
#region ABOUT_FOOD_GAUGE

    // 最大値
    public const int FOOD_GAUGE_MAX = 100;
    // 減少値
    public const int FOOD_GAUGE_DECREASE = 1;
    // 減少間隔
    public const float FOOD_GAUGE_DECREASE_INTERVAL = 1.0f;
    // 攻撃力減少割合
    [Range(0.0f, 1.0f)]
    public const float FOOD_GAUGE_DECREASE_RATIO_ATTACK = 0.02f;
    // 防御力減少割合
    [Range(0.0f, 1.0f)]
    public const float FOOD_GAUGE_DECREASE_RATIO_DEFENCE = 0.02f;
    #endregion

    // 攻撃関連
#region ABOUT_ATTACK

    // 剣の攻撃威力
    public const float ATTACK_BLADE_POWER = 5.0f;
    // 剣の攻撃速度
    public const float ATTACK_BLADE_SPEED = 5.0f;
    // 弓の攻撃威力
    public const float ATTACK_BOW_POWER = 5.0f;
    // 弓の移動速度
    public const float ATTACK_BOW_SPEED = 10.0f;
    // ボムの攻撃威力
    public const float ATTACK_BOMB_POWER = 5.0f;
    // ボムの投げる距離
    public const float ATTACK_BOMB_THROW_DISTANCE = 10.0f;
    // ボムの爆発範囲(半径)
    public const float ATTACK_BOMB_RANGE = 10.0f;
    // ボムの爆発までの時間
    public const float ATTACK_BOMB_TIME_LIMIT = 5.0f;
    // 攻撃速度調整の倍率
    public const float ATTACK_SPEED_MULTIPLY = 0.5f;
    #endregion
    #endregion

    #region ABOUT_ENEMY

    // 敵の速度調整の倍率
    public const float ENEMY_VELOCITY_MULTIPLY = 0.7f;

    #region ABOUT_DROP_EFFECT
    // ドロップエフェクトの移動速度
    public const float DROP_EFFECT_SPEED = 11.0f;
    // ドロップエフェクトのサイズ倍率
    public const float DROP_EFFECT_SIZE_MULTIPLY = 0.07f;
    #endregion
    #endregion

    #region ABOUT_UI
    // HPが少ないと判断する基準
    public const float UI_DANGER_HP_RATIO = 0.4f;
    // HPが少ないときの枠の点滅間隔
    public const float UI_DANGER_HP_BLINK_INTERVAL = 1.0f;
    #endregion

    #region ABOUT_SCENE
    // 次のシーン名
    public static string NEXT_SCENE_NAME = string.Empty;
    #endregion

    #region ABOUT_GAME_SYSTEM
    // 現在の生存日数
    public static uint CURRENT_ALIVE_DAY = 0;
    // 生存できる最大日数
    public static uint LAST_ALIVE_DAY = 1;
    #endregion

    #region ABOUT_SCORE
    // 現在のスコア
    public static long CURRENT_SCORE = 0;
    #endregion
}
