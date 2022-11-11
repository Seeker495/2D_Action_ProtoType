using System.Collections;
using System.Collections.Generic;
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

    Dictionary<MENU_TYPE, System.Action> menuProcess = new Dictionary<MENU_TYPE, System.Action>((int)MENU_TYPE.MAX);
    List<Button> buttons = new List<Button>();
    // ボタン
    int button = 0;

    public enum MENU_TYPE
    {
        NEWGAME,
        CONTINUE,
        ENDLESS,
        CORECTION,
        OPTION,
        QUIT,
        MAX
    }

    private void Awake()
    {
        buttons.AddRange(GetComponentsInChildren<Button>());
        menuProcess.Add(MENU_TYPE.NEWGAME, titleScene.Press_Start);
        menuProcess.Add(MENU_TYPE.CONTINUE, titleScene.Press_Continue);
        menuProcess.Add(MENU_TYPE.ENDLESS, titleScene.Press_Endless);
        menuProcess.Add(MENU_TYPE.CORECTION, titleScene.Press_Collection);
        menuProcess.Add(MENU_TYPE.OPTION, titleScene.Press_Option);
        menuProcess.Add(MENU_TYPE.QUIT, titleScene.Press_Quit);

    }

    // Start is called before the first frame update
    void Start()
    {
        // 画像を透過させる処理
        Color color = gameObject.GetComponent<Image>().color;
        color.r = 1.0f;
        color.g = 1.0f;
        color.b = 1.0f;
        color.a = 0.5f;
        gameObject.GetComponent<Image>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SelectUp(InputAction.CallbackContext context)
    {
        button = System.Math.Abs(--button + (int)MENU_TYPE.MAX) % (int)MENU_TYPE.MAX;
    }

    public void SelectDown(InputAction.CallbackContext context)
    {
        button = System.Math.Abs(++button) % (int)MENU_TYPE.MAX;
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
}
