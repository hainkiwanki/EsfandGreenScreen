using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class MusicNode : BaseNode 
{
    public AudioClip music;
    private bool executed = false;

    protected override void Init()
    {
        base.Init();
        type = ENodeType.Music;
    }

    public override void Enter()
    {

    }

    public override void Execute()
    {
        if (executed) return;
        executed = true;

        SoundManager.Inst.PlayMusic(music);
        isdone = true;
    }

    public override void Exit()
    {

    }
}