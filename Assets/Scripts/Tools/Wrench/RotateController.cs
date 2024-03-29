using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    [SerializeField] private Transform[] objectsToRotate;
    // Start is called before the first frame update
    public int rotationDirection = -1; // -1 for clockwise
    //  1 for anti-clockwise

    public int rotationStep = 90;
    private Vector3 currentRotation, targetRotation;

    private bool isRotating;
    
    public float RotateObjects()
    {
        if (isRotating) return 0;
        isRotating = true;
        print("Rotate");
        for (int i = 0; i < objectsToRotate.Length; ++i)
        {
            StartCoroutine(objectRotationAnimation(objectsToRotate[i]));
        }

        return 90f/rotationStep;
    }

    IEnumerator objectRotationAnimation(Transform obj)
    {
        Vector3 currRot = obj.eulerAngles;
        Vector3 targetRot = obj.eulerAngles + new Vector3(0, 0, 90 * rotationDirection);

        float dot = Vector3.Dot(obj.up, targetRot);

        while (((int)currRot.z >
            (int)targetRot.z && rotationDirection < 0) || // for clockwise
        ((int)currRot.z < (int)targetRot.z && rotationDirection > 0)) // for anti-clockwise
        {
            currRot.z += rotationDirection * rotationStep * Time.deltaTime;
            obj.eulerAngles = currRot;
            yield return new WaitForEndOfFrame();
        }

        obj.eulerAngles = targetRot;
        isRotating = false;
    }
    
}