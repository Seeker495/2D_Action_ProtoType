using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.VersionControl;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/*******************************************************************
 *  <概要>
 *  ユーザーのコントローラークラス。
 *  <仕組み>
 *  ボタンに応じて行われるアクションが異なるため,
 *  インスペクタで対応するアクションを対応させたりする。
 *******************************************************************/
public class PlayerController : MonoBehaviour
{
    private ControlActions m_controlActions;
    public ControlActions ControlActions => m_controlActions;

    private Dictionary<string, GameObject> m_objects = new Dictionary<string, GameObject>();

    private bool m_isPause = false;

    private void Awake()
    {
        m_controlActions = new ControlActions();
    }
    private void OnEnable()
    {
        Enable();
        m_controlActions.Enable();
    }

    public void Enable()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName.Contains("Title"))
        {
            m_objects.Add("Menu", GameObject.FindWithTag("Menu"));
            menu_Script menu = m_objects["Menu"].GetComponent<menu_Script>();
            m_controlActions.Title.SelectUp.started += menu.SelectUp;
            m_controlActions.Title.SelectDown.started += menu.SelectDown;
            m_controlActions.Title.EnterProcess.started += menu.EnterProcess;
            m_controlActions.Title.Enable();
        }
        else if (currentSceneName.Contains("Play"))
        {
            m_objects.Add("Player", GameObject.FindWithTag("Player"));
            m_objects.Add("PlayScene", GameObject.FindWithTag("PlayScene"));
            m_objects.Add("VirtualCamera", GameObject.FindWithTag("VirtualCamera"));
            m_objects.Add("Pause", GameObject.FindWithTag("Pause"));

            Player player = m_objects["Player"].GetComponent<Player>();
            PlayScene playScene = m_objects["PlayScene"].GetComponent<PlayScene>();
            FollowCamera camera = m_objects["VirtualCamera"].GetComponent<FollowCamera>();
            PauseDisplay pauseDisplay = m_objects["Pause"].GetComponent<PauseDisplay>();
            m_controlActions.Play.Move.performed += player.Move;
            m_controlActions.Play.Move.canceled += player.MoveEnd;
            m_controlActions.Play.Attack.started += player.Attack;
            m_controlActions.Play.Dash.started += player.Dash;
            m_controlActions.Play.Dash.canceled += player.Dash;
            m_controlActions.Play.ChangeWeaponToLeft.started += player.SelectWeaponToLeft;
            m_controlActions.Play.ChangeWeaponToRight.started += player.SelectWeaponToRight;
            m_controlActions.Play.AdjustCameraDistance.started += camera.AdjustCameraDistance;
            m_controlActions.Play.Resurrection.started += player.Resurrection;
            m_controlActions.Play.HighSpeedMove.started += player.HighSpeedMove;
            m_controlActions.Play.ToPause.started += playScene.Pause;
            m_controlActions.Pause.SelectLeft.started += pauseDisplay.SelectLeft;
            m_controlActions.Pause.SelectRight.started += pauseDisplay.SelectRight;
            m_controlActions.Pause.Enter.started += pauseDisplay.Enter;
            m_controlActions.Play.Enable();
        }
        else if (currentSceneName.Contains("Result"))
        {
            m_objects.Add("ResultScene", GameObject.FindWithTag("ResultScene"));
            ResultScene resultScene = m_objects["ResultScene"].GetComponent<ResultScene>();
            m_controlActions.Result.BackToTitle.started += resultScene.BackToTitle;
            m_controlActions.Result.Enable();
        }


    }

    public void Disable()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName.Contains("Title"))
        {

            menu_Script menu = m_objects["Menu"].GetComponent<menu_Script>();
            m_controlActions.Title.SelectUp.started -= menu.SelectUp;
            m_controlActions.Title.SelectDown.started -= menu.SelectDown;
            m_controlActions.Title.EnterProcess.started -= menu.EnterProcess;
            m_controlActions.Title.Disable();
        }
        else if (currentSceneName.Contains("Play"))
        {
            Player player = m_objects["Player"].GetComponent<Player>();
            PlayScene playScene = m_objects["PlayScene"].GetComponent<PlayScene>();
            FollowCamera camera = m_objects["VirtualCamera"].GetComponent<FollowCamera>();
            PauseDisplay pauseDisplay = m_objects["Pause"].GetComponent<PauseDisplay>();
            m_controlActions.Play.Move.performed -= player.Move;
            m_controlActions.Play.Move.canceled -= player.MoveEnd;
            m_controlActions.Play.Attack.started -= player.Attack;
            m_controlActions.Play.Dash.started -= player.Dash;
            m_controlActions.Play.Dash.canceled -= player.Dash;
            m_controlActions.Play.ChangeWeaponToLeft.started -= player.SelectWeaponToLeft;
            m_controlActions.Play.ChangeWeaponToRight.started -= player.SelectWeaponToRight;
            m_controlActions.Play.AdjustCameraDistance.started -= camera.AdjustCameraDistance;
            m_controlActions.Play.Resurrection.started -= player.Resurrection;
            m_controlActions.Play.HighSpeedMove.started -= player.HighSpeedMove;
            m_controlActions.Play.ToPause.started -= playScene.Pause;
            m_controlActions.Pause.SelectLeft.started -= pauseDisplay.SelectLeft;
            m_controlActions.Pause.SelectRight.started -= pauseDisplay.SelectRight;
            m_controlActions.Pause.Enter.started -= pauseDisplay.Enter;
            m_controlActions.Play.Disable();
            m_controlActions.Pause.Disable();
        }
        else if (currentSceneName.Contains("Result"))
        {
            ResultScene resultScene = m_objects["ResultScene"].GetComponent<ResultScene>();
            m_controlActions.Result.BackToTitle.started -= resultScene.BackToTitle;
            m_controlActions.Result.Disable();
        }
        m_objects.Clear();
    }


    public bool IsPause()
    {
        return m_isPause;
    }

    public void SetPause(in bool isPause)
    {
        m_isPause = isPause;
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("Play"))
        {
            if (m_isPause)
            {
                m_controlActions.Play.Disable();
                m_controlActions.Pause.Enable();
            }
            else
            {
                m_controlActions.Pause.Disable();
                m_controlActions.Play.Enable();
            }
        }
    }
}
