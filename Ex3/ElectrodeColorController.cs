using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrodeColorController : MonoBehaviour
{
    public Sprite defaultElectrode;
    public Sprite activeElectrode;
    public void setDefaultElectrode() {
        GetComponent<SpriteRenderer>().sprite = defaultElectrode;
    }

    public void setActiveElectrode() {
        GetComponent<SpriteRenderer>().sprite = activeElectrode;
    }
}
