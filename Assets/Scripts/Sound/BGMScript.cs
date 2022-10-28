using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{

    [SerializeField]
    SoundManager soundManager;

    [SerializeField]

    AudioClip clip;

    void Start()
    {
    }
    private void Update()
    {
        soundManager.PlayBgm(clip);        
    }
}
