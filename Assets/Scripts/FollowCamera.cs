using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*******************************************************************
 *  <�T�v>
 *  �Ǐ]�J�����̃N���X�B
 *  <�d�g��>
 *  Cinemachine�Ƃ����A�Z�b�g��p���đΏۂ̃I�u�W�F�N�g��Ǐ]�ł���悤�ɂ��Ă���B
 *******************************************************************/
public class FollowCamera : MonoBehaviour
{
    // �J�����̋����䗦�������񋓑�
    private enum eCameraDistanceRatio
    {
        NEAR,        // �߂�
        NORAML,      // ����
        FAR,         // ����
        RATIO_NUM,   // �䗦�̑���
    }

    // ���݂̃J�����̋����䗦
    private eCameraDistanceRatio m_cameraDistanceRatio = eCameraDistanceRatio.NORAML;

    // �J�����̋������i�[����z��
    private Dictionary<eCameraDistanceRatio, float> m_cameraDistance = new Dictionary<eCameraDistanceRatio, float>((int)eCameraDistanceRatio.RATIO_NUM)
    {
        {eCameraDistanceRatio.NEAR,     Parameter.CAMERA_NEAR_DISTANCE },
        {eCameraDistanceRatio.NORAML,   Parameter.CAMERA_MIDDLE_DISTANCE },
        {eCameraDistanceRatio.FAR,      Parameter.CAMERA_FAR_DISTANCE },
    };

    private void OnEnable()
    {
        PlayerController.Controller.Play.AdjustCameraDistance.started += AdjustCameraDistance;
    }

    private void OnDisable()
    {
        PlayerController.Controller.Play.AdjustCameraDistance.started -= AdjustCameraDistance;
    }

    /*******************************************************************
     *  �J�����̋����𒲐�����(�J�����̋����̐؂�ւ�)
     *******************************************************************/
    public void AdjustCameraDistance(InputAction.CallbackContext context)
    {
        // ���݂̃J�����̋����䗦��񋓑̂��琮���ɂ��Ď擾
        int cameraDistanceRatio = (int)m_cameraDistanceRatio;

        // ���̃J�����̋����䗦��񋓑̂ɂ��đ��
        m_cameraDistanceRatio = (eCameraDistanceRatio)(Math.Abs(++cameraDistanceRatio) % (int)eCameraDistanceRatio.RATIO_NUM);

        // ���̃R���|�[�l���g���擾
        CinemachineComponentBase componentBase = GameObject.FindWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent(CinemachineCore.Stage.Body);

        // �����̌^�ɔh���ł���Ȃ�J�����̋�����ύX����
        if (componentBase is CinemachineFramingTransposer)
        {
            (componentBase as CinemachineFramingTransposer).m_CameraDistance = m_cameraDistance[m_cameraDistanceRatio];
        }
    }
}
