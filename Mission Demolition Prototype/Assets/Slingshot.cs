using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
   
   [Header("Set in Inspector")]
   public GameObject prefabProjectile;

   [Header("Set Dynamically")]
   public GameObject launchPoint;
   public Vector3 launchPosition;
   public GameObject projectile;
   public bool aimingMode;

   private void Awake() {
       Transform launchPointTransform = transform.Find("LaunchPoint");
       launchPoint =  launchPointTransform.gameObject;
       launchPoint.SetActive(false);
       launchPosition = launchPointTransform.position;
   }

   private void OnMouseEnter() {
       launchPoint.SetActive(true);
   }

   private void OnMouseExit() {
       launchPoint.SetActive(false);    
   }
   private void OnMouseDown() {
       aimingMode = true;
       projectile = Instantiate(prefabProjectile) as GameObject;
       projectile.transform.position = launchPosition;
       projectile.GetComponent<Rigidbody>().isKinematic = true;
   }

}
