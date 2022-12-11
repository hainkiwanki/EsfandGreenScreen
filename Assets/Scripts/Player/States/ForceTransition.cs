using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New state", menuName = "States/Force")]
public class ForceTransition : State
{
    [SerializeField][Range(0.0f, 1.0f)]
    private float transitionTime;

    public override void OnEnter(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator)
    {
        _animator.SetBool(ETransitionParams.forceTransition.ToString(), false);
    }

    public override void OnExit(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator)
    {
        _animator.SetBool(ETransitionParams.forceTransition.ToString(), false);
    }

    public override void OnUpdate(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator)
    {
        if (_animInfo.normalizedTime >= transitionTime)
            _animator.SetBool(ETransitionParams.forceTransition.ToString(), true);
    }
}
