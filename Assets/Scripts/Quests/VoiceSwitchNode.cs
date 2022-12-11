using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class VoiceSwitchNode : BaseNode 
{
    [Output] public BaseNode defaultNode;
    [Output(dynamicPortList = true)] public EVoiceSetting[] outputs;

    protected override void Init() 
    {
		base.Init();
        type = ENodeType.Switch;

        NodePort defaultport = GetOutputPort("defaultNode").Connection;
        if (defaultport != null)
        {
            defaultNode = defaultport.node as BaseNode;
        }
    }

    public override void Enter()
    {
        next = null;
        EVoiceSetting voice = GameSettings.Inst.voiceSetting;
        for(int i = 0; i < outputs.Length; i++)
        {
            int index = i;
            if(outputs[index] == voice)
            {
                next = GetOutputPort("outputs " + index).Connection.node as BaseNode;
                isdone = true;
                break;
            }
        }
        if(next == null)
        {
            next = defaultNode;
            isdone = true;
        }
    }

    public override void Execute()
    {
    }

    public override void Exit()
    {
    }
}