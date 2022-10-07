using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject poi;

    [Header("Set in Inspecter")]
    public float easing = 0.05f;
    public Vector2 minXy = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ;

   
    void Awake()
    {
        camZ = this.transform.position.z;
    }

    
    void FixedUpdate()
    {
        Vector3 destination;
        if (poi == null) destination = Vector3.zero;

        else
        {
            destination = poi.transform.position;
            if (poi.tag == "Projectile" && poi.GetComponent<Rigidbody>().IsSleeping() == true)
            {
                poi = null;
                return;
            }
            destination.x = Mathf.Max(minXy.x, destination.x);
            destination.y = Mathf.Max(minXy.y, destination.y);
        }

        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10;
    }
}
