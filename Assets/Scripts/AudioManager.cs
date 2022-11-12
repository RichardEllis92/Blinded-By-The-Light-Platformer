using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] sfx;
    public AudioSource music;

    [SerializeField]
    private DialogueUI dialogueUI;

    [SerializeField]
    float lowerMusicVolume = 0.1f;

    [SerializeField]
    float raiseMusicVolume = 0.5f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayMusic();
    }
    private void Update()
    {
        LowerMusicVolume();
        RaiseMusicVolume();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    public void StopSFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
    }

    void LowerMusicVolume()
    {
        if (dialogueUI.isOpen == true)
        {
            music.volume = lowerMusicVolume;
        }
    }
    void RaiseMusicVolume()
    {
        if (dialogueUI.isOpen == false)
        {
            music.volume = raiseMusicVolume;
        }
    }
    public void PlayMusic()
    {
        if (music.isPlaying) return;
        music.Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }
}
