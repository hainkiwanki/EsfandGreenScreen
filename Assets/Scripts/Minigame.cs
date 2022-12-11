using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class Minigame : SerializedMonoBehaviour
{
    [Header("Base variables")]
    public GameObject characterToControl;
    public bool useTransition;
    public int startProgress = 1;
    protected int m_progress = 0;
    protected bool m_hasStarted;
    public string transformStr;
    public AudioClip newMusic, completeMusic;
    public QuestLineGraph sideQuest;

    protected virtual void OnEnable()
    {
        var go = GameObject.Find(transformStr);
        if(go != null)
        {
            transform.position = go.transform.position;
        }

        characterToControl.SetActive(false);

        if (useTransition)
            DoBeginFade();

        Init();
    }

    public void DoBeginFade()
    {
        GameSettings.Inst.controls.Disable();

        if (newMusic != null)
            SoundManager.Inst.PlayMusic(newMusic);
        ScreenFader.Inst.FadeOut(1.0f, () =>
        {
            GameSettings.Inst.DisableCharacter();
            characterToControl.SetActive(true);
            Cursor.visible = false;
            BeginMinigame();
            ScreenFader.Inst.FadeIn(1.0f, () =>
            {
                ProgressQuest(ref startProgress);
                m_hasStarted = true;
                GameSettings.Inst.controls.Enable();
            });
        });
    }

    public void DoEndFade()
    {
        GameSettings.Inst.controls.Disable();

        if (completeMusic != null)
            SoundManager.Inst.PlayMusic(completeMusic);

        ScreenFader.Inst.FadeOut(1.0f, () => 
        {
            GameSettings.Inst.EnableCharacter();
            characterToControl.SetActive(false);
            Cursor.visible = true;
            EndMinigame();
            ScreenFader.Inst.FadeIn(1.0f, () =>
            {
                GameSettings.Inst.controls.Enable();
            });
        });

    }

    protected abstract void Init();
    protected abstract void BeginMinigame();
    protected abstract void EndMinigame();

    public void ProgressQuest(ref int _progress)
    {
        if (sideQuest == null) return;
        if (m_progress >= _progress) return;

        sideQuest.ProgressTo(ref _progress);
        m_progress = _progress;
    }

}
