using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_2 : MonoBehaviour
{
    // オーディオソース(BGM)
    [SerializeField]
    AudioSource bgmAudioSource;

    // オーディオソース(SE)
    [SerializeField]
    AudioSource seAudioSource;

    // オーディオの構造体
    [System.Serializable]
    public struct AudioInfo 
    {
        // 音の名前
        public string name;

        // 音
        public AudioClip clip;
    }

    public List<AudioInfo> m_bgmList = new List<AudioInfo>();
    public List<AudioInfo> m_seList = new List<AudioInfo>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    // bgmを流す
    public void PlayBgm(int num)
    {
        bgmAudioSource.clip = m_bgmList[num].clip;
        if (m_bgmList[num].clip == null)
        {
            return;
        }
        bgmAudioSource.Play();
    }

    // seを鳴らす
    public void PlaySe(int num)
    {
        seAudioSource.clip = m_seList[num].clip;
        if (m_seList[num].clip == null)
        {
            return;
        }
        seAudioSource.Play();
    }
}
