using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;


public class EsfandMirrorAnimation : ActionInterrupt
{
    public AudioClip eatSoundEffect;
    public Transform begin, end, bread;
    public Transform mouth;
    public PickCrumbMinigame minigame;

    private void Awake()
    {
        bread.position = begin.position;
    }

    public override void DoAction()
    {
        DoAnimation();
    }

    private void DoAnimation()
    {
        float time = 2.0f;
        bread.DOMove(end.position, time);
        mouth.DOLocalMoveY(-0.27f, time *  0.6f * 0.05f).SetDelay(time * 0.4f).SetLoops(10, LoopType.Yoyo)
        .OnStart(() => 
        {
            SoundManager.Inst.PlaySoundEffect(eatSoundEffect, 2.0f);
            minigame.SpawnBreadCrumbs((time * 0.6f * 0.05f) * 0.7f);
        })
        .OnComplete(() => 
        {
            mouth.DOLocalMoveY(0.0f, 0.0f);
            onCompleteCallback?.Invoke();
        });
    }
}
