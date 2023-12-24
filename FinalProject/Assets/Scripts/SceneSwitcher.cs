using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneSwitcher : MonoBehaviour
{
    AudioSource audioSource;//background
    GameObject musicControl;
    AudioSource buttonClickSound;

    // Start is called before the first frame update

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void start()
    {
        
        SceneManager.LoadScene("SampleScene");
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
                //Debug.Log("Find functionTransform");
                //functionTransform.gameObject.transform.position = new Vector3(400f, 400f, 0f);
                functionTransform.gameObject.SetActive(true);

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
        // 使用 GameObject.Find 尋找 BackGround_Help
        GameObject functionObject = GameObject.Find("function_table");

        if (functionObject != null)
        {
            // 找到了 BackGround_Help，將其設為非啟用狀態
            functionObject.SetActive(false);
        }
        else
        {
            //Debug.LogError("Not Find function_table GameObject");
        }
    }
    public void controlVolume()
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
        UpdateMusicVolume(value);
    }

    // Callback method for when the value of the Effects_Slider changes
    private void OnEffectsSliderValueChanged(float value)
    {
        // Output a debug message
        //Debug.Log("Effects_Slider Value Changed: " + value);

        // Update the volume based on the slider value
        UpdateEffectsVolume(value);
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

            // Set the background music volume based on the mapped volume value
            musiccontrol.GetComponent<AudioSource>().volume = mappedVolume;
        }
        else
        {
            //Debug.LogError("MusicControl object does not exist!");
        }
    }

    // Method to update the effects volume

    private void UpdateEffectsVolume(float volume)
    {       

        if (musicControl != null)
        {
            
                // Map the slider value to your expected volume range
                float mappedVolume = Mathf.Clamp01(volume);

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
                }
            }
            else
            {
                //Debug.LogError("Effects_Slider not found under Music_Control");
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
            buttonClickSound.Play();
            Invoke("returnmenu", buttonClickSound.clip.length);
        }
        else
        {
            //Debug.LogError("AudioSource not assigned!");
        }
    }
    





}
