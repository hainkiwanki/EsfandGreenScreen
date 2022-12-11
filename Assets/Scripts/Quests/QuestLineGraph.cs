using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class QuestLineGraph : SceneGraph 
{
    public EVoiceSetting voiceLock = EVoiceSetting.Default;

    private int m_currentProgress = -1;
    private BaseNode m_currentNode = null;

    private bool m_noNodeFound = false;
    private bool m_didNotEnter = false;

    private void Update()
    {
        if (m_noNodeFound) return;

        if (m_currentNode == null)
        {
            ChangeCurrentNode(GetNode(ENodeType.Start));
            m_noNodeFound = m_currentNode == null;
        }

        if(m_currentNode.progress < m_currentProgress && !(m_currentNode is LoopNode))
        {
            if (m_didNotEnter)
            {
                m_currentNode.Enter();
                m_didNotEnter = false;
            }

            m_currentNode.Execute();

            if (m_currentNode.isdone)
                ChangeCurrentNode(m_currentNode.next);
        }
    }

    public BaseNode GetNode(ENodeType _type)
    {
        for (int i = 0; i < graph.nodes.Count; i++)
        {
            StartNode node = graph.nodes[i] as StartNode;
            if (node == null)
                continue;

            if (node.type == _type && node.voice == GameSettings.Inst.voiceSetting)
                return node;
        }

        Debug.Log("No node of type " + _type.ToString() + " found");
        return null;
    }

    public void ChangeCurrentNode(BaseNode _newNode)
    {
        if (m_currentNode != null)
            m_currentNode.Exit();

        m_currentNode = _newNode;

        if (m_currentNode != null && m_currentNode.progress < m_currentProgress)
            m_currentNode.Enter();
        else
            m_didNotEnter = true;
    }

    public void ProgressTo(ref int _i)
    {
        if (m_currentNode is LoopNode)
        {
            _i = m_currentProgress;
            ChangeCurrentNode(m_currentNode.next);
        }
        m_currentProgress = _i;
    }
}