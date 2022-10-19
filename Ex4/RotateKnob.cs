using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateKnob : MonoBehaviour
{
    private int currentStep = 0;
    public int maxStep = 4;
    float step = 30;
    public int direction = -1;
    public string type;

    public GameObject rotatedObject;
    public GameObject ex4controller;
    public GameObject otherArrow;
    private RotateKnob otherArrowScript;
    private Experiment4Controller ex4Script;
    bool rotating = false;
    public float smoothTime = 2.0f; //rotate over 5 seconds
    
    private void Awake() {
        otherArrowScript = otherArrow.GetComponent<RotateKnob>();
        ex4Script = ex4controller.GetComponent<Experiment4Controller>();
    }
    public void startTurning() {
        if (!rotating && currentStep < maxStep && canTurn())
        {
            rotating = true;
            currentStep++;
            otherArrowScript.reduceStep();
            if(direction == -1) ex4Script.updateGlobal(type, currentStep);
            StartCoroutine(RotateOverTime(rotatedObject.transform.localEulerAngles.z, rotatedObject.transform.localEulerAngles.z + (step * direction), smoothTime));
        }
    }

    public void reduceStep() {
        if(currentStep > 0) {
            currentStep--;
        } else {
            currentStep = 0;
        }
        if(direction == -1) ex4Script.updateGlobal(type, currentStep);
    }

    private bool canTurn() {
        if(type == "amplitude") {
            if(direction == 1) {
                return ex4Script.getGlobalAmplitude() > 0;
            } else {
                return true;
            }
        }
        else {
            if(direction == 1) {
                return ex4Script.getGlobalTime() > 0;
            } else {
                return true;
            }
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
