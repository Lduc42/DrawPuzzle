using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPathManager : MonoBehaviour
{
    [SerializeField] private PointPath[] points;
    private bool pass_checkpoint;
    void Start()
    {

    }

    void Update()
    {
        if(CheckPassPoint() == true)
        {
            Debug.Log("Win");
        }
        else
        {
           // Debug.Log("Lose");
        }
    }
    public bool CheckPassPoint()
    {
        foreach(PointPath path in points)
        {
            if (path.GetStatus() == false) return false;
        }
        return true;
    }
}
