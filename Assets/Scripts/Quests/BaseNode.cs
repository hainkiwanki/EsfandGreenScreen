using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Sirenix.OdinInspector;

public enum ENodeType
{
    Base,
    Start,
    Chat,
    Minigame,
    End,
    Answer,
    Loop,
    Action,
    Difficulty,
    PopUp,
    Reward,
    Scene,
    Fade,
    Sound,
    Music,
    Switch,
    Cutscene,
}

[NodeWidth(256)]
public abstract class BaseNode : Node 
{
    [ReadOnly, HideLabel, HorizontalGroup("Info")]
    public ENodeType type = ENodeType.Base;
    [HideLabel, HorizontalGroup("Info", MaxWidth = 0.15f), OnValueChanged("OnNameChange")]
    public int progress = 0;
    [HideIf("type", ENodeType.End),
     HideIf("type", ENodeType.Answer), 
     HideIf("type", ENodeType.Difficulty),
     HideIf("type", ENodeType.Switch), Output]
    public BaseNode next;
    [HideIf("type", ENodeType.Start), Input]
    public BaseNode prev;
    [HideInInspector]
    public bool isdone = false;

    protected override void Init() 
    {
        NodePort nextPort = GetOutputPort("next").Connection;
        if (nextPort != null)
        {
            next = nextPort.node as BaseNode;
        }

        NodePort prevPort = GetInputPort("prev").Connection;
        if (prevPort != null)
        {
            prev = prevPort.node as BaseNode;
        }
        base.Init();		
	}

	public override object GetValue(NodePort port) {
        return null;
	}

    public virtual void OnNameChange()
    {
        name = type.ToString() + " " + progress;
    }

    public abstract void Exit();
    public abstract void Enter();
    public abstract void Execute();
}