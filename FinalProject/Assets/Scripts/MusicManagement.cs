using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManagement : MonoBehaviour
{
    //private static MusicManager instance;
    private AudioSource audioSource;
    private static MusicManagement _instance;

    // 新增靜態變數
    public static float musicVolume { get; private set; } = 1.0f;
    public static float effectsVolume { get; private set; } = 1.0f;
    public AudioClip[] newAudioClip;
    private string currentSceneName;  // 保存目前的場景名稱
    private bool hasPlayedCurrentAudio;  // 是否已經播放了當前場景的音樂
    private AudioClip lastPlayedAudioClip;  // 上一次播放的音樂
    private void Awake()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
        // 檢查是否已經存在實例
        MusicManagement[] musicManagements = FindObjectsOfType<MusicManagement>();
        if (musicManagements.Length > 1)
        {
            // 已經存在其他實例，銷毀這個新實例
            Destroy(gameObject);
            return;
        }

        // 確保在場景切換時不被銷毀
        DontDestroyOnLoad(gameObject);

        // 防止在場景切換時重新播放音樂
        audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
        {
            audioSource.volume = musicVolume;
            audioSource.Play();
        }

        //SetEffectsVolume();
        //SetMusicVolume();
    }

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

        // 防止在場景切換時重新播放音樂
        if (!audioSource.isPlaying)
        {
            audioSource.volume = musicVolume;
            audioSource.Play();
        }
    }
    private void Update() {
        SetEffectsVolume();
        SetMusicVolume();
    }
    public void SetMusicVolume()
    {
        SceneSwitcher sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        musicVolume = sceneSwitcher.MusicValueChanged();
        //Debug.Log("Set MusicManagement: musicVolume=" + musicVolume);
    }

    public void SetEffectsVolume()
    {
        SceneSwitcher sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        effectsVolume = sceneSwitcher.EffectsValueChanged();
        //Debug.Log("Set MusicManagement: effectsVolume=" + effectsVolume);
    }
    public float GetMusicVolume() {
        //Debug.Log("Get musicVolume: " + musicVolume);
        return musicVolume;
    }
    public float GetEffectsVolume()
    {
        return effectsVolume;
    }

    // 這個方法在遊戲退出時被呼叫，用於保存音量值
    private void OnApplicationQuit()
    {
        SaveVolumes();
    }
    //在物件被銷毀（例如場景切換）時被調用，用於保存音量值
    private void OnDestroy()
    {
        //Debug.Log("OnDestroy called");
        SaveVolumes();
    }
    private void OnSceneUnloaded(Scene scene)
    {
        //Debug.Log("OnSceneUnload");
        //Debug.Log(musicVolume);
        //Debug.Log(effectsVolume);
        SaveVolumes();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        //musicVolume = transform.GetComponent<AudioSource>().volume;
        //Debug.Log("OnSceneUnload");
        //Debug.Log(musicVolume);
        //Debug.Log(effectsVolume);
        GameObject.FindObjectOfType<SceneSwitcher>().firstenter = true;
        // 獲取目前的場景名稱
        currentSceneName = scene.name;

        // 判斷是否已經播放了當前場景的音樂，如果沒有，就切換音樂
        if (!hasPlayedCurrentAudio)
        {
            SwitchAudioForScene(currentSceneName);
        }

        // 重置播放標誌
        hasPlayedCurrentAudio = false;

    }

    private void SwitchAudioForScene(string sceneName)
    {
        Debug.Log("Switching audio for scene: " + sceneName);
        // 根據場景名稱切換音樂
        switch (sceneName)
        {
            case "level1":
            case "level2":
            case "level3":
                audioSource.Stop();  // 停止目前的音樂
                audioSource.clip = newAudioClip[1];  // 播放第二首音樂
                audioSource.volume = musicVolume;
                audioSource.Play();  // 播放新的音樂
                hasPlayedCurrentAudio = true;
                break;
            default:
                // 如果上一次播放的音樂和目前的音樂相同，就還原音量，否則切換新音樂
                if (lastPlayedAudioClip == newAudioClip[0])
                {
                    audioSource.volume = musicVolume;
                    //audioSource.Play();
                }
                else
                {
                    audioSource.Stop();
                    audioSource.clip = newAudioClip[0];  // 播放第一首音樂
                    audioSource.volume = musicVolume;
                    audioSource.Play();
                }
                hasPlayedCurrentAudio = true;
                break;
        }
    }

    // 這個方法用於保存音量值
    private void SaveVolumes()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        //PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
        //Debug.Log("Save " + musicVolume);
        //Debug.Log("Save " + effectsVolume);
        PlayerPrefs.Save();  // 保存 PlayerPrefs
        lastPlayedAudioClip = audioSource.clip;
    }
}
