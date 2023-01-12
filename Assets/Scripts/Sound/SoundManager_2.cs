using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_2 : MonoBehaviour
{
    // �I�[�f�B�I�\�[�X(BGM)
    [SerializeField]
    AudioSource bgmAudioSource;

    // �I�[�f�B�I�\�[�X(SE)
    [SerializeField]
    AudioSource seAudioSource;

    [SerializeField]
    AudioSource pinchSource;

    // �I�[�f�B�I�̍\����
    [System.Serializable]
    public struct AudioInfo 
    {
        // ���̖��O
        public string name;

        // ��
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

    // bgm�𗬂�
    public void PlayBgm(int num)
    {
        bgmAudioSource.clip = m_bgmList[num].clip;
        if (m_bgmList[num].clip == null)
        {
            return;
        }
        bgmAudioSource.Play();
    }

    // se��炷
    public void PlaySe(int num)
    {
        seAudioSource.clip = m_seList[num].clip;
        if (m_seList[num].clip == null)
        {
            return;
        }
        seAudioSource.Play();
    }

    // se�𗬂�
    public void PlaySe(string name)
    {
        seAudioSource.clip = m_seList.Find(bgm => bgm.name == name).clip;
        if (seAudioSource.clip == null)
        {
            return;
        }
        if(name == "�s���`")
        {
            pinchSource.clip = seAudioSource.clip;
            seAudioSource.clip = null;
            pinchSource.volume = 0.2f;
            if(!pinchSource.isPlaying)
                pinchSource.Play();
        }
        seAudioSource.PlayOneShot(seAudioSource.clip);

    }

    // bgm��炷
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
