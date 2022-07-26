using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    public float depth = 1;

    PlayerController player;
    RandomBuildingSprite buildingSprite;

    public void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        buildingSprite = GetComponent<RandomBuildingSprite>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float realVelocity = player.velocity.x / depth;
        Vector2 pos = transform.position;

        pos.x -= realVelocity * Time.fixedDeltaTime;

        if (pos.x <= -40)
        {
            pos.x = 80;
            buildingSprite.ChangeBuilding();
        }

        transform.position = pos;
    }
}
