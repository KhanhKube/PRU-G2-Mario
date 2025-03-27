using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManagerr : MonoBehaviour
{
    public static AudioManagerr instance; // Singleton

    public Sound[] musicSounds;  // Danh sách nhạc nền
    public Sound[] sfxSounds;    // Danh sách hiệu ứng âm thanh

    public AudioSource musicSource; // AudioSource cho nhạc nền
    public AudioSource sfxSource;   // AudioSource cho SFX

    private Dictionary<string, Sound> musicDictionary;
    private Dictionary<string, Sound> sfxDictionary;

    void Awake()
    {
        // Đảm bảo AudioManager là Singleton
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // Không bị xóa khi load scene mới
        //DontDestroyOnLoad(gameObject);
        // Tạo dictionary để dễ quản lý
        musicDictionary = new Dictionary<string, Sound>();
        foreach (Sound s in musicSounds)
            musicDictionary[s.name] = s;

        sfxDictionary = new Dictionary<string, Sound>();
        foreach (Sound s in sfxSounds)
            sfxDictionary[s.name] = s;
    }
    void Start()
    {
        Debug.Log("AudioManager đã khởi động"); // Kiểm tra xem AudioManager có chạy không

        PlayMusic("background"); // Đổi tên nếu cần
    }

    // Phát nhạc nền
    public void PlayMusic(string name)
    {
        if (musicDictionary.ContainsKey(name))
        {
            Sound s = musicDictionary[name];
            Debug.Log("Phát nhạc: " + name);
            musicSource.clip = s.clip;
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music not found: " + name);
        }
    }

    // Phát hiệu ứng âm thanh
    public void PlaySFX(string name)
    {
        if (sfxDictionary.ContainsKey(name))
        {
            Sound s = sfxDictionary[name];
            sfxSource.PlayOneShot(s.clip);
        }
        else
        {
            Debug.LogWarning("SFX not found: " + name);
        }
    }

    public void PlayButtonClickSound()
    {
        PlaySFX("click"); // "click" là tên hiệu ứng âm thanh
    }



}
