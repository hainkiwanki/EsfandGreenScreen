using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalrusMove : ActionInterrupt
{
    public AiController walrusEsfand;

    public override void DoAction()
    {
        GameSettings.Inst.ExecuteFunctionWithDelay(15.0f, () =>
        {
            walrusEsfand.MoveLeft(10.0f, () => 
            {
                onCompleteCallback?.Invoke();
                ProgressTracker.Inst.currentProgress = EGameProgression.Error_WhaleAppear;
            });
        });
    }
}
