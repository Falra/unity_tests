using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryEx : MonoBehaviour
{
    public Dictionary<string, string> stateDict;
    // Start is called before the first frame update
    void Start()
    {
        stateDict = new Dictionary<string, string>();
        stateDict.Add("MD", "Maryland");
        stateDict.Add("TX", "Texas");
        stateDict.Add("PA", "Pennsylvania");
        stateDict.Add("CA", "California");
        stateDict.Add("MI", "Michigan");

        print("There are " + stateDict.Count + " elements in stateDict");

        foreach(KeyValuePair<string, string> kvp in stateDict) {
            print(" " + kvp.Key + ":" + kvp.Value);
        }

        print("MI is " +stateDict["MI"]);

        stateDict["BC"] = "British Columbia";

        foreach(string k in stateDict.Keys){
            print(k + " is " + stateDict[k]); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
