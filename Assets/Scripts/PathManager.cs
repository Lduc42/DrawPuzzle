using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance;
    public List<PathGameObject> paths = new List<PathGameObject>();
    [SerializeField] private int max_line;
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
        if(Count() == max_line)
        {
            for(int i = 0; i < max_line; i++)
            {
                for (int j = 0; j < max_line; j++)
                {
                    if (Mathf.Abs(GetPath(j).GetFirstPosition().x - GameController.Instance.GetObject(i).transform.position.x) <= 0.3f &&
                        Mathf.Abs(GetPath(j).GetFirstPosition().y -
                                  GameController.Instance.GetObject(i).transform.position.y) <= 0.3f)
                    {
                        GameController.Instance.GetObject(i).SetPathGameObject(GetPath(j));
                    }
                }               
            }
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //Debug.Log(paths.Count);
    }
    public void AddPaths(PathGameObject path)
    {
        path.SetId(Count() - 1);
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
