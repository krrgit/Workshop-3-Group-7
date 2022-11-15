using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatFollowPlayer : MonoBehaviour {
    [SerializeField]private float pointSpacing = 0.5f;
    [SerializeField] private int pointLimit = 5;
    [SerializeField] private Transform player;
    
    private List<Vector2> points = new List<Vector2>();

    private float playerDist;
    private float pointDist;

    public Vector2 FollowPoint
    {
        get { return points[0];  }
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
            points.Add(player.position);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdatePoints();
        ResetWhenClose();
    }

    void ResetWhenClose()
    {
        if (!CloseToPlayer) return;
        points.Clear();
        for(int i=0;i<pointLimit;++i)
        {
            points.Add(player.position);
        }
    }

    void UpdatePoints()
    {
        
        playerDist = Vector2.Distance(points[points.Count-1], player.position);
        if (playerDist > pointSpacing)
        {
            points.Add(player.position);
        }
        
        pointDist = Vector2.Distance(points[0], transform.position);
        if (points.Count > pointLimit || pointDist < pointSpacing)
        {
            points.RemoveAt(0);
        }
    }
}
