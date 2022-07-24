using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIcontrollerTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        SceneManager.LoadScene("LevelScene");        
    }

    public void option()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void quit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
