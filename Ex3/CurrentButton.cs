using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentButton : MonoBehaviour
{
    public Sprite defaultButton;
    public Sprite activeButton;
    public void setDefaultButton() {
        GetComponent<SpriteRenderer>().sprite = defaultButton;
    }

    public void setActiveButton() {
        GetComponent<SpriteRenderer>().sprite = activeButton;
    }
}
