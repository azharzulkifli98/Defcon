﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    float cameraHeight = 0;

    [SerializeField]
    float cameraSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(cameraSpeed * Time.deltaTime * (Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward));
        transform.position = new Vector3(transform.position.x, cameraHeight, transform.position.z);
    }
}
