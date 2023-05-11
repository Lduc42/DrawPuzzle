using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectsManager : MonoBehaviour
{
    public static TargetObjectsManager Instance;
    [SerializeField] private TargetObject[] targetObjects;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
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
