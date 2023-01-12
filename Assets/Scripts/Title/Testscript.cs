using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Testscript : MonoBehaviour
{

    public menu_Script menu_Script;

    float keepPos = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Color color = gameObject.GetComponent<Image>().color;        
        // 画像を透過させる処理
        color.a = 1.0f;
        gameObject.GetComponent<Image>().color = color;

    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;
        // 座標を取得
        Vector3 pos = myTransform.position;

        keepPos = -menu_Script.GetButtonNum() * 100;

    
        pos.y = keepPos + 600;


        myTransform.position = pos;  // 座標を設定
    }
}
