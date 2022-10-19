using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HoverController : MonoBehaviour
{
    public GameObject objectToHide = null;
    // Start is called before the first frame update
    void Start()
    {
        objectToHide.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        Vector2 mp = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Collider2D c = Physics2D.OverlapPoint(mp);
        if (c != null && c.gameObject.name == name)
        {
            objectToHide.SetActive(true);
        } else {
            objectToHide.SetActive(false);
        }
    }
}
