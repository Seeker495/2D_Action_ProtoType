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

    [SerializeField]
    AudioSource pinchSource;

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

    // seを流す
    public void PlaySe(string name)
    {
        seAudioSource.clip = m_seList.Find(bgm => bgm.name == name).clip;
        if (seAudioSource.clip == null)
        {
            return;
        }
        if(name == "ピンチ")
        {
            pinchSource.clip = seAudioSource.clip;
            seAudioSource.clip = null;
            pinchSource.volume = 0.2f;
            if(!pinchSource.isPlaying)
                pinchSource.Play();
        }
        seAudioSource.PlayOneShot(seAudioSource.clip);

    }

    // bgmを鳴らす
    public void PlayBgm(string name)
    {
        bgmAudioSource.clip = m_bgmList.Find(bgm => bgm.name == name).clip;
        if (bgmAudioSource.clip == null)
        {
            return;
        }
        bgmAudioSource.Play();
    }

}
