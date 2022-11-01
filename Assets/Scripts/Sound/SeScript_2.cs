using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeScript_2 : MonoBehaviour
{

    [SerializeField]
    SoundManager_2 soundManager_2;

    [SerializeField]
    int soundNum;

    void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            soundManager_2.PlaySe(soundNum);
        }
    }
}
