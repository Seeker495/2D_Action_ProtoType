using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript_2 : MonoBehaviour
{
    // サウンドマネージャー
    [SerializeField]
    SoundManager_2 soundManager_2;

    // bgmを流す番号
    [SerializeField]
    int soundNum;

    void Start()
    {
        soundManager_2.PlayBgm(soundNum);
    }
    private void Update()
    {
    }

}