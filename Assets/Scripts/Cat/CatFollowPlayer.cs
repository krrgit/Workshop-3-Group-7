using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatFollowPlayer : MonoBehaviour {
    [SerializeField]private float pointSpacing = 0.5f;
    [SerializeField] private int pointLimit = 5;
    [SerializeField] private Transform player;
    
    private List<Vector2> points = new List<Vector2>();
    private List<Vector2> forward = new List<Vector2>();

    private float playerDist;
    private float pointDist;

    private Vector2 tempPoint;

    private bool pointsExist;

    public Vector2 FollowPoint
    {
        get { return points[0]; }
    }

    public bool CloseToPlayer
    {
        get { return Vector2.Distance(transform.position,player.position) <= 2; }
    }


    void Start()
    {
        //CreateStartPoints();
    }

    public void CreateStartPoints()
    {
        if (pointsExist) return;
        for(int i=0;i<pointLimit;++i)
        {
            points.Add(transform.position);
        }

        pointsExist = true;
    }
    void Update()
    {
        //DrawPoints();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdatePoints();
    }

    void DrawPoints()
    {
        for(int i=0;i<pointLimit-1;++i)
        {
            Debug.DrawRay(points[i],points[i+1] - points[i], Color.red);
        }
        //print("p1: " + points[0] + "p2: " + points[points.Count-1]);
    }
    
    // Update the points when the player moves a certain distance away from the last point
    void UpdatePoints()
    {
        // 0 = newest, last = oldest
        playerDist = Vector2.Distance(points[points.Count-1], player.position);
        if (playerDist > pointSpacing)
        {
            points.Add(player.position);
            points.RemoveAt(0);
        }
    }
}
