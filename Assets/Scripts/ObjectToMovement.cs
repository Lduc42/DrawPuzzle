using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToMovement : MonoBehaviour
{
    [SerializeField] private int id;
    public Color colorLine;
    private ParticleSystem hit_animation;
    private MoveObjectAlongPath move_object;
    // Start is called before the first frame update
    void Start()
    {
        hit_animation = GetComponentInChildren<ParticleSystem>();
        move_object = GetComponent<MoveObjectAlongPath>();
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
            move_object.SetMove(true);
            hit_animation.Play();
            move_object.GetState().current_state = "Lose";
            move_object.GetTargetSubject().NotifyLoseObservers();
        }
    }
}
