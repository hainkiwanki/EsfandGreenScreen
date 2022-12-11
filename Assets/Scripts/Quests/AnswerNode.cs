using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[NodeTint("#136F63")]
public class AnswerNode : BaseNode
{
    [HideLabel, TextArea]
    public string text;
    [Output(dynamicPortList = true)] public string[] outputs;
    private bool m_executed = false;
    private ChatBox m_box;

    protected override void Init()
    {
        base.Init();
        type = ENodeType.Answer;
    }

    public override void Enter()
    {
        m_box = GameSettings.Inst.chatBox;
        m_box.TurnToAnswer();
        m_box.question = text;
        for(int i = 0; i < outputs.Length; i++)
        {
            int index = i;
            string portStr = "outputs " + index;
            var node = GetOutputPort(portStr).Connection.node as BaseNode;
            m_box.AddButton(outputs[index], () =>
            {
                if(node != null)
                {
                    next = node;
                }
                isdone = true;
            });
        }
    }

    public override void Execute()
    {
        if (m_executed) return;

        m_executed = true;
        m_box.Show();
        GameSettings.Inst.controls.Disable();
    }

    public override void Exit()
    {
        m_box.Hide();
        GameSettings.Inst.controls.Enable();
    }
}