using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : BaseManager
{
    public AudioManager(GameFacade facade) : base(facade)
    {
    }


    private Dictionary<string, AudioClip> clipDict = new Dictionary<string, AudioClip>();

    private const string sound_Prefix = "Sounds/";
    public const string Sound_Alert = "Alert";
    public const string Sound_ArrowShoot = "ArrowShoot";
    public const string Sound_Bg_Fast = "Bg(fast)";
    public const string Sound_Bg_Moderate = "Bg(moderate)";
    public const string Sound_ButtonClick = "ButtonClick";
    public const string Sound_Miss = "Miss";
    public const string Sound_ShootPerson = "ShootPerson";
    public const string Sound_Timer = "Timer";
    private AudioSource bgAudioSource;
    private AudioSource normalAudioSource;


    public override void OnInit()
    {
        GameObject audioSourceGO = new GameObject("AudioSource(GameObject)");

        bgAudioSource = audioSourceGO.AddComponent<AudioSource>();
        normalAudioSource = audioSourceGO.AddComponent<AudioSource>();

        //Play bg Audio
//        PlaySound(bgAudioSource, GetSound(Sound_Bg_Moderate), 0.5f, true);
    }

    public void PlayBgSound(string soundName)
    {
        PlaySound(bgAudioSource, GetSound(soundName), 0.3f, true);
    }

    public void PlayNormalSound(string soundName)
    {
        PlaySound(normalAudioSource, GetSound(soundName), 1f);
    }

    private void PlaySound(AudioSource audioSource, AudioClip clip, float volume, bool loop = false)
    {
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.loop = loop;
    }

    private AudioClip GetSound(string soundsName)
    {
        AudioClip audioClip = clipDict.TryGet(soundsName);
        if (audioClip == null)
        {
            audioClip = LoadSound(soundsName);
        }

        return audioClip;
    }



    private AudioClip LoadSound(string soundsName)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(sound_Prefix + soundsName);
        clipDict.Add(soundsName,audioClip);
        return  audioClip;
    }
}