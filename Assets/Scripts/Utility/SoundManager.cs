using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Serializable]
    public struct BGMInfo
    {
        public eBGM type;
        public AudioClip clip;
        [Range(0.0f, 1.0f)]
        public float volume;
        public bool isLoopMusic;
        public float startSecond;
        public float endSecond;
    }

    [Serializable]
    public struct SFXInfo
    {
        public eSFX type;
        public AudioClip clip;
        [Range(0.0f, 1.0f)]
        public float volume;
    }

    [Serializable]
    public struct BGMPlayer
    {
        public AudioSource audioSource;
        public float volume;
        public int startSample;
        public int endSample;
        public bool isLoop;
    }

    [SerializeField]
    private List<BGMInfo> m_bgmList;
    [SerializeField]
    private List<SFXInfo> m_sfxList;
    [SerializeField]
    private List<BGMPlayer> m_multiBgmPlayer;
    [SerializeField]
    private List<AudioSource> m_multiSfxPlayer;

    private List<BGMPlayer> m_currentPlayers = new List<BGMPlayer>();

    private float m_bgmVolume;
    private float m_sfxVolume;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < m_multiSfxPlayer.Count; i++)
        {
            m_multiSfxPlayer[i] = gameObject.AddComponent<AudioSource>(); ;
        }
        SoundPlayer.BGM_Volume = m_bgmVolume = m_multiBgmPlayer.First().volume;
        SoundPlayer.SFX_Volume = m_sfxVolume = m_multiSfxPlayer.First().volume;


    }

    // Update is called once per frame
    void Update()
    {
        if (SoundPlayer.BGM_Volume >= 0.01f)
        {
            m_multiBgmPlayer.ForEach(player => { player.audioSource.mute = false; });
            m_multiBgmPlayer.ForEach(player => { player.volume = SoundPlayer.BGM_Volume; player.audioSource.volume = player.audioSource.volume / m_bgmVolume * player.volume; });
            m_bgmVolume = SoundPlayer.BGM_Volume;
        }
        else
        {
            m_multiBgmPlayer.ForEach(player => { player.audioSource.mute = true; });
        }
        if (SoundPlayer.SFX_Volume >= 0.01f)
        {
            m_multiSfxPlayer.ForEach(player => { player.mute = false; });
            m_multiSfxPlayer.ForEach(player => { player.volume = SoundPlayer.SFX_Volume; player.volume = player.volume / m_sfxVolume * player.volume; });
            m_sfxVolume = SoundPlayer.SFX_Volume;
        }
        else
        {
            m_multiSfxPlayer.ForEach(player => { player.mute = true; });
        }
        if (m_currentPlayers.Count == 0) return;
        if (m_currentPlayers.First().audioSource.timeSamples >= m_currentPlayers.First().endSample - CalculateSample(0.05f) && m_currentPlayers.First().isLoop)
        {
            var unusedBgmPlayer = GetUnUsedBGMPlayer();

            unusedBgmPlayer.audioSource.clip = m_currentPlayers[0].audioSource.clip;
            unusedBgmPlayer.volume = m_currentPlayers[0].volume;
            unusedBgmPlayer.audioSource.volume = m_currentPlayers[0].audioSource.volume;
            unusedBgmPlayer.startSample = m_currentPlayers[0].startSample;
            unusedBgmPlayer.endSample = m_currentPlayers[0].endSample;
            unusedBgmPlayer.isLoop = m_currentPlayers[0].isLoop;
            unusedBgmPlayer.audioSource.Play();
            unusedBgmPlayer.audioSource.timeSamples = unusedBgmPlayer.startSample;

            m_currentPlayers.Add(unusedBgmPlayer);
            m_currentPlayers.Remove(m_currentPlayers.First());
            //m_currentPlayer.audioSource.Stop();
        }
    }

    public BGMInfo GetBGM(eBGM type)
    {
        return m_bgmList.Find(bgm => bgm.type == type);
    }

    public SFXInfo GetSFX(eSFX type)
    {
        return m_sfxList.Find(sfx => sfx.type == type);
    }

    public void PlayBGM(in eBGM type)
    {
        var bgmPlayer = GetUnUsedBGMPlayer();
        var bgm = GetBGM(type);
        bgmPlayer.audioSource.clip = bgm.clip;
        bgmPlayer.volume = bgm.volume;
        bgmPlayer.audioSource.volume = m_bgmVolume;
        if (bgm.isLoopMusic)
        {
            if (bgm.endSecond <= 0)
                bgm.endSecond = float.Parse(bgm.clip.length.ToString("f2")) - 0.01f;
            Debug.Log($"End:{bgm.endSecond}");
            bgmPlayer.endSample = CalculateSample(bgm.endSecond);
            bgmPlayer.startSample = CalculateSample(bgm.startSecond);
        }
        bgmPlayer.isLoop = bgm.isLoopMusic;
        bgmPlayer.audioSource.loop = true;
        bgmPlayer.audioSource.Play();
        m_currentPlayers.Add(bgmPlayer);
    }

    public int CalculateSample(in float second)
    {
        float secondToMill = second * 100;
        return (int)secondToMill * 441;
    }

    public void PlaySFX(in eSFX type)
    {
        var sfx = GetSFX(type);
        AudioSource sfxPlayer = GetUnUsedSFXPlayer();
        sfxPlayer.PlayOneShot(sfx.clip, m_sfxVolume * sfx.volume);
    }

    public AudioSource GetUnUsedSFXPlayer()
    {
        return m_multiSfxPlayer.Find(player => player.isPlaying == false);
    }

    public BGMPlayer GetUnUsedBGMPlayer()
    {
        return m_multiBgmPlayer.Find(player => player.audioSource.isPlaying == false);
    }
    public BGMPlayer GetUsingBGMPlayer()
    {
        return m_multiBgmPlayer.Find(player => player.audioSource.isPlaying == true);
    }

}


public enum eBGM
{
    TITLE,
    PLAY,
    RESULT,
}

public enum eSFX
{
    BLADE,
    BOW,
    CHANGE_WEAPON,
    DAMAGE,
    DECISION,
    DROP,
    FLAME,
    GET_EXP,
    HIGH_SPEED,
    WATER,
    OK,
    PINCHI,
}
