using System.Collections.Generic;
using UnityEngine;

public class PathGameObject : MonoBehaviour
{
    public static PathGameObject Instance;
    public List<Vector3> points = new List<Vector3>();
    LineRenderer lineRenderer;
    private int id;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SetId(int idx)
    {
        id = idx;
    }
    public Vector2 GetPosition(int index)
    {
        return points[index];
    }

    public void AddPosition(Vector3 position)
    {
        points.Add(position);
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public int Count()
    {
        return points.Count;
    }
    public Vector2 GetLastPosition()
    {
        return points[Count() - 1];
    }

    public Vector2 GetFirstPosition()
    {
        return points[0];
    }
    public bool IsPathValid()
    {
        
            return false;
    }
}
