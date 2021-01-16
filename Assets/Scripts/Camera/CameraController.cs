﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject trackedObject;

    public float cameraHeight = 10;
    public float cameraCenteringX = 0;
    public float cameraCenteringZ = -2;

    public float cameraTilt = 80;
    public float cameraPan = 0;
    public float cameraYaw = 0;

    private Vector3 cameraOffset;
    private Quaternion cameraRotation;

    private Vector3 defaultOffset;
    private Quaternion defaultRotation;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate and set default offset based on settings
        defaultOffset = new Vector3(cameraCenteringX, cameraHeight, cameraCenteringZ);
        transform.position = trackedObject.transform.position + defaultOffset;

        // Calculate and set default camera rotation based on settings
        defaultRotation = Quaternion.Euler(cameraTilt, cameraPan, cameraYaw);
        transform.rotation = defaultRotation;
    }

    // LateUpdate is called once per frame, after Update().
    void LateUpdate()
    {
        float x = trackedObject.transform.position.x + defaultOffset.x;
        float y = defaultOffset.y;
        float z = trackedObject.transform.position.z + defaultOffset.z;

        transform.position = new Vector3(x, y, z);
    }
}
