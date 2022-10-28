using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeScript : MonoBehaviour
{
    [SerializeField]
    SoundManager soundManager;

    [SerializeField]
    AudioClip audioClip;

    [SerializeField]
    KeyCode keyCode;


    void Start()
    {
        // soundManager = GetComponent<SoundManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            Debug.Log("hivl;");
            soundManager.PlaySe(audioClip);
        }
    }

    private void Inputnoyatu(int clipType, KeyCode code)
    {

    }

}