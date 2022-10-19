using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateDial : MonoBehaviour
{
    private const float startAngle = 180;
    private bool isPressed = false;
    InputActions controls = null;

    void Awake() {
        controls = new InputActions();
        controls.Dialogue.Enable();
        controls.Dialogue.Click.performed += _ => changePressed(true);
        controls.Dialogue.Click.canceled += _ => changePressed(false);
    }

    void Update() {
        Vector2 mp = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Collider2D c = Physics2D.OverlapPoint(mp);
        if (c != null && c.gameObject.tag == "Handle" && isPressed)
        {
            Vector2 dir = mp - (Vector2)transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - startAngle;

            if (angle < transform.eulerAngles.z)
            {
                if(angle <= -360) angle = 360;
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }

    void changePressed(bool boolean) {
        isPressed = boolean;
    }

    private static float unwrapAngle(float angle)
    {
        if(angle >=0) return angle;
        angle = -angle%360;
        return 360-angle;
    }
}
