using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToMovement : MonoBehaviour
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
    public int GetId()
    {
        return id;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ObjectToMovement"))
        {
            Time.timeScale = 0;
        }
    }
}
