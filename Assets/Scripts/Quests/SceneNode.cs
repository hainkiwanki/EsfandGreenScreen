using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XNode;

public class SceneNode : BaseNode
{
    private bool m_executed = false;
    public string scene;

    protected override void Init()
    {
        base.Init();
        type = ENodeType.Scene;
    }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        if (m_executed) return;
        m_executed = true;

        isdone = true;
        ScreenFader.Inst.FadeOut(3.0f, () =>
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        });
    }

    public override void Exit()
    {
    }
}