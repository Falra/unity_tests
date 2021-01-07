using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeExample : MonoBehaviour
{
    public int numTimesCalled = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() {
        PrintGameObjectName(this.gameObject);
        SetColor(Color.red, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        numTimesCalled++;    
        //PrintUpdates();
    }

    void PrintUpdates() {
        string OutputMassage = "Updates: " + numTimesCalled;
        print(OutputMassage);
    }

    void PrintGameObjectName(GameObject obj) {
        print(obj.name);
    }

    void SetColor(Color color, GameObject obj) {
        Renderer r = obj.GetComponent<Renderer>();
        r.material.color = color;
    }
}
