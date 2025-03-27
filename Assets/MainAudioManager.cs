using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip backGroundClip;

    [SerializeField] private AudioClip buttonClickClip;
    public AudioSource musicSounds;
    public AudioSource sfxSounds; // AudioSource riêng để phát âm thanh hiệu ứng (SFX)


    void Start()
    {
        PlayBackGroundMusic();
    }


    public void PlayBackGroundMusic()
    {
        if (musicSounds != null && backGroundClip != null)
        {
            musicSounds.clip = backGroundClip;
            musicSounds.loop = true; // Lặp lại nhạc
            musicSounds.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource hoặc AudioClip chưa được gán trong Inspector!");
        }
    }

    public void PlayButtonClickSound()
    {
        if (sfxSounds != null && buttonClickClip != null)
        {
            sfxSounds.PlayOneShot(buttonClickClip); // Phát hiệu ứng âm thanh 1 lần
        }
        else
        {
            Debug.LogWarning("AudioSource hoặc buttonClickClip chưa được gán trong Inspector!");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Đã bấm phím SPACE, thử phát âm thanh...");
            PlayButtonClickSound();
        }
    }
}
