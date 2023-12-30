using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagement : MonoBehaviour
{
    //private static MusicManager instance;
    private AudioSource audioSource;
    private static MusicManagement _instance;
    // 新增靜態變數
    public static float musicVolume { get; private set; } = 1.0f;
    public static float effectsVolume { get; private set; } = 1.0f;

    private void Awake()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
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
        Debug.Log("MusicManagement: musicVolume=" + musicVolume);
    }

    public void SetEffectsVolume()
    {
        SceneSwitcher sceneSwitcher = FindObjectOfType<SceneSwitcher>();
        effectsVolume = sceneSwitcher.EffectsValueChanged();
        Debug.Log("MusicManagement: effectsVolume=" + effectsVolume);
    }
    public float GetMusicVolume() {
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
        Debug.Log("OnDestroy called");
        SaveVolumes();
    }
    private void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("OnSceneUnload");
        Debug.Log(musicVolume);
        Debug.Log(effectsVolume);
        SaveVolumes();
    }

    // 這個方法用於保存音量值
    private void SaveVolumes()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
        Debug.Log("Save " + musicVolume);
        PlayerPrefs.Save();  // 保存 PlayerPrefs
    }
}
