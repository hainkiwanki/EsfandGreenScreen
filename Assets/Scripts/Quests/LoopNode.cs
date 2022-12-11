using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#e08d79")]
public class LoopNode : BaseNode
{
    protected override void Init()
    {
        base.Init();
        type = ENodeType.Loop;
    }

    public override void Enter()
    {
        if (next == null)
            next = prev;
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
        isdone = false;
    }
}