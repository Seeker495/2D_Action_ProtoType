using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript_2 : MonoBehaviour
{

    [SerializeField]
    SoundManager soundManager;

    [SerializeField]
    AudioClip clip;

    void Start()
    {
        soundManager.PlayBgm(clip);
    }
    private void Update()
    {
    }

}