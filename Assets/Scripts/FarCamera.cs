using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // 基底のコンポーネントを取得
        CinemachineComponentBase componentBase = GameObject.FindWithTag("Camera").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent(CinemachineCore.Stage.Body);

        // 条件の型に派生できるならカメラの距離を変更する
        if (componentBase is CinemachineFramingTransposer)
        {
            (componentBase as CinemachineFramingTransposer).m_CameraDistance = Parameter.CAMERA_OUT_OF_RANGE_DISTANCE;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
