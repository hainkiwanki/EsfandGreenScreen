using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundInteractable : Interactable
{
    [Header("Sound specific")]
    public float volumeScale = 1.0f;
    public float extraDelay = 0.5f;
    public SubbedAudio audioSub;
    public EGameProgression progressToOnUse = EGameProgression.Bathroom_Mirror;

    protected override void OnUse()
    {
        if (audioSub != null)
        {
            SoundManager.Inst.PlaySubbedVoiceLine(audioSub, volumeScale, extraDelay, () =>
            {
                ProgressTracker.Inst.currentProgress = progressToOnUse;
                if (useForSidequest)
                {
                    ProgressSideQuest(ref m_amountUsed);
                }
            });
        }
        else
        {
            ProgressTracker.Inst.currentProgress = progressToOnUse;
            if (useForSidequest)
            {
                ProgressSideQuest(ref m_amountUsed);

            }
        }
    }
}
