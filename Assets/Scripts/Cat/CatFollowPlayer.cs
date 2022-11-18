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

    public Vector2 FollowPoint
    {
        get { return points[0]; }
    }

    public bool CloseToPlayer
    {
        get { return Vector2.Distance(transform.position,player.position) <= 2; }
    }


    // Start is called before the first frame update
    void Awake()
    {
        for(int i=0;i<pointLimit;++i)
        {
            points.Add(player.position + (Vector3.left * 2));
        }
    }

    void Update()
    {
        DrawPoints();
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
