using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Serializable]
    public struct Controller
    {
        public InputActionReference Move;
        public InputActionReference Attack;
        public InputActionReference Dash;
        public InputActionReference ChangeWeaponToLeft;
        public InputActionReference ChangeWeaponToRight;
        public InputActionReference AdjustCameraDistance;
        public InputActionReference Resurrection;

        public void Enable()
        {
            List<InputActionReference> inputActionReferences = new List<InputActionReference>(7)
            {
                Move, Attack, Dash, ChangeWeaponToLeft, ChangeWeaponToRight,AdjustCameraDistance,Resurrection,
            };
            foreach (var inputActionReference in inputActionReferences)
            {
                inputActionReference.action.Enable();
            }
        }

        public void Disable()
        {
            List<InputActionReference> inputActionReferences = new List<InputActionReference>(6)
            {
                Move, Attack, Dash, ChangeWeaponToLeft, ChangeWeaponToRight,AdjustCameraDistance,
            };
            foreach (var inputActionReference in inputActionReferences)
            {
                inputActionReference.action.Disable();
            }

        }
    }
    [SerializeField]
    private Controller m_playerController;
    public Controller Player_Controller => m_playerController;
    GameObject m_player;
    GameObject m_camera;
    private void Start()
    {
        m_player = GameObject.Find("Player");
        m_camera = GameObject.Find("CM vcam1");
        m_playerController.Enable();
        m_playerController.Move.action.performed += m_player.GetComponent<Player>().Move;
        m_playerController.Move.action.canceled += m_player.GetComponent<Player>().MoveEnd;
        m_playerController.Attack.action.started += m_player.GetComponent<Player>().Attack;
        m_playerController.Dash.action.started += m_player.GetComponent<Player>().Dash;
        m_playerController.Dash.action.canceled += m_player.GetComponent<Player>().Dash;
        m_playerController.ChangeWeaponToLeft.action.started += m_player.GetComponent<Player>().SelectWeaponToLeft;
        m_playerController.ChangeWeaponToRight.action.started += m_player.GetComponent<Player>().SelectWeaponToRight;
        m_playerController.AdjustCameraDistance.action.started += m_camera.GetComponent<FollowCamera>().AdjustCameraDistance;
        m_playerController.Resurrection.action.started += m_player.GetComponent<Player>().Resurrection;

    }

    private void OnDisable()
    {
        m_playerController.Disable();
    }
}
