using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPath : MonoBehaviour
{
    [SerializeField] private Point[] points;
    private int count_point = 0;
    private bool allInActive = false;
    [SerializeField] private int id;
    private bool pass_all_point;
    // Start is called before the first frame update
    void Start()
    {
        SetIdForPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckInActiveAllPoint())
        {
            allInActive = true;
        }
    }
    public bool GetPassAllPoint()
    {
        return pass_all_point;
    }
    public void SetPassAllPoint(bool value)
    {
        pass_all_point = value;
    }
    public int CountPoint()
    {
        return count_point;
    }
    public void AddCount()
    {
        count_point++;
    }
    public void ResetCount()
    {
        count_point = 0;
    }
    public bool CheckInActiveAllPoint() {
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
    public bool GetStatus()
    {
        return allInActive;
    }
    private void SetIdForPoints()
    {
        for(int i = 0; i < points.Length; i++)
        {
            points[i].SetId(id);
        }
    }
}
