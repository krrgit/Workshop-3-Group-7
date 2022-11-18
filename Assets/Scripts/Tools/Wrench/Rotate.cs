using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Transform[] objectsToRotate;
    // Start is called before the first frame update
    public int rotationDirection = -1; // -1 for clockwise
    //  1 for anti-clockwise

    public int rotationStep = 10; // should be less than 90


    private Vector3 currentRotation, targetRotation;


    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Z) && ToolManager.Instance.CurrentTool == ToolInUse.Wrench)
        // {
        //     rotateObject();
        // }
    }
    public void rotateObjects()
    {
        
        currentRotation = gameObject.transform.eulerAngles;
        targetRotation.z = (currentRotation.z + (90 * rotationDirection));
        StartCoroutine(objectRotationAnimation());
    }

    IEnumerator objectRotationAnimation()
    {
        // add rotation step to current rotation.
        currentRotation.z += (rotationStep * rotationDirection);
        gameObject.transform.eulerAngles = currentRotation;

        yield return new WaitForSeconds(0);

        if (((int)currentRotation.z >
                (int)targetRotation.z && rotationDirection < 0) || // for clockwise
            ((int)currentRotation.z < (int)targetRotation.z && rotationDirection > 0)) // for anti-clockwise
        {
            StartCoroutine(objectRotationAnimation());
        }
    }
    
}