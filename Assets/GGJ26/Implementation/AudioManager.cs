using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESourceSFX
{
    Jump,
    Land,
    Hurt,
    Hover,
    Laser,
    Slash,
    Fire
}

public enum ESourceBGM
{
    Menu,
    Level
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource adudiSourceSFX;
    public AudioSource adudiSourceBGM;

    [Header("Player SFX")]
    public AudioClip hurtSFX;
    public AudioClip[] slashSFX;
    public AudioClip[] laserSFX;
    public AudioClip[] fireSFX;

    [Header("Level BGM")]
    public AudioClip levelBGM;

    [Header("User Interface")]
    public AudioClip hoverSFX;
    public AudioClip menuBGM;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Can only play one at a time
    public void PlaySFX(ESourceSFX _src)
    {
        adudiSourceSFX.Stop();
        switch(_src)
        {
            case ESourceSFX.Hover:
                adudiSourceSFX.PlayOneShot(hoverSFX);
                break;
            case ESourceSFX.Hurt:
                adudiSourceSFX.PlayOneShot(hurtSFX);
                break;
            case ESourceSFX.Slash:
                adudiSourceSFX.PlayOneShot(slashSFX[Random.Range(0, slashSFX.Length)]);
                break;
            case ESourceSFX.Laser:
                adudiSourceSFX.PlayOneShot(laserSFX[Random.Range(0, laserSFX.Length)]);
                break;
            case ESourceSFX.Fire:
                adudiSourceSFX.PlayOneShot(fireSFX[Random.Range(0, fireSFX.Length)]);
                break;
            default: return;
        }
    }

    public void PlayBGM(ESourceBGM _src)
    {
        adudiSourceBGM.Stop();
        switch (_src)
        {
            case ESourceBGM.Level:
                adudiSourceBGM.clip = levelBGM;
                break;
            case ESourceBGM.Menu:
                adudiSourceBGM.clip = menuBGM;
                break;
            default: return;
        }
        adudiSourceBGM.Play();

    }

    public void SetVolumeBGM(float _value)
    {
        adudiSourceBGM.outputAudioMixerGroup.audioMixer.SetFloat("BGM_Volume", Mathf.Log10(_value) * 20);
    }

    public void SetVolumeSFX(float _value)
    {
        adudiSourceSFX.outputAudioMixerGroup.audioMixer.SetFloat("SFX_Volume", Mathf.Log10(_value) * 20);
    }
}