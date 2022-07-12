using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 100;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(currentHealth);
    }

    public void PlayerHit()
    {
        currentHealth -= 5;
    }
}
