using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise3Controllers : MonoBehaviour
{
    private int current = 1;
    private int step = 0;
    private int spriteIndex = 0;
    public ElectrodeColorController electrode;
    public SpriteRenderer paintGraph;

    public Sprite[] sprites;

    private void updateSprite() {
        spriteIndex = step * current;
        if(spriteIndex < 0) spriteIndex = spriteIndex + 7;
        if(spriteIndex == 0) {
            electrode.setDefaultElectrode();
        } else {
            electrode.setActiveElectrode();
        }
        paintGraph.sprite = sprites[spriteIndex];
        Debug.Log(spriteIndex);
    }

    public void updateGlobalStep(int currentStep) {
        step = currentStep;
        updateSprite();
    }

    public int getGlobalStep() {
        return step;
    }

    public void updateGlobalCurrent(int newCurrent) {
        current = newCurrent;
        updateSprite();
    }

    public int getGlobalCurrent() {
        return current;
    }
}
