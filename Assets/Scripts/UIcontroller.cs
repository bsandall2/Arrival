using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIcontroller : MonoBehaviour
{
    PlayerController player;
    Text distanceText;

    GameObject results;
    Text finalDistanceText;

    public bool activePauseMusic = false;

    public void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();

        finalDistanceText = GameObject.Find("FinalDistanceText").GetComponent<Text>();
        results = GameObject.Find("Results");
        results.SetActive(false);    
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("GameTheme");
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";

        if (player.isDead)
        {
            results.SetActive(true);
            finalDistanceText.text = distance + " m";           
            FindObjectOfType<AudioManager>().Stop("GameTheme");

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Retry();
            }
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("LevelScene");        
    }
}
