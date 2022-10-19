using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp1Controller : MonoBehaviour
{
    public GameObject knob;
    public GameObject electrode;
    public GameObject line;
    Animator electrodeAnimator = null;
    Animator knobAnimator = null;
    Animator lineAnimator = null;

    private int currentFrame = 0;
    // Start is called before the first frame update
    void Awake()
    {
        knobAnimator = knob.GetComponent<Animator>();
        electrodeAnimator = electrode.GetComponent<Animator>();
        lineAnimator = line.GetComponent<Animator>();
    }
    void Start()
    {

    }

    public void advanceOneFrame(int toFrame) {
        currentFrame = currentFrame + toFrame;
        if(currentFrame < 0) currentFrame = 0;
        if(currentFrame > 60) currentFrame = 60;
        Debug.Log(currentFrame);
        if(toFrame == 1) {
            knobAnimator.Play("rotate_to_full", 0 , 0);
            electrodeAnimator.Play("electrode_movement", 0 , 0);
            StartCoroutine(moveLine(1.3f, toFrame));
        } else if(toFrame == -1) {
            electrodeAnimator.Play("electrode_reverse", 0 , 0);
            knobAnimator.Play("rotate_to_empty", 0 , 0);
            StartCoroutine(moveLine(0.3f, toFrame));
        }
    }


    IEnumerator moveLine(float time, int direction) {
        yield return new WaitForSeconds(time);
        if(direction == 1) {
            lineAnimator.Play("line_go_to_minus_75", 0 , 0);
        } else {
            lineAnimator.Play("line_from_minus_75_to_0", 0 , 0);
        }
    }
}
