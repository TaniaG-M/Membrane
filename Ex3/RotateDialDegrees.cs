using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDialDegrees : MonoBehaviour
{
    private int currentStep = 0;
    private int maxStep = 3;
    float step = 30;
    public int direction = -1;

    public GameObject rotatedObject;
    public GameObject ex3controller;
    public GameObject otherArrow;
    private RotateDialDegrees otherArrowScript;
    private Exercise3Controllers ex3Script;
    bool rotating = false;
    public float smoothTime = 2.0f; //rotate over 5 seconds
    
    private void Awake() {
        otherArrowScript = otherArrow.GetComponent<RotateDialDegrees>();
        ex3Script = ex3controller.GetComponent<Exercise3Controllers>();
    }
    public void startTurning() {
        if (!rotating && currentStep < maxStep && canTurn())
        {
            rotating = true;
            currentStep++;
            otherArrowScript.reduceStep();
            if(direction == -1) ex3Script.updateGlobalStep(currentStep);
            StartCoroutine(RotateOverTime(rotatedObject.transform.localEulerAngles.z, rotatedObject.transform.localEulerAngles.z + (step * direction), smoothTime));
        }
    }

    public void reduceStep() {
        if(currentStep > 0) {
            currentStep--;
        } else {
            currentStep = 0;
        }
        if(direction == -1) ex3Script.updateGlobalStep(currentStep);
    }

    private bool canTurn() {
        if(direction == 1) {
            return ex3Script.getGlobalStep() > 0;
        } else {
            return true;
        }
    }
    IEnumerator RotateOverTime(float currentRotation, float desiredRotation, float overTime)
    {
        float i = 0.0f;
        while (i <= 1)
        {
        rotatedObject.transform.localEulerAngles = new Vector3(rotatedObject.transform.localEulerAngles.x, rotatedObject.transform.localEulerAngles.y, Mathf.Lerp(currentRotation, desiredRotation, i));
        i += Time.deltaTime / overTime;
        yield return null;
        }
        yield return new WaitForSeconds(overTime);
        rotating = false; // no longer rotating
    }
}