using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backGroundAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [SerializeField] private AudioClip backGroundClip;
    [SerializeField] private AudioClip backGroundClip2;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip gamewin;
    [SerializeField] private AudioClip gameloss;
    [SerializeField] private AudioClip buttonClickClip; // 🔴 Âm thanh khi click nút

    // Start is called before the first frame update

    void Awake()
    {
        // Đảm bảo chỉ có một AudioManager duy nhất
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject); // Giữ lại khi load scene mới
        backGroundAudioSource = gameObject.AddComponent<AudioSource>();
        effectAudioSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        PlayBackGroundMusic();
    }
    public void PlayBackGroundMusic()
    {
        backGroundAudioSource.clip = backGroundClip;
        backGroundAudioSource.Play();
    }
    public void PlayBackGroundMusic2()
    {
        backGroundAudioSource.clip = backGroundClip2;
        backGroundAudioSource.Play();
    }
    public void StopBackGroundMusic()
    {
        backGroundAudioSource.Stop();
    }
    public void StopBackGroundMusic2()
    {
        backGroundAudioSource.Stop();
    }

    public void PlayCoinSound()
    {
        effectAudioSource.PlayOneShot(coinClip);
    }

    public void PlayJumpSound()
    {
        effectAudioSource.PlayOneShot(jumpClip);
    }
    public void gameWinSound()
    {
        StopBackGroundMusic();
        effectAudioSource.PlayOneShot(gamewin);
    }
    public void gameOverSound()
    {
        StopBackGroundMusic();
        effectAudioSource.PlayOneShot(gameloss);
    }
    public void PlayButtonClickSound()
    {
        Debug.Log("Nút đã được ấn!");
        if (buttonClickClip != null)
        {
            effectAudioSource.volume = 1f;
            effectAudioSource.PlayOneShot(buttonClickClip);
        }
        else
        {
            Debug.LogError("Âm thanh nút click chưa được gán!");
        }
    }


}
