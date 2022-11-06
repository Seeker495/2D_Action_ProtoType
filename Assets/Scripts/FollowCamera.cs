using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*******************************************************************
 *  <概要>
 *  追従カメラのクラス。
 *  <仕組み>
 *  Cinemachineというアセットを用いて対象のオブジェクトを追従できるようにしている。
 *******************************************************************/
public class FollowCamera : MonoBehaviour
{
    public enum eCameraDistanceRatio
    {
        NEAR,        // 近い
        NORAML,      // 中間
        FAR,         // 遠い
        RATIO_NUM,   // 比率の総数
    }

    public eCameraDistanceRatio m_cameraDistanceRatio = eCameraDistanceRatio.NORAML;

    // カメラの距離を格納する配列
    private Dictionary<eCameraDistanceRatio, float> m_cameraDistance = new Dictionary<eCameraDistanceRatio, float>((int)eCameraDistanceRatio.RATIO_NUM)
    {
        {eCameraDistanceRatio.NEAR,     Parameter.CAMERA_NEAR_DISTANCE },
        {eCameraDistanceRatio.NORAML,   Parameter.CAMERA_MIDDLE_DISTANCE },
        {eCameraDistanceRatio.FAR,      Parameter.CAMERA_FAR_DISTANCE },
    };

    /*******************************************************************
     *  カメラの距離を調整する(カメラの距離の切り替え)
     *******************************************************************/
    public void AdjustCameraDistance(InputAction.CallbackContext context)
    {
        // 現在のカメラの距離比率を列挙体から整数にして取得
        int cameraDistanceRatio = (int)m_cameraDistanceRatio;

        // 次のカメラの距離比率を列挙体にして代入
        m_cameraDistanceRatio = (eCameraDistanceRatio)(Math.Abs(++cameraDistanceRatio) % (int)eCameraDistanceRatio.RATIO_NUM);

        // 基底のコンポーネントを取得
        CinemachineComponentBase componentBase = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent(CinemachineCore.Stage.Body);

        // 条件の型に派生できるならカメラの距離を変更する
        if (componentBase is CinemachineFramingTransposer)
        {
            (componentBase as CinemachineFramingTransposer).m_CameraDistance = m_cameraDistance[m_cameraDistanceRatio];
        }
    }
}
