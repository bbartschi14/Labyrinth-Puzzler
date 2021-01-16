using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Transform moveTo;
    private GameObject target;
    private Vector3 posTo;
    private Rigidbody rb;
    private int frames = 60;
    private int currentFrame = 0;
    private int direction = -1;
    private Vector3 pos0;
    void Start()
    {
        pos0 = transform.position;
        posTo = moveTo.transform.position;
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (currentFrame == frames || currentFrame == 0)
        {
            direction *= -1;
        }
        rb.MovePosition(Vector3.Lerp(pos0, posTo, (float) currentFrame / frames));
        currentFrame += direction;
    }
    
}
