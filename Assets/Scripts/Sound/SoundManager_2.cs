using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class SoundManager_2 : MonoBehaviour
{
    [System.Serializable]
    public struct AudioInfo 
    {
        public string name;
        public AudioClip clip;
    }

    [SerializeField]
    AudioSource bgmAudioSource;

    [SerializeField]
    AudioSource seAudioSource;


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
    public void PlayBgm(int sound)
    {
        bgmAudioSource.clip = m_bgmList[sound].clip;
        if (bgmAudioSource.clip == null)
        {
            return;
        }
        bgmAudioSource.Play();
    }
    public void PlaySe(int sound)
    {
        seAudioSource.clip = m_seList[sound].clip;
        if (bgmAudioSource.clip == null)
        {
            return;
        }
        seAudioSource.Play();
    }
}
