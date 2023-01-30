using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionWindow : MonoBehaviour
{
    [SerializeField] private Slider m_bgmSlider;
    [SerializeField] private Slider m_sfxSlider;
    [SerializeField] private TextMeshProUGUI m_bgmText;
    [SerializeField] private TextMeshProUGUI m_sfxText;
    [SerializeField] private Button m_closeButton;
    [SerializeField] private GameObject m_focusObject;
    private int m_index = 1;
    private List<GameObject> m_objects;

    private void Awake()
    {
        m_bgmSlider.value = 0.15f;
        m_sfxSlider.value = 0.5f;
        SoundPlayer.BGM_Volume = m_bgmSlider.value;
        SoundPlayer.SFX_Volume = m_sfxSlider.value;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerController.Controller.Title.Disable();
        PlayerController.Controller.DebugTitle.Disable();
        PlayerController.Controller.Option.Enable();

        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(0.0f, 0.0f));
        sequence.Append(transform.DOScale(1.0f, 0.5f));
        sequence.Play().SetUpdate(true);
        m_bgmSlider.value = SoundPlayer.BGM_Volume;
        m_sfxSlider.value = SoundPlayer.SFX_Volume;
        m_bgmSlider.onValueChanged.AddListener((volume) => volume = SoundPlayer.BGM_Volume);
        m_sfxSlider.onValueChanged.AddListener((volume) => volume = SoundPlayer.SFX_Volume);
        m_objects = new List<GameObject>() { m_bgmSlider.gameObject, m_sfxSlider.gameObject };

        PlayerController.Controller.Option.SelectUp.started += SelectUp;
        PlayerController.Controller.Option.SelectDown.started += SelectDown;
        PlayerController.Controller.Option.EnterProcess.started += EnterProcess;
        PlayerController.Controller.Option.VolumeDown.started += VolumeDown;
        PlayerController.Controller.Option.VolumeUp.started += VolumeUp;
        m_index = 0;
        m_focusObject.transform.DOLocalMoveY(-(m_index * 195.0f) + 165.0f, 0.2f).SetUpdate(true);

    }

    private void OnDisable()
    {
        m_bgmSlider.onValueChanged.RemoveAllListeners();
        m_sfxSlider.onValueChanged.RemoveAllListeners();
        m_objects.Clear();

        PlayerController.Controller.Option.SelectUp.started -= SelectUp;
        PlayerController.Controller.Option.SelectDown.started -= SelectDown;
        PlayerController.Controller.Option.EnterProcess.started -= EnterProcess;
        PlayerController.Controller.Option.VolumeDown.started -= VolumeDown;
        PlayerController.Controller.Option.VolumeUp.started -= VolumeUp;

        PlayerController.Controller.Option.Disable();
        PlayerController.Controller.Title.Enable();
        PlayerController.Controller.DebugTitle.Enable();



    }

    // Update is called once per frame
    void Update()
    {
        int bgm = (int)(SoundPlayer.BGM_Volume * 100);
        int sfx = (int)(SoundPlayer.SFX_Volume * 100);
        m_bgmText.text = $"{bgm.ToString()}%";
        m_sfxText.text = $"{sfx.ToString()}%";
    }

    private void Press_Close()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.0f, 0.0f));
        sequence.Append(transform.DOScale(0.0f, 0.5f));
        sequence.Play().OnComplete(() => gameObject.SetActive(false)).SetUpdate(true);
    }

    private void SelectUp(InputAction.CallbackContext context)
    {
        m_index = (--m_index + m_objects.Count) % m_objects.Count;
        m_focusObject.transform.DOLocalMoveY(-(m_index * 195.0f) + 165.0f, 0.2f).SetUpdate(true);
    }

    private void SelectDown(InputAction.CallbackContext context)
    {
        m_index = ++m_index % m_objects.Count;
        m_focusObject.transform.DOLocalMoveY(-(m_index * 195.0f) + 165.0f, 0.2f).SetUpdate(true);
    }

    private void VolumeUp(InputAction.CallbackContext context)
    {
        switch (m_index)
        {
            case 0:
                {
                    SoundPlayer.BGM_Volume += 0.05f;
                    SoundPlayer.BGM_Volume = Mathf.Clamp01(SoundPlayer.BGM_Volume);
                    m_bgmSlider.value = SoundPlayer.BGM_Volume;
                    break;
                }
            case 1:
                {
                    SoundPlayer.SFX_Volume += 0.05f;
                    SoundPlayer.SFX_Volume = Mathf.Clamp01(SoundPlayer.SFX_Volume);
                    SoundPlayer.PlaySFX(eSFX.BOW);
                    m_sfxSlider.value = SoundPlayer.SFX_Volume;
                    break;
                }

        }
    }

    private void VolumeDown(InputAction.CallbackContext context)
    {
        switch (m_index)
        {
            case 0:
                {
                    SoundPlayer.BGM_Volume -= 0.05f;
                    SoundPlayer.BGM_Volume = Mathf.Clamp01(SoundPlayer.BGM_Volume);
                    m_bgmSlider.value = SoundPlayer.BGM_Volume;
                    break;
                }
            case 1:
                {
                    SoundPlayer.SFX_Volume -= 0.05f;
                    SoundPlayer.SFX_Volume = Mathf.Clamp01(SoundPlayer.SFX_Volume);
                    SoundPlayer.PlaySFX(eSFX.BOW);
                    m_sfxSlider.value = SoundPlayer.SFX_Volume;
                    break;
                }
        }
    }

    private void EnterProcess(InputAction.CallbackContext context)
    {
        Press_Close();
    }
}
