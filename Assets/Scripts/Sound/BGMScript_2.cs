using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript_2 : MonoBehaviour
{

    [SerializeField]
    SoundManager_2 soundManager_2;

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
