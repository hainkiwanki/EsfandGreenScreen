using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletionMessage : PopUpMessage
{
    public Sprite success, failure;
    public Image completionBackground;
    public AudioClip successTune, failureTune;
    public float volScaleSuccess, volScaleFailure;

    public string successMessage
    {
        set
        {
            completionBackground.sprite = success;
            SoundManager.Inst.PlaySoundEffect(successTune, volScaleSuccess);
            m_messageText.text = value;
        }
    }

    public string failureMessage
    {
        set
        {
            completionBackground.sprite = failure;
            SoundManager.Inst.PlaySoundEffect(failureTune, volScaleFailure);
            m_messageText.text = value;
        }
    }

    public float successDuration
    {
        get
        {
            return successTune.length;
        }
    }

    public float failureDuration
    {
        get
        {
            return failureTune.length;
        }
    }
}
