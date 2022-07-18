using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCube : MonoBehaviour
{
    PlayerController player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
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
