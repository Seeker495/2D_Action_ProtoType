using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeScript_2 : MonoBehaviour
{
    // サウンドマネージャー
    [SerializeField]
    SoundManager_2 soundManager_2;

    // 音を流す番号
    [SerializeField]
    int soundNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // se音を流す
        soundManager_2.PlayBgm(soundNum);
    }
}
