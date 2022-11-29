using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // ���̃R���|�[�l���g���擾
        CinemachineComponentBase componentBase = GameObject.FindWithTag("Camera").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent(CinemachineCore.Stage.Body);

        // �����̌^�ɔh���ł���Ȃ�J�����̋�����ύX����
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
