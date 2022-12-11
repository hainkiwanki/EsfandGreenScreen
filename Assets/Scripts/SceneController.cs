using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneController : SerializedMonoBehaviour
{
    [BoxGroup("Basic")]
    public float volumeScale = 1.0f;
    [BoxGroup("Basic")]
    public float fadeTime = 3.0f;
    [BoxGroup("Basic")]
    public AudioClip sceneMusic;
    [BoxGroup("Basic")]
    public VideoClip cutscene;
    [BoxGroup("Basic")]
    public EGameProgression progressToOnLoad = EGameProgression.Nothing;
    public QuestLineGraph questline;
    private EGameProgression currentProgress = EGameProgression.Nothing;

    protected virtual void Awake()
    {
        ProgressTracker.onProgressChanged += OnProgressChanged;
        var box = GameSettings.Inst.chatBox; // will create chatbox
        if (cutscene != null)
            CutsceneManager.Inst.PlayCutscene(cutscene, () =>
            {
                ProgressTracker.Inst.currentProgress = progressToOnLoad;
                FadeInScene();
            });
        else
        {
            ProgressTracker.Inst.currentProgress = progressToOnLoad;
            FadeInScene();
        }
    }

    protected void FadeInScene()
    {
        ScreenFader.Inst.FadeIn(fadeTime);
        if(sceneMusic != null)
            SoundManager.Inst.PlayMusic(sceneMusic, fadeTime, volumeScale);
        OnSceneLoad();

        GameSettings.Inst.ExecuteFunctionWithDelay(fadeTime, OnFinishedFading);
    }

    protected void FadeOutScene(string _newScene)
    {
        ScreenFader.Inst.FadeOut(fadeTime);
        SoundManager.Inst.FadeOutMusic(fadeTime);
        OnSceneUnload();

        GameSettings.Inst.ExecuteFunctionWithDelay(fadeTime, () =>
        {
            SceneManager.LoadScene(_newScene, LoadSceneMode.Single);
        });
    }

    [Button]
    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnProgressChanged(EGameProgression _progression)
    {
        if (questline == null)
            return;

        if (_progression <= currentProgress) return;

        int p = (int)_progression;
        questline.ProgressTo(ref p);
        currentProgress = (EGameProgression)p;
    }

    private void OnDisable()
    {
        ProgressTracker.onProgressChanged -= OnProgressChanged;
    }

    protected virtual void OnSceneLoad() { }
    protected virtual void OnSceneUnload() { }
    protected virtual void OnFinishedFading() { }
}
