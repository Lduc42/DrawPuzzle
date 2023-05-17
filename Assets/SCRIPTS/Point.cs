using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ObjectToMovement"))
        {
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
            }
        }
        
    }
    public void SetId(int value)
    {
        id = value;
    }
}
