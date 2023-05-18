using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class CharacterStateManager : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, move, loot_bottom, loot_hair, loot_top, lose, win;
    public string current_state;
    public string current_animation;
    private bool not_move = true;
    private int track_index = 0;
    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        current_state = "Idle";
        SetCharacterState(current_state);
    }

    // Update is called once per frame
    void Update()
    {
        SetCharacterState(current_state);
    }
    public void SetMove(bool value)
    {
        not_move = value;
    }
    public bool GetMove()
    {
        return not_move;
    }
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float time_scale)
    {
        if (animation.name.Equals(current_animation)) return;
        skeletonAnimation.state.SetAnimation(track_index, animation, loop).TimeScale = time_scale;
        track_index++;
        current_animation = animation.name;
    }
    public void AddAnimation(AnimationReferenceAsset animation, bool loop,float time_scale, float delay)
    {
        skeletonAnimation.state.AddAnimation(0, animation, loop,0f);
    }
    public void SetCharacterState(string state)
    {
        if (state.Equals("Idle"))
        {
            SetAnimation(idle, true, 1f);
        }
        else if (state.Equals("Move"))
        {
            SetAnimation(move, true, 1f);
        }
        else if (state.Equals("LootBottom"))
        {
            SetAnimation(loot_bottom, true, 1f);
        }
        else if (state.Equals("LootHair"))
        {
            SetAnimation(loot_hair, true, 1f);
        }
        else if (state.Equals("LootTop"))
        {
            SetAnimation(loot_top, true, 1f);
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
    public string GetState()
    {
        return current_state;
    }
}
