using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class TargetStateManager : MonoBehaviour,IObserver
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, lose, win;
    public string current_state;
    public string current_animation;
    public ParticleSystem heart_animation;
    private bool isCount;
    // Start is called before the first frame update
    void Start()
    {
        //skeletonAnimation = GetComponent<SkeletonAnimation>();
        current_state = "Idle";
        SetCharacterState(current_state);
        heart_animation = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float time_scale)
    {
        if (animation.name.Equals(current_animation)) return;
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = time_scale;
        current_animation = animation.name;
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("Idle"))
        {
            SetAnimation(idle, true, 1f);
        }
        else if (state.Equals("Lose"))
        {
            SetAnimation(lose, true, 1f);
        }
        else if (state.Equals("Win"))
        {
            SetAnimation(win, true, 1f);
        }
    }

    public void OnWin()
    {
        SetCharacterState("Win");
        heart_animation.Play();
        if (!isCount)
        {
            GameManager.Instance.AddCountState();
            isCount = true;
        }
    }

    public void OnLose()
    {
        SetCharacterState("Lose");
        if (!isCount)
        {
            GameManager.Instance.AddCountState();
            isCount = true;
            GameManager.Instance.AddCountLose();
        }
    }
}
