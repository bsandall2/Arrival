using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    PlayerController player;
    public GameObject pauseMenu;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
            if (isPaused)
            {
                ResumeGame();
            }

            else
            {
                PauseGame();
            }
       }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        FindObjectOfType<AudioManager>().Pause("GameTheme");
        FindObjectOfType<AudioManager>().Play("PauseDeathMenu");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        FindObjectOfType<AudioManager>().UnPause("GameTheme");
        FindObjectOfType<AudioManager>().Stop("PauseDeathMenu");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
