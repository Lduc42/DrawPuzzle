using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int id;
    public string state;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
/*    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ObjectToMovement"))
        {
            Debug.Log("object va cham voi point");
            // get object
            ObjectToMovement collidedObject = other.gameObject.GetComponent<ObjectToMovement>();
            if (collidedObject != null)
            {
                int collidedId = collidedObject.GetId();
                Debug.Log("collidedId: " + collidedId);
                if (collidedId == id)
                {
                    Debug.Log("pass point");
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("id point:" + id);
                }
            }
        }
        
    }*/
    public void SetId(int value)
    {
        id = value;
    }
    public int GetId()
    {
        return id;
    }
    public string GetState()
    {
        return state;
    }
}
