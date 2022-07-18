using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100;
    public float coef = 0.6f;
    public float addOverTime = 0.2f;

    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        currentHealth = 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.isDead == false)
        {
            currentHealth -= coef * Time.deltaTime;
            coef += addOverTime * Time.deltaTime;
        }
        
        if (currentHealth <= 0)
        {
            player.isDead = true;
            player.velocity.x = 0;
        }

        //Debug.Log(currentHealth);
    }

    public void PlayerHit()
    {
        currentHealth -= 10;
    }
}
