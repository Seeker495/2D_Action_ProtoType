using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCamera : MonoBehaviour
{
    public enum eCameraDistanceRatio
    {
        NEAR,NORAML,FAR,RATIO_NUM,
    }

    public eCameraDistanceRatio CameraDistanceRatio = eCameraDistanceRatio.NORAML;

    Dictionary<eCameraDistanceRatio, float> CameraDistance = new Dictionary<eCameraDistanceRatio, float>((int)eCameraDistanceRatio.RATIO_NUM)
    {
        {eCameraDistanceRatio.NEAR,7.0f },
        {eCameraDistanceRatio.NORAML,15.0f },
        {eCameraDistanceRatio.FAR,25.0f },
    };


    public void AdjustCameraDistance(InputAction.CallbackContext context)
    {
        int cameraDistanceRatio = (int)CameraDistanceRatio;
        CameraDistanceRatio = (eCameraDistanceRatio)(Math.Abs(++cameraDistanceRatio) % (int)eCameraDistanceRatio.RATIO_NUM);
        CinemachineComponentBase componentBase = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is CinemachineFramingTransposer)
        {
            (componentBase as CinemachineFramingTransposer).m_CameraDistance = CameraDistance[CameraDistanceRatio];
        }
    }
}
