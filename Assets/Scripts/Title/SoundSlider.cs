using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    Slider hpSlider;

    float nowHp = 0.5f;

    // Use this for initialization
    void Start()
    {
        hpSlider = GetComponent<Slider>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) && nowHp < 1)
        {            
            nowHp += 0.005f;
        }
        if (Input.GetKey(KeyCode.A) && nowHp > 0)
        {
            nowHp -= 0.005f;
        }

        //スライダーの現在値の設定
        hpSlider.value = nowHp;
    }
}
