using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class menu_Script : MonoBehaviour
{
    // ボタンのサイズ(選択前)
    public Vector2 SizeSelect;

    // ボタンの色(選択前)
    public Vector3 ColorSelect;

    // ボタンのサイズ(選択後)
    public Vector2 SizeNotSelect;

    // ボタンの色(選択後)
    public Vector3 ColorNotSelect;

    public TitleScene titleScene;
    [SerializeField]
    private GameObject FocusSelect;

    Dictionary<MENU_TYPE, System.Action> menuProcess = new Dictionary<MENU_TYPE, System.Action>((int)MENU_TYPE.MAX);
    List<Button> buttons = new List<Button>();
    // ボタン
    int button = 0;

    public enum MENU_TYPE
    {
        NEWGAME,
        OPTION,
        QUIT,
        MAX
    }

    private void OnEnable()
    {
        PlayerController.Controller.Title.SelectUp.started += SelectUp;
        PlayerController.Controller.Title.SelectDown.started += SelectDown;
        PlayerController.Controller.Title.EnterProcess.started += EnterProcess;

        buttons.AddRange(GetComponentsInChildren<Button>());
        menuProcess.Add(MENU_TYPE.NEWGAME, titleScene.Press_Start);
        menuProcess.Add(MENU_TYPE.OPTION, titleScene.Press_Option);
        menuProcess.Add(MENU_TYPE.QUIT, titleScene.Press_Quit);

    }

    private void OnDisable()
    {
        PlayerController.Controller.Title.SelectUp.started -= SelectUp;
        PlayerController.Controller.Title.SelectDown.started -= SelectDown;
        PlayerController.Controller.Title.EnterProcess.started -= EnterProcess;
    }

        // Start is called before the first frame update
        void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SelectUp(InputAction.CallbackContext context)
    {
        button = System.Math.Abs(--button + (int)MENU_TYPE.MAX) % (int)MENU_TYPE.MAX;
        FocusMenu(FocusSelect.GetComponent<LineRenderer>());
    }

    public void SelectDown(InputAction.CallbackContext context)
    {
        button = System.Math.Abs(++button) % (int)MENU_TYPE.MAX;
        FocusMenu(FocusSelect.GetComponent<LineRenderer>());
    }

    public void EnterProcess(InputAction.CallbackContext context)
    {
        if (buttons[button].IsInteractable())
            menuProcess[(MENU_TYPE)button]();
    }

    // ボタンの数字のゲット
    public int GetButtonNum()
    {
        return button;
    }

    private void FocusMenu(LineRenderer lineRenderer)
    {

        float alpha;
        if (buttons[button].IsInteractable())
            lineRenderer.colorGradient.colorKeys[1].color = Color.yellow;
        else
            lineRenderer.colorGradient.colorKeys[1].color = Color.red;
        //lineRenderer.SetPositions(new Vector3[] { new Vector3(-1.0f, 0.1f - 0.45f * button, -1.0f), new Vector3(0.0f, 0.1f - 0.45f * button, -1.0f), new Vector3(1.0f, 0.1f - 0.45f * button, -1.0f) });
        FocusSelect.transform.DOLocalMoveY(-(button * 1.2f) + 0.5f, Parameter.FOCUS_TIME).SetUpdate(true);

    }
}
