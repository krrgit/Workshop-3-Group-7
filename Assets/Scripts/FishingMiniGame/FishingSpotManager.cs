using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSpotManager : MonoBehaviour {
    [SerializeField] private int maxFishingSpots = 4;
    [SerializeField] private int successes = 0;
    [SerializeField] private CircleCollider2D spot;
    [SerializeField] private BoxCollider2D[] areas;

    private int currArea;

    public void SpotFished(bool fishingSuccess)
    { 
        successes += fishingSuccess ? 1 : 0;
        RandomizeSpot();
    }

    void Start()
    {
        SpotFished(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpotFished(true);
        }
    }

    void RandomizeSpot()
    {
        if (successes >= maxFishingSpots) return;
        
        // Select random area
        int area = Random.Range(0, areas.Length);
        while (area == currArea)
        {
            area = Random.Range(0, areas.Length);
        }
        // select random position within area
        
        // Set new Position
        Vector3 newPos = GetRandomPositionInArea(areas[area]);

        spot.transform.position = newPos;
        currArea = area;
    }
    
    

    Vector3 GetRandomPositionInArea(BoxCollider2D area)
    {
        bool isX = (area.size.x > area.size.y);
        float range = isX ? area.size.x : area.size.y;
        range -= spot.radius;
        float pos = Random.Range(0, range) - (range * 0.5f);

        Vector3 displacement = (isX ? Vector2.right : Vector2.up) * pos;

        return area.transform.position + displacement;
    }
    
    
    
    

}
