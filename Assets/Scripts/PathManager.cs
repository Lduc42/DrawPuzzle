using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    #region delcare
    public static PathManager Instance;
    public List<PathGameObject> paths = new List<PathGameObject>();
    [SerializeField] private int max_line;
    #endregion
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
        //complete all line, set line for each object
        if(Count() == max_line)
        {
            SetPathForObject();
        }
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        
    }
    private void SetPathForObject()
    {
        for (int i = 0; i < max_line; i++)
        {
            for (int j = 0; j < max_line; j++)
            {
                if (Mathf.Abs(GetPath(j).GetFirstPosition().x - DrawController.Instance.GetObject(i).transform.position.x) <= 0.3f &&
                    Mathf.Abs(GetPath(j).GetFirstPosition().y -
                              DrawController.Instance.GetObject(i).transform.position.y) <= 0.3f)
                {
                    DrawController.Instance.GetObject(i).SetPathGameObject(GetPath(j));
                }
            }
        }
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
