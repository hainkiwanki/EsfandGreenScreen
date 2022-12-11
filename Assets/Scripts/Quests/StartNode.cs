using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNode : BaseNode
{
    public EVoiceSetting voice;

    protected override void Init()
    {
        base.Init();
        type = ENodeType.Start;
    }

    public override void Enter(){ isdone = true; }
    public override void Execute(){}
    public override void Exit(){}
}
