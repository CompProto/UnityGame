﻿using UnityEngine;
using System.Collections;

public class DungeonCamera : MonoBehaviour
{
    public GameObject targetPlayer;
    public float damping = 1;
    public float minZoom = 3;
    public float maxZoom = 10; 

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - targetPlayer.transform.position;
    }

    void Update()
    {
        // Debug.Log(offset);
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            if(offset.magnitude < minZoom && scroll > 0)
            {
                return;
            }
            else if(offset.magnitude > maxZoom && scroll < 0)
            {
                return;
            }
            offset *= 1 - scroll;
            // Debug.Log(Input.GetAxis("Mouse ScrollWheel")); 
        }
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = targetPlayer.transform.position + offset;
        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        transform.position = position;
        transform.LookAt(targetPlayer.transform.position);
    }
}
