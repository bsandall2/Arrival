using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OptionMenu : MonoBehaviour
{
    PlayerController player;
    public GameObject PauseRef;
    public GameObject optionMenu;
    public static bool isPaused;

    void Start()
    {
        optionMenu.SetActive(false);
        isPaused = false;

    }

    public void GoToOption()
    {
        optionMenu.gameObject.SetActive(true);
        PauseRef.gameObject.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void backButton()
    {
        optionMenu.gameObject.SetActive(false);
        PauseRef.gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}