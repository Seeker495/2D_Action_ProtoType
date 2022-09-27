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

        public void Enable()
        {
            List<InputActionReference> inputActionReferences = new List<InputActionReference>(6)
            {
                Move, Attack, Dash, ChangeWeaponToLeft, ChangeWeaponToRight,AdjustCameraDistance,
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
    Controller playerController;
    GameObject Player;
    GameObject Camera;
    private void Start()
    {
        Player = GameObject.Find("Player");
        Camera = GameObject.Find("CM vcam1");
        playerController.Enable();
        playerController.Move.action.performed += Player.GetComponent<Player>().Move;
        playerController.Move.action.canceled += Player.GetComponent<Player>().MoveEnd;
        playerController.Attack.action.started += Player.GetComponent<Player>().Attack;
        playerController.Dash.action.started += Player.GetComponent<Player>().Dash;
        playerController.Dash.action.canceled += Player.GetComponent<Player>().Dash;
        playerController.ChangeWeaponToLeft.action.started += Player.GetComponent<Player>().SelectWeaponToLeft;
        playerController.ChangeWeaponToRight.action.started += Player.GetComponent<Player>().SelectWeaponToRight;
        playerController.AdjustCameraDistance.action.started += Camera.GetComponent<FollowCamera>().AdjustCameraDistance;
    }

    private void OnDisable()
    {
        playerController.Move.action.performed -= Player.GetComponent<Player>().Move;
        playerController.Move.action.canceled -= Player.GetComponent<Player>().MoveEnd;
        playerController.Attack.action.started -= Player.GetComponent<Player>().Attack;
        playerController.Dash.action.started -= Player.GetComponent<Player>().Dash;
        playerController.Dash.action.canceled -= Player.GetComponent<Player>().Dash;
        playerController.ChangeWeaponToLeft.action.started -= Player.GetComponent<Player>().SelectWeaponToLeft;
        playerController.ChangeWeaponToRight.action.started -= Player.GetComponent<Player>().SelectWeaponToRight;
        playerController.AdjustCameraDistance.action.started -= Camera.GetComponent<FollowCamera>().AdjustCameraDistance;
        playerController.Disable();
    }
}
