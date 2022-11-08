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
    GameObject m_titleText;

    [SerializeField]
    SoundManager_2 soundManager_2;

    // Start is called before the first frame update
    void Start()
    {
        // �^�C�g���p��BGM��炷
        soundManager_2.PlayBgm("�^�C�g���V�[����");
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void Press_Start()
    {

        Debug.Log("Press_Start");
        Parameter.NEXT_SCENE_NAME = "Play";
        SceneManager.LoadSceneAsync("Loading");
        // ���艹
        soundManager_2.PlaySe(0);
    }

    public void Press_Option()
    {

        Debug.Log("Press_Option");
        // ���艹
        soundManager_2.PlaySe(0);
    }

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
}
