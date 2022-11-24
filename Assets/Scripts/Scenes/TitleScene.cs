using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

/*******************************************************************
 *  <�T�v>
 *  �^�C�g���V�[���̃I�u�W�F�N�g���Ǘ�����N���X�B
 *******************************************************************/
public class TitleScene : MonoBehaviour
{
    [SerializeField]
    private GameObject m_playerController;
    [SerializeField]
    SoundManager_2 soundManager_2;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_playerController = Instantiate(m_playerController, null);
        InitializeParameter();
    }

    // Start is called before the first frame update
    // ������
    void Start()
    {
    }

    // Update is called once per frame
    // �X�V
    void Update()
    {
    }

    // �Q�[���X�^�[�g�̃{�^��
    public void Press_Start()
    {

        Debug.Log("Press_Start");
        Parameter.NEXT_SCENE_NAME = "Play";
        m_playerController.GetComponent<PlayerController>().Disable();
        SceneManager.LoadSceneAsync("Loading");


        // ���艹
        soundManager_2.PlaySe(0);
    }
    // �R���e�B�j���[�{�^��
    public void Press_Continue()
    {
        Debug.Log("Press_Continue");
        // ���艹
        soundManager_2.PlaySe(0);
    }
    // �G���h���X�{�^��
     public void Press_Endless()
    {

        Debug.Log("Press_Endless");
        // ���艹
        soundManager_2.PlaySe(0);
    }
    // �R���N�V�����{�^��
     public void Press_Collection()
    {

        Debug.Log("Press_Collection");
        // ���艹
        soundManager_2.PlaySe(0);
    }

    // �I�v�V�����̃{�^��
     public void Press_Option()
    {
        Debug.Log("Press_Option");
        // ���艹
        soundManager_2.PlaySe(0);
    }

    // �Q�[�����I���
    public void Press_Quit()
    {
        Debug.Log("Press_Quit");
        // ���艹
        soundManager_2.PlaySe(0);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    private void InitializeParameter()
    {
        Parameter.CURRENT_ALIVE_DAY = 0;
        Parameter.CURRENT_SCORE = 0;
    }

}
