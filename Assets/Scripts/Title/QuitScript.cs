using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitScript : MonoBehaviour
{

    public TitleScene titleScene;
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

        if (titleScene.GetButtonNum() == 6)
        {
            // ボタンのサイズを300x200に変更する
            targetSize = new Vector2(170, 40);

            // 画像を透過させる処理
            color.r = 0.0f;
            color.g = 1.0f;
            color.b = 0.0f;
            color.a = 1.0f;
            gameObject.GetComponent<Image>().color = color;
        }
        else
        {
            targetSize = new Vector2(160, 30);
            color.r = 0.0f;
            color.g = 0.0f;
            color.b = 0.0f;
            color.a = 0.0f;
        }

        gameObject.GetComponent<Image>().color = color;

        GetComponent<RectTransform>().sizeDelta = targetSize;
    }
}
