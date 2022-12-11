using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETransitionParams
{
    isSideways, isForward, forceTransition, hasLeftClicked, hasRightClicked
}

public class CharacterState : StateMachineBehaviour
{
    public List<State> transitionList = new List<State>();
    private CharacterControl characterControl;

    public CharacterControl GetCharControl(Animator _anim)
    {
        if (characterControl == null)
            characterControl = _anim.GetComponentInParent<CharacterControl>();

        return characterControl;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (State s in transitionList)
            s.OnEnter(this, stateInfo, animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (State s in transitionList)
            s.OnExit(this, stateInfo, animator);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (State s in transitionList)
            s.OnUpdate(this, stateInfo, animator);
    }
}
