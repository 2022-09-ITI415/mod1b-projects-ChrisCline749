using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{

    [Header("Set in Inspecter")]
    public GameObject poi;
    public float easing = 0.05f;

    [Header("Set Dynamically")]
    public float camZ;


    void Awake()
    {
        camZ = this.transform.position.z;
    }


    void FixedUpdate()
    {
        Vector3 destination;

        destination = poi.transform.position;
        destination.y = Mathf.Max(0, destination.y);
        

        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10;
        if (Camera.main.orthographicSize > 100) Camera.main.orthographicSize = 100;
    }
}