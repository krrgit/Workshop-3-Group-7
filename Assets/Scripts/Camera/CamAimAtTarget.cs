using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAimAtTarget : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float playerCamSize = 8;

    private Transform roomTarget;
    [SerializeField] private float roomCamSize = 12;
    [SerializeField] private float speed = 8;

    private bool aimAtRoom;

    private Vector3 zDist = new Vector3(0,0,-10);

    public static CamAimAtTarget Instance;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ToggleRoomCam(bool state, Transform _roomTarget)
    {
        aimAtRoom = state;
        roomTarget = _roomTarget;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (aimAtRoom)
        {
            TargetRoom();
            return;
        }
        else
        {
            TargetPlayer();
        }
    }

    void TargetRoom()
    {
        transform.position = Vector3.Lerp(transform.position, roomTarget.position+zDist, Time.deltaTime * speed);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, roomCamSize, Time.deltaTime * speed * 0.4f);
    }

    void TargetPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, target.position+zDist, Time.deltaTime * speed);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, playerCamSize, Time.deltaTime * speed);
    }
}
