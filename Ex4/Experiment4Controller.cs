using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment4Controller : MonoBehaviour
{
    private int time = 0;
    private int amplitude = 0;
    private int spriteIndex = 0;
    public ElectrodeColorController electrode;
    public SpriteRenderer paintGraph;
    public GameObject nextButton;

    public Sprite[] sprites;
    int [,] permutation = new int [6,5] {
        {0, 0, 0, 0, 0},
        {0, 1, 2, 3, 4} ,   
        {0, 5, 6, 7, 8} ,   
        {0, 9, 10, 11, 12},
        {0, 13, 14, 15, 16},
        {0, 17, 18, 19, 20}
    };

    private void updateSprite() {
        spriteIndex = permutation[amplitude, time];
        if(spriteIndex == 0) {
            electrode.setDefaultElectrode();
        } else {
            electrode.setActiveElectrode();
        }
        paintGraph.sprite = sprites[spriteIndex];
    }

    private void updateGlobalTime(int currentTime) {
        time = currentTime;
        updateSprite();
    }

    public int getGlobalTime() {
        return time;
    }

    private void updateGlobalAmplitude(int currentAmplitude) {
        amplitude = currentAmplitude;
        updateSprite();
    }

    public int getGlobalAmplitude() {
        return amplitude;
    }

    public void updateGlobal(string type, int currentValue) {
        if(type == "time") {
            updateGlobalTime(currentValue);
        } else {
            updateGlobalAmplitude(currentValue);
        }
    }

    private void Start() {
        if(!GlobalControl.Instance.localPlayer.answeredQuestion5) {
            nextButton.SetActive(true);
        }
    }
}
