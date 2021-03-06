using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 position = this.transform.position;
        position.x = mousePos3D.x;
        this.transform.position = position;    
    }

    void OnCollisionEnter(Collision other) {
        GameObject collidedWith = other.gameObject;
        if (collidedWith.tag == "Apple") {
            Destroy(collidedWith);
        }    
    }
}
