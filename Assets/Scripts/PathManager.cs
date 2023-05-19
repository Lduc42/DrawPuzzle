using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    #region delcare
    public static PathManager Instance;
    public List<PathGameObject> paths = new List<PathGameObject>();
    public int max_line;
    private bool isSetPath;
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
        if(Count() == max_line && !isSetPath)
        {
           // Debug.Log("du line");
            SetPathForObject();
            isSetPath = true;
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
            /*            for (int j = 0; j < max_line; j++)
                        {
                            if (Mathf.Abs(GetPath(j).GetFirstPosition().x - DrawController.Instance.GetObject(i).transform.position.x) <= 1f &&
                                Mathf.Abs(GetPath(j).GetFirstPosition().y -
                                          DrawController.Instance.GetObject(i).transform.position.y) <= 1f)
                            {
                                DrawController.Instance.GetObject(i).SetPathGameObject(GetPath(j));
                            }
                            else
                            {
                                //Debug.Log("xa object");
                            }
                        }*/
            GameManager.Instance.CurrentLevel.drawController.GetObject(paths[i].GetId() -1).SetPathGameObject(GetPath(i));
        }
    }
    public void AddPaths(PathGameObject path)
    {
        //path.SetId(Count() - 1);
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
    public bool IsEnough()
    {
        if (Count() - max_line == 0) return true;
        else return false;
    }
    public int GetMaxLine()
    {
        return max_line;
    }
    public void SetMaxLine(int value)
    {
        max_line = value;
    }
    public void Reset()
    {
        isSetPath = false;
    }
}
