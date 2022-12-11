using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XNode;

[NodeTint("#1E91D6")]
public class ActionNode : BaseNode
{ 
    public UnityEvent doEvent;
    public bool doOnComplete = false;
    [ShowIf("doOnComplete")]
    public ActionInterrupt actionInterrupt;
    private bool m_executed = false;

	protected override void Init() 
    {
		base.Init();
        type = ENodeType.Action;
	}

    public override void Exit()
    {
        if (actionInterrupt != null)
            actionInterrupt.onCompleteCallback -= OnActionComplete;
    }

    public override void Enter()
    {
        if (actionInterrupt != null)
            actionInterrupt.onCompleteCallback += OnActionComplete;
    }

    private void OnActionComplete()
    {
        isdone = true;
    }

    public override void Execute()
    {
        if (m_executed) return;
        m_executed = true;

        doEvent?.Invoke();
        if(!doOnComplete)
            isdone = true;
        actionInterrupt?.DoAction();
    }
}