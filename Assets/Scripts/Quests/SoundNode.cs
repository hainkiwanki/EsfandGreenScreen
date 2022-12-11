using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class SoundNode : BaseNode 
{
    public AudioClip sfx;

	protected override void Init()
    {
		base.Init();
        type = ENodeType.Sound;
	}

    public override void Exit()
    {
    }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        SoundManager.Inst.PlaySoundEffect(sfx);
        isdone = true;
    }
}