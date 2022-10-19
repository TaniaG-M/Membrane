using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCheckController : MonoBehaviour
{
    string nextScene = "Pretest1";
    // Start is called before the first frame update
    void Start()
    {
        if(GlobalControl.Instance.localPlayer.answeredQuestion4)
        {
            nextScene = "Experiment1";
        } else if(GlobalControl.Instance.localPlayer.uuid != "") {
            nextScene = "Pretest2";
        }
    }

    public void goToNextScene() {
        LevelLoader.Instance.startTransitionTo(nextScene);
    }
}
