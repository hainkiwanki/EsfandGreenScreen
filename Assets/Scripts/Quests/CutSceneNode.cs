using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using XNode;

public class CutSceneNode : BaseNode 
{
    public VideoClip video;
    private bool m_executed = false;

    public override void Enter()
    {
    }

    public override void Execute()
    {
        if (m_executed) return;

        SoundManager.Inst.FadeOutMusic(1.0f);
        CutsceneManager.Inst.PlayCutscene(video);
        isdone = true;
        m_executed = true;
    }

    public override void Exit()
    {
    }

    protected override void Init()
    {
		base.Init();
        type = ENodeType.Cutscene;
	}

}