using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class EndNode : BaseNode
{
    public bool playEndSfx = false;

    protected override void Init()
    {
        base.Init();
        type = ENodeType.End;
    }

    public override void Enter()
    {
        if(playEndSfx)
            SoundManager.Inst.QComplete();
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}