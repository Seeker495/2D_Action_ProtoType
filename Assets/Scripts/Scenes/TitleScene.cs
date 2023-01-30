using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

/*******************************************************************
 *  <概要>
 *  タイトルシーンのオブジェクトを管理するクラス。
 *******************************************************************/
public class TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject m_soundManagerObject;
    [SerializeField] private GameObject m_optionWindow;
    [SerializeField] private TextMeshProUGUI m_highScoreText;
    private void OnEnable()
    {
        ScoreFile.Load();
        m_highScoreText.text = $"High Score:{ScoreFile.GetHighScore().ToString("#,#")}";
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        InitializeParameter();
        m_soundManagerObject = Instantiate(m_soundManagerObject, null);
        SoundPlayer.SetUp(m_soundManagerObject);
        SoundPlayer.PlayBGM(eBGM.TITLE);

        PlayerController.Controller.Title.Enable();

#if UNITY_EDITOR
        PlayerController.Controller.DebugTitle.Stage1.started += Press_Start_Debug_1;
        PlayerController.Controller.DebugTitle.Stage2.started += Press_Start_Debug_2;
        PlayerController.Controller.DebugTitle.Stage3.started += Press_Start_Debug_3;
        PlayerController.Controller.DebugTitle.Stage4.started += Press_Start_Debug_4;
        PlayerController.Controller.DebugTitle.Stage5.started += Press_Start_Debug_5;
        PlayerController.Controller.DebugTitle.Stage6.started += Press_Start_Debug_6;
        PlayerController.Controller.DebugTitle.Stage7.started += Press_Start_Debug_7;
        PlayerController.Controller.DebugTitle.Stage8.started += Press_Start_Debug_8;
        PlayerController.Controller.DebugTitle.Stage9.started += Press_Start_Debug_9;
        PlayerController.Controller.DebugTitle.Enable();
#endif
    }

    private void OnDisable()
    {
        PlayerController.Controller.Title.Disable();
#if UNITY_EDITOR
        PlayerController.Controller.DebugTitle.Stage1.started -= Press_Start_Debug_1;
        PlayerController.Controller.DebugTitle.Stage2.started -= Press_Start_Debug_2;
        PlayerController.Controller.DebugTitle.Stage3.started -= Press_Start_Debug_3;
        PlayerController.Controller.DebugTitle.Stage4.started -= Press_Start_Debug_4;
        PlayerController.Controller.DebugTitle.Stage5.started -= Press_Start_Debug_5;
        PlayerController.Controller.DebugTitle.Stage6.started -= Press_Start_Debug_6;
        PlayerController.Controller.DebugTitle.Stage7.started -= Press_Start_Debug_7;
        PlayerController.Controller.DebugTitle.Stage8.started -= Press_Start_Debug_8;
        PlayerController.Controller.DebugTitle.Stage9.started -= Press_Start_Debug_9;
        PlayerController.Controller.DebugTitle.Disable();
#endif
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    // 初期化
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    // 更新
    void Update()
    {
    }

#if UNITY_EDITOR

    private void DebugStart()
    {
        Parameter.IS_DEBUG_MODE = true;
        Debug.Log("Press_Start");
        Parameter.NEXT_SCENE_NAME = "Play";
        SceneManager.LoadSceneAsync("Loading");
    }


    private void Press_Start_Debug_1(InputAction.CallbackContext context)
    {
        Parameter.DEBUG_MAP_INDEX = 0;
        DebugStart();
    }

    private void Press_Start_Debug_2(InputAction.CallbackContext context)
    {
        Parameter.DEBUG_MAP_INDEX = 1;
        DebugStart();
    }

    private void Press_Start_Debug_3(InputAction.CallbackContext context)
    {
        Parameter.DEBUG_MAP_INDEX = 2;
        DebugStart();
    }

    private void Press_Start_Debug_4(InputAction.CallbackContext context)
    {
        Parameter.DEBUG_MAP_INDEX = 3;
        DebugStart();
    }

    private void Press_Start_Debug_5(InputAction.CallbackContext context)
    {
        Parameter.DEBUG_MAP_INDEX = 4;
        DebugStart();
    }

    private void Press_Start_Debug_6(InputAction.CallbackContext context)
    {
        Parameter.DEBUG_MAP_INDEX = 5;
        DebugStart();
    }

    private void Press_Start_Debug_7(InputAction.CallbackContext context)
    {
        Parameter.DEBUG_MAP_INDEX = 6;
        DebugStart();
    }

    private void Press_Start_Debug_8(InputAction.CallbackContext context)
    {
        Parameter.DEBUG_MAP_INDEX = 7;
        DebugStart();
    }

    private void Press_Start_Debug_9(InputAction.CallbackContext context)
    {
        Parameter.DEBUG_MAP_INDEX = 8;
        DebugStart();
    }

    private void Press_Start_Debug_0(InputAction.CallbackContext context)
    {
        Parameter.DEBUG_MAP_INDEX = 9;
        DebugStart();
    }
#endif



    // ゲームスタートのボタン
    public void Press_Start()
    {
        Debug.Log("Press_Start");
        Parameter.NEXT_SCENE_NAME = "Play";
        SceneManager.LoadSceneAsync("Loading");

        // 決定音
        SoundPlayer.PlaySFX(eSFX.DECISION);
    }
    // コンティニューボタン
    public void Press_Continue()
    {
        Debug.Log("Press_Continue");
        // 決定音
        SoundPlayer.PlaySFX(eSFX.DECISION);
    }
    // エンドレスボタン
    public void Press_Endless()
    {

        Debug.Log("Press_Endless");
        // 決定音
        SoundPlayer.PlaySFX(eSFX.DECISION);
    }
    // コレクションボタン
    public void Press_Collection()
    {

        Debug.Log("Press_Collection");
        // 決定音
        SoundPlayer.PlaySFX(eSFX.DECISION);
    }

    // オプションのボタン
    public void Press_Option()
    {
        Debug.Log("Press_Option");
        // 決定音
        SoundPlayer.PlaySFX(eSFX.DECISION);
        m_optionWindow.SetActive(true);
    }

    // ゲームを終わる
    public void Press_Quit()
    {
        Debug.Log("Press_Quit");
        // 決定音
        SoundPlayer.PlaySFX(eSFX.DECISION);
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
#if UNITY_EDITOR
        Parameter.IS_DEBUG_MODE = false;
#endif
    }

}
