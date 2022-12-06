using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

/*******************************************************************
 *  <概要>
 *  タイトルシーンのオブジェクトを管理するクラス。
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
    // 初期化
    void Start()
    {
    }

    // Update is called once per frame
    // 更新
    void Update()
    {
    }

    // ゲームスタートのボタン
    public void Press_Start()
    {

        Debug.Log("Press_Start");
        Parameter.NEXT_SCENE_NAME = "Play";
        m_playerController.GetComponent<PlayerController>().Disable();
        SceneManager.LoadSceneAsync("Loading");


        // 決定音
        soundManager_2.PlaySe(0);
    }
    // コンティニューボタン
    public void Press_Continue()
    {
        Debug.Log("Press_Continue");
        // 決定音
        soundManager_2.PlaySe(0);
    }
    // エンドレスボタン
     public void Press_Endless()
    {

        Debug.Log("Press_Endless");
        // 決定音
        soundManager_2.PlaySe(0);
    }
    // コレクションボタン
     public void Press_Collection()
    {

        Debug.Log("Press_Collection");
        // 決定音
        soundManager_2.PlaySe(0);
    }

    // オプションのボタン
     public void Press_Option()
    {
        Debug.Log("Press_Option");
        // 決定音
        soundManager_2.PlaySe(0);
    }

    // ゲームを終わる
    public void Press_Quit()
    {
        Debug.Log("Press_Quit");
        // 決定音
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
