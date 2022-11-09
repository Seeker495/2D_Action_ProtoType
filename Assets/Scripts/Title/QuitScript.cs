using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitScript : MonoBehaviour
{
    public menu_Script menu_Script;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 画像を透過させる処理
        Color color = gameObject.GetComponent<Image>().color;

        Vector2 targetSize;
        if (menu_Script.GetButtonNum() == (int)menu_Script.MENU_TYPE.QUIT)
        {
            // ボタンのサイズを300x200に変更する
            targetSize = menu_Script.SizeSelect;

            // 画像を透過させる処理
            color.r = menu_Script.ColorSelect.x;
            color.g = menu_Script.ColorSelect.y;
            color.b = menu_Script.ColorSelect.z;
            color.a = 1.0f;
        }
        else
        {
            targetSize = menu_Script.SizeNotSelect;
            color.r = menu_Script.ColorNotSelect.x;
            color.g = menu_Script.ColorNotSelect.y;
            color.b = menu_Script.ColorNotSelect.z;
            color.a = 0.0f;
        }

        gameObject.GetComponent<Image>().color = color;

        GetComponent<RectTransform>().sizeDelta = targetSize;
    }
}
