using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_2 : MonoBehaviour
{
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
        AudioInfo audio;
        //audio.clip = m_bgmList;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
