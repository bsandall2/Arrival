using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fullscreenScript : MonoBehaviour
{
    public Image On;
    public Image Off;
    public Image img;
    int index;
    private void Update()
    {
        if (index == 1)
        {
            img.gameObject.SetActive(false);
        }

        if (index == 0)
        {
            img.gameObject.SetActive(true);
        }
    }

    public void ON()
    {
        Debug.Log("ON");
        index = 1;
        Off.gameObject.SetActive(true);
        On.gameObject.SetActive(false);
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OFF ()
    {
        Debug.Log("OFF");
        index = 0;
        On.gameObject.SetActive(true);
        Off.gameObject.SetActive(false);
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }
}
