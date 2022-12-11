using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameProgression
{
    Nothing = 0,
    Bathroom_Cutscene,
    Bathroom_Intro,
    Bathroom_Mirror,
    Bedroom_Intro,
    Error_Intro,
    Error_WhaleAppear,
    Finale,
}


public class ProgressTracker : Singleton<ProgressTracker>
{

    public delegate void OnProgressChanged(EGameProgression _currentProgress);
    public static OnProgressChanged onProgressChanged;
    private string m_progressString = "game_progress";

    private Dictionary<EGameProgression, bool> progressionTracker = new Dictionary<EGameProgression, bool>();

    public EGameProgression currentProgress
    {
        get
        {
            if (PlayerPrefs.HasKey(m_progressString))
                m_currentProgress = (EGameProgression)PlayerPrefs.GetInt(m_progressString);
            return m_currentProgress;
        }
        set
        {
            if(m_currentProgress != value && (int)m_currentProgress < (int)value)
            {
                m_currentProgress = value;
                onProgressChanged?.Invoke(m_currentProgress);
                PlayerPrefs.SetInt(m_progressString, (int)m_currentProgress);
            }
        }
    }
    private EGameProgression m_currentProgress = EGameProgression.Nothing;

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey(m_progressString);
    }
}
