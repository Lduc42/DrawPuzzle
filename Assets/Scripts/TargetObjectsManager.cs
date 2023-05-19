using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectsManager : MonoBehaviour
{
    [SerializeField] private TargetObject[] targetObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public TargetObject GetTargetObject( int idx)
    {
        return targetObjects[idx];
    }
}
