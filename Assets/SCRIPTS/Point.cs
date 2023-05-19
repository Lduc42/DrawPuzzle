using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class Point : MonoBehaviour
{
    public int id;
    public State state;
    public ParticleSystem lootAnimation;
    public enum State
    {
        Idle, Move, LootBottom, LootHair, LootTop, Lose, Win
    }
    // Start is called before the first frame update
    void Start()
    {
        lootAnimation = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
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
                    lootAnimation.Play();
                    StartCoroutine(DelayToDeActive(0.7f));
                }
                else
                {
                    Debug.Log("id point:" + id);
                }
            }
        }

    }
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
        return state.ToString();
    }
    IEnumerator DelayToDeActive(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
