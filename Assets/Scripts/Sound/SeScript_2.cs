using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeScript_2 : MonoBehaviour
{
    // �T�E���h�}�l�[�W���[
    [SerializeField]
    SoundManager_2 soundManager_2;

    // ���𗬂��ԍ�
    [SerializeField]
    int soundNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // se���𗬂�
        soundManager_2.PlayBgm(soundNum);
    }
}
