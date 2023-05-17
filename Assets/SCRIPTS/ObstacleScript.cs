using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    SpriteRenderer sprite_renderer;
    private float halfWidth;
    private float halfHeight;
    private void Awake()
    {
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
        halfWidth = sprite_renderer.bounds.size.x / 2;
        halfHeight = sprite_renderer.bounds.size.y / 2;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetHalfWidth()
    {
        return halfWidth;
    }
    public float GetHalfHeight()
    {
        return halfHeight;
    }
}
