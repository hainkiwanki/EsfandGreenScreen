using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New state", menuName = "States/MoveSide")]
public class MoveSideState : State
{
    [SerializeField]
    private float m_speed = 10.0f;
    [SerializeField]
    public bool m_flipMovement = false;

    public override void OnEnter(CharacterState _state, AnimatorStateInfo _animInfo, Animator animator)
    {
    }

    public override void OnExit(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator)
    {
    }

    public override void OnUpdate(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator)
    {
        CharacterControl control = _state.GetCharControl(_animator);

        if (control.xDirection < 0.0f)
            control.Flip(false);
        else if (control.xDirection > 0.0f)
            control.Flip(true);

        control.Move(control.xDirection, m_speed);

        if (control.xDirection == 0.0f)
            _animator.SetBool(ETransitionParams.isSideways.ToString(), false);

        if (control.leftClicked)
            _animator.SetBool(ETransitionParams.hasLeftClicked.ToString(), true);

        if (control.rightClicked)
            _animator.SetBool(ETransitionParams.hasRightClicked.ToString(), true);
    }
}
