using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_2 : MonoBehaviour
{
    [SerializeField]
    AudioSource bgmAudioSource;

    [SerializeField]
    AudioSource seAudioSource;

    [System.Serializable]
    public struct AudioInfo 
    {
        public string name;
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

    public void PlayBgm(int num)
    {
        bgmAudioSource.clip = m_bgmList[num].clip;
        if (m_bgmList[num].clip == null)
        {
            return;
        }
        bgmAudioSource.Play();
    }
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
