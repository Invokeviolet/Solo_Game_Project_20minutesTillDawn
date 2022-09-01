using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFunction : MonoBehaviour
{
    float angle;
    Vector2 target;
    Vector2 mouse;

    private void Start()
    {
        target = transform.position;
    }
    private void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}