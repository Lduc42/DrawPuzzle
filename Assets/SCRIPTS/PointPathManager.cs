using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPathManager : MonoBehaviour
{
    [SerializeField] private PointPath[] points;
    private bool pass_checkpoint;
    private List<int> complete_point_path;
    void Start()
    {

    }

    void Update()
    {
        if(CheckPassLevel() == true)
        {
            //Debug.Log("Win");
        }
        else
        {
           // Debug.Log("Lose");
        }
    }
    public bool CheckPassLevel()
    {
        foreach(PointPath path in points)
        {
            if (path.GetStatus() == false) return false;
        }
        return true;
    }
}
