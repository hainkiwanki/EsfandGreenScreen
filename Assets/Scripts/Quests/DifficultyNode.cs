using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#CCA43B")]
public class DifficultyNode : BaseNode 
{
    [Output] public BaseNode defaultNode;
    [Output(dynamicPortList = true)] public EGameDifficulty[] outputs;

    private BaseNode result = null;

    protected override void Init()
    {
        base.Init();
        type = ENodeType.Difficulty;
    }

    public override void Enter()
    {
        for(int i = 0; i < outputs.Length; i++)
        {
            if(outputs[i] == GameSettings.Inst.gameDifficulty)
            {
                int index = i;
                result = GetOutputPort("outputs " + i).Connection.node as BaseNode;
                break;
            }
        }

        if(result == null)
        {
            result = GetOutputPort("defaultNode").Connection.node as BaseNode;
        }

        next = result;
        isdone = true;
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}