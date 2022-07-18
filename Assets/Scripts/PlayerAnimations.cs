using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    PlayerController player;
    public float animSpeed;

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
        }

        if (player.isGrounded == true)
        {
            anim.SetBool("isRunning", true);
        }

        animSpeed *= player.velocity.x;
        anim.speed = animSpeed;
    }
}
