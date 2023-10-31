using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;

    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnMenu;
    [SerializeField] private Button btnQuit;

    private void Awake()
    {
        btnMenu.onClick.AddListener(Menu);
        btnQuit.onClick.AddListener(Quit);
        btnResume.onClick.AddListener(Resume);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    void Resume()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        player.GetComponent<PlayerMy>().enabled = true;
        camera.GetComponent<CameraRotate>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        player.GetComponent<PlayerMy>().enabled = false;
        camera.GetComponent<CameraRotate>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void Quit()
    {
        Application.Quit();
    }

    void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
