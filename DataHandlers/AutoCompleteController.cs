using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCompleteController : MonoBehaviour
{
    public float maxSeconds;
    public GameObject dataController;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FinishInSeconds(maxSeconds));
    }

    IEnumerator FinishInSeconds(float time)
    {
        yield return new WaitForSeconds(time);

        SimpleAnswerController simple = dataController.GetComponent<SimpleAnswerController>();
        MultipleChoiceController multiple = dataController.GetComponent<MultipleChoiceController>();
        if(simple != null) {
            simple.forceFinish();
        } else if(multiple != null) {
            multiple.forceFinish();
        }
    }   
}
