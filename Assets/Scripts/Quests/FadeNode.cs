using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#1B2432")]
public class FadeNode : BaseNode 
{
    private bool m_executed = false;
    public float fadeTime = 1.0f;
    public bool fadeOut = true;
    public bool muteMusic = true;

	protected override void Init() 
    {
		base.Init();
        type = ENodeType.Fade;
	}
    public override void Exit()
    {
    }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        if (m_executed) return;
        m_executed = true;

        if (fadeOut)
        {
            if(muteMusic)
                SoundManager.Inst.FadeOutMusic();
            ScreenFader.Inst.FadeOut(fadeTime, () =>
            {
                isdone = true;
            });
        }
        else
        {
            if(muteMusic)
                SoundManager.Inst.FadeInMusic();
            ScreenFader.Inst.FadeIn(fadeTime, () => 
            {
                isdone = true;
            });
        }
    }
}