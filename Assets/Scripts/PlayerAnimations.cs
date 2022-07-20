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
        anim = GameObject.Find("PlayerSprite").GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();        
    }

    void Update()
    {  
        if (player.isJumping == false)
        {
            anim.CrossFade("PlayerRunANIM", 0, 0);
        }    
        
        if (player.isJumping == true)
        {
            anim.CrossFade("PlayerJump", 0, 0);
        }     
    }

}
