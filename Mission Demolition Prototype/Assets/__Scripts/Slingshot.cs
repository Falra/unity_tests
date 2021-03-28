using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    static private Slingshot S;
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMultiplier = 10f;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPosition;
    public GameObject projectile;
    public bool aimingMode;
    private Rigidbody projectileRigidbody;

    private void Awake()
    {
        S = this;
        Transform launchPointTransform = transform.Find("LaunchPoint");
        launchPoint = launchPointTransform.gameObject;
        launchPoint.SetActive(false);
        launchPosition = launchPointTransform.position;
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
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPosition;
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }
    private void Update()
    {
        if (!aimingMode) return;
        // координаты мыши
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPosition;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projectilePosition = launchPosition + mouseDelta;
        projectile.transform.position = projectilePosition;
        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMultiplier;
            FollowCam.POI = projectile;
            projectile = null;
        }
    }

    static public Vector3 LAUNCH_POINT
    {
        get
        {
            if (S == null) return Vector3.zero;
            return S.launchPosition;
        }
    }

}
