using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public abstract void OnEnter(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator);
    public abstract void OnUpdate(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator);
    public abstract void OnExit(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator);
}
