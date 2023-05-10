using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance;
    public List<PathGameObject> paths = new List<PathGameObject>();
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
    private void Update()
    {
        if(Count() == 2)
        {
            GameController.Instance.object1.pathGameObject = GetPath(0);
            GameController.Instance.object2.pathGameObject = GetPath(1);
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        Debug.Log(paths.Count);
    }
    public void AddPaths(PathGameObject path)
    {
        paths.Add(path);
    }
    public PathGameObject GetPath(int index)
    {
        return paths[index];
    }
    public int Count()
    {
        return paths.Count;
    }
}
