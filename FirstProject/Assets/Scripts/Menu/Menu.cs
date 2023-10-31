using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelSettings;

    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnQuit;
    [SerializeField] private Button btnBack;

    [SerializeField] private Toggle tglFullScreen; 
    [SerializeField] private Slider sdrVolume;

    [SerializeField] private TMP_Dropdown drpGraphics;

    [SerializeField] private TMP_Dropdown drpResolution;

    public AudioMixer audioMixer;

    private Resolution[] resolutions;

    private void Awake()
    {
        btnStart.onClick.AddListener(StartGame);
        btnSettings.onClick.AddListener(ShowSettings);
        btnQuit.onClick.AddListener(QuitGame);
        btnBack.onClick.AddListener(ShowMenu);
        
        tglFullScreen.onValueChanged.AddListener(SetFullScreen);
        sdrVolume.onValueChanged.AddListener(SetVolume);
        
        drpGraphics.onValueChanged.AddListener(SetQuality);
        drpResolution.onValueChanged.AddListener(SetResotulion);
    }

    private void Start()
    {
        resolutions = Screen.resolutions;
        drpResolution.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        string lastResolution = "";
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width}x{resolutions[i].height} + @{resolutions[i].refreshRateRatio} Hz";
            options.Add(option);
            // if (option != lastResolution)
            // {
            // }
            //lastResolution = option;
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        drpResolution.AddOptions(options);
        drpResolution.value = currentResolutionIndex;
        drpResolution.RefreshShownValue();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FirstLevel");
    }

    public void ShowSettings()
    {
        panelSettings.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ShowMenu()
    {
        panelSettings.SetActive(false);
        panelMenu.SetActive(true);
    }
    
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (Screen.fullScreen == true)
        {
            Debug.Log("full screen true");
        }
        if (Screen.fullScreen == false)
        {
            Debug.Log("full screen false");
        }
    }
    
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("volume", value);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResotulion(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}


