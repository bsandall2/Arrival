using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity;
    public Vector2 velocity;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float distance = 0;
    public float jumpVelocity = 20;
    public float maxXVelocity = 100;
    public float groundHeight = 10;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float maxMaxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;
    public bool isJumping = false;

    public float jumpGroundThreshold = 1;

    public bool isDead = false;

    public LayerMask groundLayerMask;
    public LayerMask obstacleLayerMask;
    public LayerMask healthLayerMask;

    public PlayerHealthScript playerHealth;

    public GameObject deathBox;

    private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        float currentPHealth = playerHealth.currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
       Vector2 pos = transform.position;
       float groundDistance = Mathf.Abs(pos.y - groundHeight);

       if (isGrounded || groundDistance <= jumpGroundThreshold)
       {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
                FindObjectOfType<AudioManager>().Play("PlayerJump");
            }          
       }

       if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
       {
            isHoldingJump = false;                      
       }

       if (isGrounded == false)
       {
           isJumping = true;
       }

        if (isGrounded == true)
       {
           isJumping = false;
       }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (isDead)
        {
            return;
        }

        if (pos.y < -20)
        {
            isDead = true;
        }

        if (!isGrounded)
        {            
            if (isHoldingJump)
            {               
                holdJumpTimer += Time.fixedDeltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }               
            }

            pos.y += velocity.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {                
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundLayerMask);
            if (hit2D.collider != null)
            {
                GroundScript ground = hit2D.collider.GetComponent<GroundScript>();
                if (ground != null)
                {
                    if(pos.y >= ground.groundHeight)
                    {
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        velocity.y = 0;
                        isGrounded = true;
                    } 
                }
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

            Vector2 wallOrigin = new Vector2(pos.x, pos.y);
            RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime, groundLayerMask);
            if (wallHit.collider != null)
            {
                GroundScript ground = wallHit.collider.GetComponent<GroundScript>();
                if (ground != null)
                {
                    if (pos.y < ground.groundHeight)
                    {
                        velocity.x = 0;
                    }
                }
            }
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);
            maxHoldJumpTime = maxMaxHoldJumpTime * velocityRatio;

            velocity.x += acceleration * Time.fixedDeltaTime;
            
            if (velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider == null)
            {
                isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);       
        }

        Vector2 obstOrigin = new Vector2(pos.x, pos.y);
        RaycastHit2D obsHitX = Physics2D.Raycast(obstOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime, obstacleLayerMask);
        if (obsHitX.collider != null)
        {
            Obstacle obstacle = obsHitX.collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                hitObstacle(obstacle);
            }
        }

        RaycastHit2D obsHitY = Physics2D.Raycast(obstOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime, obstacleLayerMask);
        if (obsHitY.collider != null)
        {
            Obstacle obstacle = obsHitY.collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                hitObstacle(obstacle);
            }
        }

        Vector2 healthOrigin = new Vector2(pos.x, pos.y);
        RaycastHit2D healthHitX = Physics2D.Raycast(healthOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime, healthLayerMask);
        if (healthHitX.collider != null)
        {
            HealthCube health = healthHitX.collider.GetComponent<HealthCube>();
            if (health != null)
            {
                hitHealth(health);
            }
        }

        RaycastHit2D healthHitY = Physics2D.Raycast(healthOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime, healthLayerMask);
        if (healthHitY.collider != null)
        {
            HealthCube health = healthHitY.collider.GetComponent<HealthCube>();
            if (health != null)
            {
                hitHealth(health);
            }
        }

        transform.position = pos;
    }

    void hitObstacle(Obstacle obstacle)
    {
        Destroy(obstacle.gameObject);
        velocity.x *= 0.7f;
        playerHealth.GetComponent<PlayerHealthScript>().PlayerHit();
        FindObjectOfType<AudioManager>().Play("PlayerHit");
    }

    void hitHealth(HealthCube health)
    {
        Destroy(health.gameObject);
        playerHealth.currentHealth += 20;
        velocity.x *= 1.05f;
        FindObjectOfType<AudioManager>().Play("PlayerHeal");
    }
}
