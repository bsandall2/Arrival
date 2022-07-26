using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBuildingSprite : MonoBehaviour
{
    private int rand;

    public Sprite[] BuildingSprite;

    // Start is called before the first frame update
    void Start()
    {
        ChangeBuilding();
    }

    public void ChangeBuilding()
    {
        rand = Random.Range(0, BuildingSprite.Length);
        GetComponent<SpriteRenderer>().sprite = BuildingSprite[rand];
    }
}
