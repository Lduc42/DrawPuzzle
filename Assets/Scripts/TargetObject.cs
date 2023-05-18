using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    [SerializeField] private int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ObjectToMovement"))
        {
            // get object
            ObjectToMovement collidedObject = collision.gameObject.GetComponent<ObjectToMovement>();
            if (collidedObject != null)
            {
                int collidedId = collidedObject.GetId();
                //Debug.Log("collidedId: " + collidedId);
                if (collidedId == id)
                {
                    Debug.Log("moved to target " + collidedId.ToString());
                }
            }
        }
    }
    public int GetId()
    {
        return id;
    }
}
