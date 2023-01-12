using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMode : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField] private TextMeshProUGUI m_hp;
    [SerializeField] private TextMeshProUGUI m_attack;
    [SerializeField] private TextMeshProUGUI m_defence;
    [SerializeField] private TextMeshProUGUI m_foodGauge;
    [SerializeField] private TextMeshProUGUI m_waterGauge;
    [SerializeField] private GameObject m_focus;
    private int m_index = 0;
    private PlayerStatus m_playerStatus;
    private void OnEnable()
    {
        PlayerController.Controller.Play.Disable();
        PlayerController.Controller.Debug.DebugMode_Left.started += SelectLeft;
        PlayerController.Controller.Debug.DebugMode_Left.performed += SelectLeft;

        PlayerController.Controller.Debug.DebugMode_Right.started += SelectRight;
        PlayerController.Controller.Debug.DebugMode_Right.performed += SelectRight;
        PlayerController.Controller.Debug.DebugMode_Up.started += SelectUp;
        PlayerController.Controller.Debug.DebugMode_Down.started += SelectDown;
        PlayerController.Controller.Debug.DebugMode_Cancel.started += Cancel;
        PlayerController.Controller.Debug.DebugMode_Submit.started += Submit;
        PlayerController.Controller.Debug.Enable();

        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        m_playerStatus = player.GetStatus();
        m_hp.text = m_playerStatus.actorStatus.hp.ToString();
        m_attack.text = m_playerStatus.maxAttack.ToString();
        m_defence.text = m_playerStatus.maxdefence.ToString();
        m_foodGauge.text = m_playerStatus.foodGauge.ToString();
        m_waterGauge.text = m_playerStatus.waterGauge.ToString();
        FocusItem();
        Time.timeScale = 0.0f;

    }

    private void OnDisable()
    {
        var player = GameObject.FindWithTag("Player").GetComponent<Player>();
        PlayerController.Controller.Debug.DebugMode_Left.started -= SelectLeft;
        PlayerController.Controller.Debug.DebugMode_Left.performed -= SelectLeft;
        PlayerController.Controller.Debug.DebugMode_Right.started -= SelectRight;
        PlayerController.Controller.Debug.DebugMode_Right.performed -= SelectRight;
        PlayerController.Controller.Debug.DebugMode_Up.started -= SelectUp;
        PlayerController.Controller.Debug.DebugMode_Down.started -= SelectDown;
        PlayerController.Controller.Debug.DebugMode_Cancel.started -= Cancel;
        PlayerController.Controller.Debug.DebugMode_Submit.started -= Submit;
        PlayerController.Controller.Debug.Disable();
        m_playerStatus.actorStatus.hp = int.Parse(m_hp.text);
        m_playerStatus.maxAttack = float.Parse(m_attack.text);
        m_playerStatus.maxdefence = float.Parse(m_defence.text);
        m_playerStatus.foodGauge = int.Parse(m_foodGauge.text);
        m_playerStatus.waterGauge = int.Parse(m_waterGauge.text);
        player.SetParameter(m_playerStatus);
        PlayerController.Controller.Play.Enable();

        Time.timeScale = 1.0f;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hp = Mathf.Clamp(int.Parse(m_hp.text), 0, Parameter.PLAYER_MAX_HP);
        m_hp.text = hp.ToString();
        m_playerStatus.actorStatus.hp = int.Parse(m_hp.text);
        m_playerStatus.maxAttack = float.Parse(m_attack.text);
        m_playerStatus.maxdefence = float.Parse(m_defence.text);
        var foodGauge = Mathf.Clamp(int.Parse(m_foodGauge.text), 0, Parameter.FOOD_GAUGE_MAX);
        m_foodGauge.text = foodGauge.ToString();
        m_playerStatus.foodGauge = int.Parse(m_foodGauge.text);
        var waterGauge = Mathf.Clamp(int.Parse(m_waterGauge.text), 0, Parameter.WATER_GAUGE_MAX);
        m_waterGauge.text = waterGauge.ToString();
        m_playerStatus.waterGauge = int.Parse(m_waterGauge.text);
    }

    private void SelectUp(InputAction.CallbackContext context)
    {
        TextMeshProUGUI[] textArray = { m_hp, m_attack, m_defence, m_foodGauge, m_waterGauge };
        m_index = System.Math.Abs(--m_index + textArray.Length) % textArray.Length;
        FocusItem();
    }

    private void SelectDown(InputAction.CallbackContext context)
    {
        TextMeshProUGUI[] textArray = { m_hp, m_attack, m_defence, m_foodGauge, m_waterGauge };
        m_index = System.Math.Abs(++m_index) % textArray.Length;
        FocusItem();
    }

    private void SelectRight(InputAction.CallbackContext context)
    {

        TextMeshProUGUI[] textArray = { m_hp, m_attack, m_defence, m_foodGauge, m_waterGauge };
        var value = m_index == 1 || m_index == 2 ? float.Parse(textArray[m_index].text) : int.Parse(textArray[m_index].text);
        value++;
        textArray[m_index].text = value.ToString();
    }

    private void SelectLeft(InputAction.CallbackContext context)
    {
        TextMeshProUGUI[] textArray = { m_hp, m_attack, m_defence, m_foodGauge, m_waterGauge };
        var value = m_index == 1 || m_index == 2 ? float.Parse(textArray[m_index].text) : int.Parse(textArray[m_index].text);
        value--;
        textArray[m_index].text = value.ToString();
    }

    private void Cancel(InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
    }

    private void Submit(InputAction.CallbackContext context)
    {
        gameObject.SetActive(false);
    }

    private void FocusItem()
    {
        TextMeshProUGUI[] textArray = { m_hp, m_attack, m_defence, m_foodGauge, m_waterGauge };
        m_focus.transform.DOMoveY(textArray[m_index].transform.position.y, 0.2f).SetUpdate(true);
    }
#endif
}
