using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 3;
    public float cloudSpeedMultiplier = 0.5f;

    private GameObject[] cloudInstances;
    private void Awake()
    {
        cloudInstances = new GameObject[numClouds];
        GameObject anchor = GameObject.Find("CloudAnchor");
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);
            
            Vector3 cPosition = Vector3.zero;
            cPosition.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPosition.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

            float scaleU = Random.value;
            float scaleValue = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            cPosition.y = Mathf.Lerp(cloudPosMin.y, cPosition.y, scaleU);
            cPosition.z = 100 - 90 * scaleU;

            cloud.transform.position = cPosition;
            cloud.transform.localScale = Vector3.one * scaleValue;

            cloud.transform.SetParent(anchor.transform);
            cloudInstances[i] = cloud;
        }
    }

    private void Update() {
        foreach(GameObject cloud in cloudInstances)
        {
            float scaleValue = cloud.transform.localScale.x;
            Vector3 cPosition = cloud.transform.position;
            cPosition.x -= scaleValue * Time.deltaTime * cloudSpeedMultiplier;

            if (cPosition.x <= cloudPosMin.x)
            {
                cPosition.x = cloudPosMax.x;
            }

            cloud.transform.position = cPosition;
        }
    }
}
