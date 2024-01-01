using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
//using UnityEngine.UIElements;


public class SceneSwitcher : MonoBehaviour
{
    AudioSource audioSource;//background
    GameObject musicControl;
    AudioSource buttonClickSound;
    bool lockcamera = false;
    public event Action OnCameraLockStateChanged;
    float musicsound=1.0f, effectssound=1.0f;
    public bool firstenter=true;
    // Start is called before the first frame update

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        musicsound= PlayerPrefs.GetFloat("MusicVolume");
        effectssound = PlayerPrefs.GetFloat("EffectsVolume");
        //Debug.Log("start"+effectssound);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            Cursor.visible = true;
            Cursor.lockState= CursorLockMode.None;            
        }
        /*if (GameObject.Find("Music_Control") != null&&firstenter) {
            SetSliderValue();
        }*/
    }
    public void start()
    {
        //firstenter = true;
        SceneManager.LoadScene("ChooseLevelScene");
    }
    public void exit()
    {
        Application.Quit();
    }
    public void help()
    {
        buttonClickSound = GetComponent<AudioSource>();
        // 檢查 AudioSource 是否存在並播放音效
        if (buttonClickSound != null)
        {
           buttonClickSound.volume = effectssound;
           buttonClickSound.Play();
        }
        else
        {
            //Debug.LogError("AudioSource not assigned!");
        }
        Transform helpButtonTransform = GetComponentInChildren<Transform>().Find("BackGround_Help");

        if (helpButtonTransform != null)
        {
           
            if (helpButtonTransform.gameObject != null)
            {
                //Debug.Log("Find helpButtonTransform");
                //helpButtonTransform.gameObject.transform.position = new Vector3(400f, 400f, 0f);
                helpButtonTransform.gameObject.SetActive(true);

            }
            else
            {
                //Debug.LogError("helpButtonTransform is null");
            }
        }
        else
        {
            //Debug.LogError("Not Find helpButtonTransform Transform");
        }
    }

    public void closeHelp()
    {
        // 使用 GameObject.Find 尋找 BackGround_Help
        GameObject helpButtonObject = GameObject.Find("BackGround_Help");

        if (helpButtonObject != null)
        {
            // 找到了 BackGround_Help，將其設為非啟用狀態
            helpButtonObject.SetActive(false);
        }
        else
        {
            //Debug.LogError("Not Find BackGround_Help GameObject");
        }
    }

    public void returnmenu()
    {
        
        SceneManager.LoadScene("StartScene");
    }

    public void function()
    {
        Transform functionTransform = GetComponentInChildren<Transform>().Find("function_table");

        if (functionTransform != null)
        {

            if (functionTransform.gameObject != null)
            {
                if (transform.tag == "LevelFunction") {

                    Time.timeScale = 0f;
                    lockcamera = true;//LockCamera
                }
                ThirdPersonController thirdPersonController = FindObjectOfType<ThirdPersonController>();
                if (thirdPersonController != null)
                {
                    thirdPersonController.pause = lockcamera;//直接更新是否暫停   
                    //Debug.Log("function pause: " + thirdPersonController.pause);
                }
                functionTransform.gameObject.SetActive(true);
                OnCameraLockStateChanged?.Invoke();//告知LockCameraPosition已改變
            }
            else
            {
                //Debug.LogError("functionTransform is null");
            }
        }
        else
        {
            //Debug.LogError("Not Find functionTransform Transform");
        }
    }

    public void closeFunction()
    {
        // 使用 GameObject.Find 尋找function_table
        GameObject functionObject = GameObject.Find("function_table");
        
        if (functionObject != null)
        {
            
            Time.timeScale = 1f;
            lockcamera = false;//UnLock Camera
            ThirdPersonController thirdPersonController=FindObjectOfType<ThirdPersonController>();
            if (thirdPersonController != null) { 
                thirdPersonController.pause = lockcamera;//直接更新是否暫停   
            }                     
            if (transform.parent.parent.tag == "LevelFunction")
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            // 找到了 function_table，將其設為非啟用狀態
            functionObject.SetActive(false);
            OnCameraLockStateChanged?.Invoke();//告知LockCameraPosition已改變
        }
        else
        {
            //Debug.LogError("Not Find function_table GameObject");
        }

    }
    

    public bool GetCamera() {
        //Debug.Log("lock"+lockcamera);
        return lockcamera;//回傳改變的LockCameraPosition之值
    }
    public void chooselevel() {
        string originalString = transform.name.Substring(0,6);
        string level = originalString.ToLower();
        //Debug.Log(level);
        SceneManager.LoadScene(level);
    }

    public void controlVolume()//音量一直維持一樣在所有場景，開始和選擇關卡音樂一樣，開始遊戲另一個
    {
        // Find MusicControl
        Transform musicControlTransform = GetComponentInChildren<Transform>().Find("Music_Control");

        if (musicControlTransform != null)
        {
            musicControl = musicControlTransform.gameObject;

            if (musicControl != null)
            {
                //Debug.Log("Find MusicControl");
                //musicControl.transform.position = new Vector3(400f,400f,0f);
                //Debug.Log(transform.parent.name);
                Transform parentTransform = transform.parent;
                Transform helpButtonTransform = parentTransform.Find("Help_Button");
                if (helpButtonTransform != null)
                {
                    transform.SetAsLastSibling();
                    //Debug.Log("Help_Button find");
                }
                musicControl.SetActive(true);
                // Get the Music_Slider/Effects_Slider component
                Slider musicSlider=null;
                Slider effectsSlider=null;
                foreach (Slider slider in musicControlTransform.GetComponentsInChildren<Slider>())
                {
                    if (slider.name == "Effects_Slider")
                    {
                        effectsSlider = slider;

                    }
                    else if (slider.name == "Music_Slider") {
                        musicSlider = slider;
                    }
                }
                

                if (musicSlider != null)
                {
                    // Add a listener for the value change event of the slider                  
                    musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);
                }
                else
                {
                    //Debug.LogError("Music_Slider is null");
                }
                if (effectsSlider != null)
                {
                    // Add a listener for the value change event of the slider
                    effectsSlider.onValueChanged.AddListener(OnEffectsSliderValueChanged);
                }
                else
                {
                    //Debug.LogError("Effects_Slider is null");
                }
                if (firstenter) {
                    //Debug.Log("SetSliderValue enter");
                    SetSliderValue();
                    firstenter = false;
                }
                
            }
            else
            {
                //Debug.LogError("musicControl is null");
            }
        }
        else
        {
            //Debug.LogError("Not Find MusicControl Transform");
        }
    }


    // Callback method for when the value of the Music_Slider changes
    private void OnMusicSliderValueChanged(float value)
    {
        // Output a debug message
        //Debug.Log("Music_Slider Value Changed: " + value);

        // Update the volume based on the slider value
        musicsound = value;
        MusicValueChanged();//更改儲存的背景音量大小
        //Debug.Log("OnMusicSliderValueChanged "+FindObjectOfType<MusicManagement>().GetMusicVolume());
        UpdateMusicVolume(value);
    }

    public float MusicValueChanged()
    {
        return musicsound;
    }

    // Callback method for when the value of the Effects_Slider changes
    private void OnEffectsSliderValueChanged(float value)
    {
        // Output a debug message
        //Debug.Log("Effects_Slider Value Changed: " + value);

        // Update the volume based on the slider value
        effectssound = value;
        EffectsValueChanged();//更改儲存的按鍵音量大小
        PlayerPrefs.SetFloat("EffectsVolume", effectssound);
        Debug.Log("OnEffectsSliderValueChanged " + effectssound);
        UpdateEffectsVolume(value);
    }

    public float EffectsValueChanged()
    {
        return effectssound;
    }

    // Method to update the volume
    // Method to update the background music volume
    private void UpdateMusicVolume(float volume)
    {
        GameObject musiccontrol = GameObject.FindWithTag("MusicControl");
        if (musiccontrol != null)
        {
            // Map the slider value to your expected volume range
            float mappedVolume = Mathf.Clamp01(volume);
            //float mappedVolume = Mathf.Clamp01(FindObjectOfType<MusicManagement>().GetMusicVolume());
            GameObject music = GameObject.Find("Music");
            // Set the background music volume based on the mapped volume value
            music.GetComponent<AudioSource>().volume = mappedVolume;//更改music物件上音量大小
        }
        else
        {
            //Debug.LogError("MusicControl object does not exist!");
        }
    }

    // Method to update the effects volume

    private void UpdateEffectsVolume(float volume)
    {
        // Map the slider value to your expected volume range
        //float mappedVolume = Mathf.Clamp01(volume);
        float mappedVolume = Mathf.Clamp01(volume);
        if (musicControl != null)
        {           
            // Set the volume for each button's AudioSource
            Button[] buttons = transform.parent.GetComponentsInChildren<Button>()
                    .Where(button => button.name.Contains("Button"))
                    .ToArray();

                foreach (Button button in buttons)
                {
                    // Find the button's AudioSource
                    AudioSource buttonAudioSource = button.GetComponent<AudioSource>();

                    if (buttonAudioSource != null)
                    {
                        // Set the volume for the button's AudioSource
                        buttonAudioSource.volume = mappedVolume;
                    }
                    else
                    {
                        //Debug.LogError("Button's AudioSource is null");
                    }
                //Debug.Log(button.name);
            }
        }
        else
        {
                //Debug.LogError("Effects_Slider not found under Music_Control");
        }

        
    }

    public void SetSliderValue()//進入新場景更新slider
    {
        //Debug.Log("enter setslidervalue");
        // Find MusicControl
        GameObject musicControl = GameObject.Find("Music_Control");

        if (musicControl != null)
        {
            foreach (Slider slider in musicControl.transform.GetComponentsInChildren<Slider>())
            {
                if (slider.name == "Effects_Slider")
                {
                    slider.value = effectssound;
                    //Debug.Log("EffectsVolume: " + FindObjectOfType<MusicManagement>().GetEffectsVolume());
                    //Debug.Log("Effects: " + slider.value);
                }
                else if (slider.name == "Music_Slider")
                {
                    slider.value = GameObject.Find("Music").GetComponent<AudioSource>().volume;
                }
                //Debug.Log(slider.name + slider.value);
            }
        }
        else
        {
            //Debug.LogError("Music_Control not found.");
        }
    }



    public void closeChangeVolume()
    {
        // 使用 GameObject.Find 尋找 Music_Control
        GameObject musicControlObject = GameObject.Find("Music_Control");

        if (musicControlObject != null)
        {
            // 找到了 Music_Control，將其設為非啟用狀態
            musicControlObject.SetActive(false);
        }
        else
        {
            //Debug.LogError("Not Find Music_Control GameObject");
        }
        //Debug.Log(transform.name);
        Transform helpButtonTransform = transform.parent.parent.parent.Find("Help_Button");
        if (helpButtonTransform != null)
        {
            helpButtonTransform.SetAsLastSibling();
        }
    }
    public void OnPlayClick()
    {
        buttonClickSound = GetComponent<AudioSource>();
        // 檢查 AudioSource 是否存在並播放音效
        if (buttonClickSound != null)
        {
            buttonClickSound.volume = effectssound;
            buttonClickSound.Play();
            Invoke("start", buttonClickSound.clip.length);
        }
        else
        {
            //Debug.LogError("AudioSource not assigned!");
        }
    }
    public void OnExitClick()
    {
        buttonClickSound = GetComponent<AudioSource>();
        // 檢查 AudioSource 是否存在並播放音效
        if (buttonClickSound != null)
        {
            buttonClickSound.volume = effectssound;
            buttonClickSound.Play();
            Invoke("exit", buttonClickSound.clip.length);
        }
        else
        {
            //Debug.LogError("AudioSource not assigned!");
        }
    }
    public void OnReturnClick()
    {
        buttonClickSound = GetComponent<AudioSource>();
        // 檢查 AudioSource 是否存在並播放音效
        if (buttonClickSound != null)
        {
            buttonClickSound.volume = PlayerPrefs.GetFloat("EffectsVolume",effectssound);
            //Debug.Log(effectssound);
            buttonClickSound.Play();
            if (transform.parent.parent.tag == "LevelFunction")
            {
                Time.timeScale = 1f;
            }
            Invoke("returnmenu", buttonClickSound.clip.length);
        }
        else
        {
            //Debug.LogError("AudioSource not assigned!");
        }
    }
    public void OnChooseLevel() {
        buttonClickSound = GetComponent<AudioSource>();
        // 檢查 AudioSource 是否存在並播放音效
        if (buttonClickSound != null)
        {
            buttonClickSound.volume = effectssound;
            buttonClickSound.Play();
            Invoke("chooselevel", buttonClickSound.clip.length);
        }
        else
        {
            //Debug.LogError("AudioSource not assigned!");
        }

    }

}
