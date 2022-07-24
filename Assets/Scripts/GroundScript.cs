using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    PlayerController player;

    public float groundHeight;
    public float groundRight;
    public float screenRight;
    BoxCollider2D collider;

    bool didGenerateGround = false;

    public Obstacle boxTemplate;
    public HealthCube healthCubeTemplate;

    public int amountObstacle = 2;
    public int amountHealth = 2;
    public float travelledDistance = 1000;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        collider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (collider.size.y / 2);
        screenRight = Camera.main.transform.position.x * 2.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        groundRight = transform.position.x + (collider.size.x / 2);

        if (groundRight < -20)
        {
            Destroy(gameObject);
            return;
        }

        if (!didGenerateGround)
        {
            if (groundRight < screenRight)
            {
                didGenerateGround = true;
                generateGround();               
            }
        }

        transform.position = pos; 
        
        if (player.distance >= travelledDistance )
        {
            amountObstacle += 1;
            amountHealth += 1;
            travelledDistance += 1000;
            return;
        }
    }

    void generateGround()
    {
        if (player.velocity.x > 0)
        {
            GameObject go = Instantiate(gameObject);
            BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
            Vector2 pos;

            float h1 = player.jumpVelocity * player.maxHoldJumpTime;
            float t = player.jumpVelocity / -player.gravity;
            float h2 = player.jumpVelocity * t + (0.6f * (player.gravity * (t * t)));
            float maxJumpHeight = h1 + h2;
            float maxY = player.transform.position.y + maxJumpHeight;
            maxY *= 0.7f;
            float minY = 5f;
            float actualY = Random.Range(minY, maxY);

            pos.y = actualY - goCollider.size.y / 2; ;
            if (pos.y > 2)
            {
                pos.y = 2;
            }

            float t1 = t + player.maxHoldJumpTime;
            float t2 = Mathf.Sqrt((2.0f * (maxY - actualY)) / -player.gravity);
            float totalTime = t1 + t2;
            float maxX = totalTime * player.velocity.x;
            maxX *= 0.7f;
            maxX += groundRight;
            float minX = screenRight + 5;
            float actualX = Random.Range(minX, maxX);

            pos.x = actualX + goCollider.size.x /2;
            go.transform.position = pos;

            GroundScript goGround = go.GetComponent<GroundScript>();
            goGround.groundHeight = go.transform.position.y + (goCollider.size.y / 2);

            if (player.distance >= 230)
            {
                int obstacleNum = Random.Range(0, amountObstacle);
                for (int i = 0; i < obstacleNum; i++)
                {
                    GameObject box = Instantiate(boxTemplate.gameObject);
                    float y = goGround.groundHeight;
                    float halfWidth = goCollider.size.x / 4 - 1;
                    float left = go.transform.position.x - halfWidth;
                    float right = go.transform.position.x + halfWidth;
                    float x = Random.Range(left, right);
                    Vector2 boxPos = new Vector2(x, y);
                    box.transform.position = boxPos;
                }

            }

            int healthNum = Random.Range(0, amountHealth);
            for (int i = 0; i <healthNum; i++)
            {
                GameObject healthBox = Instantiate(healthCubeTemplate.gameObject);
                float y = goGround.groundHeight; 
                float halfWidth = goCollider.size.x / 3 - 1;
                float left = go.transform.position.x - halfWidth;
                float right = go.transform.position.x + halfWidth;
                float x = Random.Range(left, right);
                Vector2 boxPos = new Vector2(x, y);
                healthBox.transform.position = boxPos;
            }
        }
        
    }
}
