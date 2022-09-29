using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;

    [Header("Set in Inspecter")]
    public GameObject prefabProjectile;
    public float velocityMult = 8;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aiming;

    private Rigidbody projectileRigidbody;

    static public Vector3 Launch_Pos
    {
        get
        {
            if (S == null) return Vector3.zero;
            return S.launchPos;
        }
    }

    private void Awake()
    {
        S = this;

        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;

    }
    private void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        //check if player has projectiles?

        aiming = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        

        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;

    }

    private void Update()
    {
        if (aiming == false) return;

        Vector3 mousePos2d = Input.mousePosition;
        mousePos2d.z = -Camera.main.transform.position.z;
        Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(mousePos2d);

        Vector3 mouseDelta = mousePos3d - launchPos;

        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            aiming = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.poi = projectile;

            projectile = null;
        }
    }
}
