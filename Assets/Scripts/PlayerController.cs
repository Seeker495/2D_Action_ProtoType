using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
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

            menu_Script menu = GameObject.FindWithTag("Menu").GetComponent<menu_Script>();
            m_controlActions.Title.SelectUp.started += menu.SelectUp;
            m_controlActions.Title.SelectDown.started += menu.SelectDown;
            m_controlActions.Title.EnterProcess.started += menu.EnterProcess;
            m_controlActions.Title.Enable();
        }
        else if (currentSceneName.Contains("Play"))
        {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            PlayScene playScene = GameObject.FindWithTag("PlayScene").GetComponent<PlayScene>();
            FollowCamera camera = GameObject.FindWithTag("VirtualCamera").GetComponent<FollowCamera>();
            PauseDisplay pauseDisplay = GameObject.FindWithTag("Pause").GetComponent<PauseDisplay>();
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
            m_controlActions.Pause.SelectUp.started += pauseDisplay.SelectUp;
            m_controlActions.Pause.SelectDown.started += pauseDisplay.SelectDown;
            m_controlActions.Pause.Enter.started += pauseDisplay.Enter;
            m_controlActions.Play.Enable();
        }
        else if (currentSceneName.Contains("Result"))
        {
            ResultScene resultScene = GameObject.FindWithTag("ResultScene").GetComponent<ResultScene>();
            m_controlActions.Result.BackToTitle.started += resultScene.BackToTitle;
            m_controlActions.Result.Enable();
        }


    }

    public void Disable()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName.Contains("Title"))
        {

            menu_Script menu = GameObject.FindWithTag("Menu").GetComponent<menu_Script>();
            m_controlActions.Title.SelectUp.started -= menu.SelectUp;
            m_controlActions.Title.SelectDown.started -= menu.SelectDown;
            m_controlActions.Title.EnterProcess.started -= menu.EnterProcess;
            m_controlActions.Title.Disable();
        }
        else if (currentSceneName.Contains("Play"))
        {
            Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
            PlayScene playScene = GameObject.FindWithTag("PlayScene").GetComponent<PlayScene>();
            FollowCamera camera = GameObject.FindWithTag("VirtualCamera").GetComponent<FollowCamera>();
            PauseDisplay pauseDisplay = GameObject.FindWithTag("Pause").GetComponent<PauseDisplay>();
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
            m_controlActions.Pause.SelectUp.started -= pauseDisplay.SelectUp;
            m_controlActions.Pause.SelectDown.started -= pauseDisplay.SelectDown;
            m_controlActions.Pause.Enter.started -= pauseDisplay.Enter;
            m_controlActions.Play.Disable();
            m_controlActions.Pause.Disable();
        }
        else if (currentSceneName.Contains("Result"))
        {
            ResultScene resultScene = GameObject.FindWithTag("ResultScene").GetComponent<ResultScene>();
            m_controlActions.Result.BackToTitle.started -= resultScene.BackToTitle;
            m_controlActions.Result.Disable();
        }

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
