using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerController player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        Animator ani = GetComponent<Animator>();
        ani.Play("DroneHover", 0, Random.Range(0.0f, 1.0f));
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= player.velocity.x * Time.fixedDeltaTime;
        if (pos.x < -100)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }
}
