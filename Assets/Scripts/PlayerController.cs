using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
                FollowCamera camera = GameObject.FindWithTag("VirtualCamera").GetComponent<FollowCamera>();
                asset.FindActionMap("Player").FindAction("Move").performed += player.Move;
                asset.FindActionMap("Player").FindAction("Move").canceled += player.MoveEnd;
                asset.FindActionMap("Player").FindAction("Attack").started += player.Attack;
                asset.FindActionMap("Player").FindAction("Dash").started += player.Dash;
                asset.FindActionMap("Player").FindAction("Dash").canceled += player.Dash;
                asset.FindActionMap("Player").FindAction("ChangeWeaponToLeft").started += player.SelectWeaponToLeft;
                asset.FindActionMap("Player").FindAction("ChangeWeaponToRight").started += player.SelectWeaponToRight;
                asset.FindActionMap("Player").FindAction("AdjustCameraDistance").started += camera.AdjustCameraDistance;
                asset.FindActionMap("Player").FindAction("Resurrection").started += player.Resurrection;
                asset.FindActionMap("Player").FindAction("HighSpeedMove").started += player.HighSpeedMove;
                asset.FindActionMap("Player").Enable();
            }
        }

        public void Disable()
        {
            asset.Disable();
        }
    }

    [SerializeField]
    private Controller m_playerController;
    public Controller Player_Controller => m_playerController;
    [SerializeField]
    private List<InputActionAsset> m_inputActionAssets;

    private void Awake()
    {
    }
    private void OnEnable()
    {
        if (m_playerController.asset != null) m_playerController.asset = null;
        m_playerController.asset = m_inputActionAssets.Find(asset => asset.name == SceneManager.GetActiveScene().name);
        m_playerController.Enable();
    }

    private void OnDisable()
    {
        m_playerController.Disable();
    }
}
