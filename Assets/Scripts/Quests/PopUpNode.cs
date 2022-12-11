using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Sirenix.OdinInspector;

public class PopUpNode : BaseNode
{
    public EPopUpType popup;
    private bool m_executed = false;
    [HideLabel, TextArea]
    public string text;

    protected override void Init()
    {
        base.Init();
        type = ENodeType.PopUp;
    }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        if (m_executed) return;
        m_executed = true;

        switch (popup)
        {
            case EPopUpType.Completion:
                PopUpManager.Inst.CreateCompletionPopUp(text, true, null, () => { isdone = true; });
                break;
            case EPopUpType.Failure:
                PopUpManager.Inst.CreateCompletionPopUp(text, false, null, () => { isdone = true; });
                break;
            default:
                isdone = true; 
                break;
        }
    }

    public override void Exit()
    {
    }
}