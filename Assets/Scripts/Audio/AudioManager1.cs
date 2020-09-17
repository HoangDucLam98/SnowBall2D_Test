using UnityEngine;
using System.Collections.Generic;

public class AudioManager1 : MonoBehaviour
{
    public static AudioManager1 Ins;
    public List<AudioInfo> infors;

    public int maxAudioSource = 6;
    private List<AudioSource> audios;

    public AudioSource musicAudio;

    private bool isMuteSound, isMuteMusic;
    private MusicType type;

    public AudioClip homeMusic, ingameMusic;

    private void Awake()
    {
        Ins = this;
        audios = new List<AudioSource>();
    }

    public void PlaySound(SoundType type)
    {
        if (isMuteSound)
            return;

        var a = GetAudioSource();
        if (a != null)
        {
            var s = GetClip(type);
            a.volume = s.volume;
            a.PlayOneShot(s.clip);
        }
    }

    public void CheckSound(int index)
    {
        switch (index)
        {
            case 1:
                PlaySound(SoundType.FirstBlood);
                break;
            case 2:
                PlaySound(SoundType.Double);
                break;
            case 3:
                PlaySound(SoundType.Triple);
                break;
            case 4:
                PlaySound(SoundType.Quadra);
                break;
            case 5:
                PlaySound(SoundType.Penta);
                break;
            case 6:
                PlaySound(SoundType.Unstoppable);
                break;
            case 7:
                PlaySound(SoundType.Rampage);
                break;
            case 8:
                PlaySound(SoundType.Legendary);
                break;
            case 9:
                PlaySound(SoundType.Legendary);
                break;
            case 10:
                PlaySound(SoundType.Legendary);
                break;
            case 11:
                PlaySound(SoundType.Legendary);
                break;
        }
    }

    public AudioSource GetAudioSource()
    {
        foreach (var item in audios)
        {
            if (!item.isPlaying)
            {
                return item;
            }
        }

        if (audios.Count < maxAudioSource)
        {
            var a = gameObject.AddComponent<AudioSource>();
            audios.Add(a);
            return a;
        }

        return null;

    }

    public void PlayMusic(MusicType type)
    {
        if (isMuteMusic)
        {
            return;
        }

        if (this.type == type && musicAudio.clip != null)
            musicAudio.Play();
        else
        {
            this.type = type;
            switch (type)
            {
                case MusicType.HomeMusic:
                    musicAudio.clip = homeMusic;
                    musicAudio.loop = true;
                    musicAudio.Play();
                    break;
                case MusicType.IngameMusic:
                    musicAudio.clip = ingameMusic;
                    musicAudio.loop = true;
                    musicAudio.Play();
                    break;
            }
        }
    }

    public void TurnOffMusic()
    {
        isMuteMusic = true;
        musicAudio.Stop();
    }

    public void TurnOnMusic()
    {
        isMuteMusic = false;
        PlayMusic(this.type);
    }

    public void TurnOffSound()
    {
        isMuteSound = true;
        foreach (var item in audios)
        {
            item.Stop();
        }
    }
    public void TurnOnSound()
    {
        isMuteSound = false;
    }

    public AudioInfo GetClip(SoundType type)
    {
        return infors.Find(s => s.type == type);
    }

}

[System.Serializable]
public struct AudioInfo
{
    public SoundType type;
    public AudioClip clip;
    public float volume;
}

public enum MusicType
{
    HomeMusic,
    IngameMusic
}

public enum SoundType
{
    Click,
    Claim,
    Win,
    Lose,
    Select,
    CoinClaim,
    Spin,
    Revive,
    UnlockFigure,
    FirstBlood,
    Double,
    Triple,
    Quadra,
    Penta,
    Unstoppable,
    Rampage,
    Legendary,
    BallCollision,
    ShowRank,
    ShowRankWin
}