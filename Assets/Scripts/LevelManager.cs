using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] DrawController DrawController;
    [SerializeField] TargetObjectsManager TargetObjectsManager;

    public DrawController drawController => DrawController;
    public TargetObjectsManager targetObjectManager => TargetObjectsManager; 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
