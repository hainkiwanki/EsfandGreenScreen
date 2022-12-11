using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New state", menuName = "States/Attack")]
public class AttackState : State
{
    public float startAttackTime, endAttackTime;

    public override void OnEnter(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator)
    {
        CharacterControl control = _state.GetCharControl(_animator);
        _animator.SetBool(ETransitionParams.hasLeftClicked.ToString(), false);
        _animator.SetBool(ETransitionParams.hasRightClicked.ToString(), false);
    }

    public override void OnExit(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator)
    {
        CharacterControl control = _state.GetCharControl(_animator);
        _animator.SetBool(ETransitionParams.hasLeftClicked.ToString(), false);
        _animator.SetBool(ETransitionParams.hasRightClicked.ToString(), false);
    }

    public override void OnUpdate(CharacterState _state, AnimatorStateInfo _animInfo, Animator _animator)
    {
        // RegisterAttack(_state, _animInfo, _animator);
        // DeregisterAttack(_state, _animInfo, _animator);
        CheckCombo(_state, _animInfo, _animator);
    }

    public void RegisterAttack(CharacterState _state, AnimatorStateInfo _stateInfo, Animator _animator)
    {
        if (startAttackTime <= _stateInfo.normalizedTime && endAttackTime >= _stateInfo.normalizedTime)
        {
            //foreach (AttackInfo info in AttackManager.Inst.currentAttacks)
            //{
            //    if (info == null)
            //        continue;

            //    if (!info.isRegistered && info.attackState == this)
            //    {
            //        if (debug)
            //        {
            //            Debug.Log(name + " registered " + _stateInfo.normalizedTime);
            //        }
            //        info.Register(this);
            //    }
            //}
        }
    }

    public void DeregisterAttack(CharacterState _state, AnimatorStateInfo _stateInfo, Animator _animator)
    {
        if (_stateInfo.normalizedTime >= endAttackTime)
        {
            //foreach (AttackInfo info in AttackManager.Inst.currentAttacks)
            //{
            //    if (info == null)
            //        continue;

            //    if (info.attackState == this && !info.isFinished)
            //    {
            //        if (debug)
            //        {
            //            Debug.Log(name + " deregistered " + _stateInfo.normalizedTime);
            //        }

            //        info.isFinished = true;
            //        info.GetComponent<PoolObject>().TurnOff();
            //    }
            //}
        }
    }

    public void CheckCombo(CharacterState _state, AnimatorStateInfo _stateInfo, Animator _animator)
    {
        if (_stateInfo.normalizedTime >= startAttackTime + ((endAttackTime - startAttackTime) / 3.0f))
        {
            if (_stateInfo.normalizedTime < endAttackTime + ((endAttackTime - startAttackTime) / 2.0f))
            {
                CharacterControl control = _state.GetCharControl(_animator);
                if(control.leftClicked)
                {
                    _animator.SetBool(ETransitionParams.hasLeftClicked.ToString(), true);
                }
                if(control.rightClicked)
                {
                    _animator.SetBool(ETransitionParams.hasRightClicked.ToString(), true);
                }
            }
        }
    }
}
