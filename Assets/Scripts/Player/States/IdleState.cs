using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New state", menuName = "States/Idle")]
public class IdleState : State
{
    public override void OnEnter(CharacterState _state, AnimatorStateInfo _animInfo, Animator animator)
    {
    }

    public override void OnExit(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator)
    {
    }

    public override void OnUpdate(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator)
    {
        CharacterControl control = _state.GetCharControl(_animator);

        if (control.leftClicked)
        {
            _animator.SetBool(ETransitionParams.hasLeftClicked.ToString(), true);
        }

        if (control.rightClicked)
            _animator.SetBool(ETransitionParams.hasRightClicked.ToString(), true);

        if (control.xDirection != 0.0f)
            _animator.SetBool(ETransitionParams.isSideways.ToString(), true);
    }
}
