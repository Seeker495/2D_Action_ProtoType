using System;
using System.Collections;
using System.Collections.Generic;
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
    [Serializable]
    public struct Controller
    {
        public InputActionAsset asset;
        public void Enable()
        {
            if (asset.name.Contains("Title"))
            {
                menu_Script menu = GameObject.FindWithTag("Menu").GetComponent<menu_Script>();
                asset.FindActionMap("UI").FindAction("SelectUp").started += menu.SelectUp;
                asset.FindActionMap("UI").FindAction("SelectDown").started += menu.SelectDown;
                asset.FindActionMap("UI").FindAction("EnterProcess").started += menu.EnterProcess;
                asset.FindActionMap("UI").Enable();
            }
            else if (asset.name.Contains("Play"))
            {
                Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
                PlayScene playScene = GameObject.FindWithTag("PlayScene").GetComponent<PlayScene>();
                FollowCamera camera = GameObject.FindWithTag("VirtualCamera").GetComponent<FollowCamera>();
                PauseDisplay pauseDisplay = GameObject.FindWithTag("Pause").GetComponent<PauseDisplay>();
                asset.FindActionMap("Play").FindAction("Move").performed += player.Move;
                asset.FindActionMap("Play").FindAction("Move").canceled += player.MoveEnd;
                asset.FindActionMap("Play").FindAction("Attack").started += player.Attack;
                asset.FindActionMap("Play").FindAction("Dash").started += player.Dash;
                asset.FindActionMap("Play").FindAction("Dash").canceled += player.Dash;
                asset.FindActionMap("Play").FindAction("ChangeWeaponToLeft").started += player.SelectWeaponToLeft;
                asset.FindActionMap("Play").FindAction("ChangeWeaponToRight").started += player.SelectWeaponToRight;
                asset.FindActionMap("Play").FindAction("AdjustCameraDistance").started += camera.AdjustCameraDistance;
                asset.FindActionMap("Play").FindAction("Resurrection").started += player.Resurrection;
                asset.FindActionMap("Play").FindAction("HighSpeedMove").started += player.HighSpeedMove;
                asset.FindActionMap("Play").FindAction("ToPause").started += playScene.Pause;
                asset.FindActionMap("Pause").FindAction("SelectUp").started += pauseDisplay.SelectUp;
                asset.FindActionMap("Pause").FindAction("SelectDown").started += pauseDisplay.SelectDown;
                asset.FindActionMap("Pause").FindAction("Enter").started += pauseDisplay.Enter;
                asset.FindActionMap("Play").Enable();
            }
            else if (asset.name.Contains("Result"))
            {
                ResultScene resultScene = GameObject.FindWithTag("ResultScene").GetComponent<ResultScene>();
                asset.FindActionMap("UI").FindAction("BackToTitle").started += resultScene.BackToTitle;
                asset.FindActionMap("UI").Enable();
            }

        }

        public void Disable()
        {
            if (asset.name.Contains("Title"))
            {
                menu_Script menu = GameObject.FindWithTag("Menu").GetComponent<menu_Script>();
                asset.FindActionMap("UI").FindAction("SelectUp").started -= menu.SelectUp;
                asset.FindActionMap("UI").FindAction("SelectDown").started -= menu.SelectDown;
                asset.FindActionMap("UI").FindAction("EnterProcess").started -= menu.EnterProcess;
                asset.FindActionMap("UI").Disable();
            }
            else if (asset.name.Contains("Play"))
            {
                Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
                PlayScene playScene = GameObject.FindWithTag("PlayScene").GetComponent<PlayScene>();
                FollowCamera camera = GameObject.FindWithTag("VirtualCamera").GetComponent<FollowCamera>();
                PauseDisplay pauseDisplay = GameObject.FindWithTag("Pause").GetComponent<PauseDisplay>();
                asset.FindActionMap("Play").FindAction("Move").performed -= player.Move;
                asset.FindActionMap("Play").FindAction("Move").canceled -= player.MoveEnd;
                asset.FindActionMap("Play").FindAction("Attack").started -= player.Attack;
                asset.FindActionMap("Play").FindAction("Dash").started -= player.Dash;
                asset.FindActionMap("Play").FindAction("Dash").canceled -= player.Dash;
                asset.FindActionMap("Play").FindAction("ChangeWeaponToLeft").started -= player.SelectWeaponToLeft;
                asset.FindActionMap("Play").FindAction("ChangeWeaponToRight").started -= player.SelectWeaponToRight;
                asset.FindActionMap("Play").FindAction("AdjustCameraDistance").started -= camera.AdjustCameraDistance;
                asset.FindActionMap("Play").FindAction("Resurrection").started -= player.Resurrection;
                asset.FindActionMap("Play").FindAction("HighSpeedMove").started -= player.HighSpeedMove;
                asset.FindActionMap("Play").FindAction("ToPause").started -= playScene.Pause;
                asset.FindActionMap("Pause").FindAction("SelectUp").started -= pauseDisplay.SelectUp;
                asset.FindActionMap("Pause").FindAction("SelectDown").started -= pauseDisplay.SelectDown;
                asset.FindActionMap("Pause").FindAction("Enter").started -= pauseDisplay.Enter;
                asset.FindActionMap("Play").Disable();
                asset.FindActionMap("Pause").Disable();
            }
            else if (asset.name.Contains("Result"))
            {
                ResultScene resultScene = GameObject.FindWithTag("ResultScene").GetComponent<ResultScene>();
                asset.FindActionMap("UI").FindAction("BackToTitle").started -= resultScene.BackToTitle;
                asset.FindActionMap("UI").Disable();
            }

        }
    }

    [SerializeField]
    private Controller m_playerController;
    public Controller Player_Controller => m_playerController;
    [SerializeField]
    private List<InputActionAsset> m_inputActionAssets;

    private bool m_isPause = false;

    private void Awake()
    {
    }
    private void OnEnable()
    {
        if (m_playerController.asset != null) m_playerController.asset = null;
        m_playerController.asset = m_inputActionAssets.Find(asset => asset.name == SceneManager.GetActiveScene().name);
        m_playerController.Enable();
    }

    public void Disable()
    {
        m_playerController.Disable();
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
        if (m_playerController.asset.name.Contains("Play"))
        {
            if (m_isPause)
            {
                m_playerController.asset.FindActionMap("Play").Disable();
                m_playerController.asset.FindActionMap("Pause").Enable();
            }
            else
            {
                m_playerController.asset.FindActionMap("Pause").Disable();
                m_playerController.asset.FindActionMap("Play").Enable();
            }
        }

    }
}
