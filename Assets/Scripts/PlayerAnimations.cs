using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    PlayerController player;

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isGrounded == false)
        {
            anim.SetTrigger("Jumping");
            anim.SetBool("isRunning", false);
        }

        if (player.isGrounded == true)
        {
            anim.SetBool("isRunning", true);
        }
    }
}
