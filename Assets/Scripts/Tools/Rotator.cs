//rotation script, call rotateObject() it in a different script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotator : MonoBehaviour
{
    // Start is called before the first frame update
    public int rotationDirection = -1; // -1 for clockwise
    //  1 for anti-clockwise

    public int rotationStep = 10; // should be less than 90
    

    private Vector3 currentRotation, targetRotation;

    public void rotateObject()
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
