using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum EPopUpType
{
    Normal, Completion, Failure,
}

public class PopUpManager : Singleton<PopUpManager>
{
    private PopUpMessage m_normalPopUp;
    private CompletionMessage m_completionPopUp;

    private Dictionary<EPopUpType, RectTransform> m_currentPopUps = new Dictionary<EPopUpType, RectTransform>();
       
    private bool m_isDeletingCurrent = false;

    private void Awake()
    {
        m_normalPopUp = Resources.Load<PopUpMessage>("Prefabs/PopUpMessage");
        m_completionPopUp = Resources.Load<CompletionMessage>("Prefabs/CompletionPopUp");
    }

    public void CreateCompletionPopUp(string _msg, bool _isSuccess = true, TweenCallback _onStart = null, TweenCallback _onComplete = null)
    {
        var popup = Instantiate(m_completionPopUp, GameSettings.Inst.mainCanvas);
        var duration = (_isSuccess) ? popup.successDuration : popup.failureDuration;
        if (_isSuccess)
            popup.successMessage = _msg;
        else
            popup.failureMessage = _msg;
        EPopUpType type = EPopUpType.Completion;

        if (!m_currentPopUps.ContainsKey(type))
            m_currentPopUps.Add(type, null);

        m_currentPopUps[type] = popup.GetComponent<RectTransform>();
        m_currentPopUps[type].localPosition = new Vector3(0.0f, GameSettings.Inst.ScreenHeight * 0.8f, 0.0f);

        _onStart?.Invoke();

        float volume = SoundManager.Inst.musicSource.volume;
        SoundManager.Inst.musicSource.DOFade(0.1f, 0.2f);

        float time = 0.5f;
        m_currentPopUps[type].DOLocalMoveY(GameSettings.Inst.ScreenHeight * 0.5f - 90.0f, time).SetEase(Ease.OutBack);
        m_currentPopUps[type].DOLocalMoveY(GameSettings.Inst.ScreenHeight * 0.8f, time).SetDelay(duration + time)
        .OnComplete(() =>
        {
            if (m_currentPopUps[type] != null)
            {
                Destroy(m_currentPopUps[type].gameObject);
            }
            m_isDeletingCurrent = false;
            SoundManager.Inst.musicSource.DOFade(volume, 0.2f);
            _onComplete?.Invoke();
        })
        .OnStart(() =>
        {
            m_isDeletingCurrent = true;
        });
    }

    public void CreatePopUpMessage(string _msg, float _time = 3.0f)
    {
        var popup = Instantiate(m_normalPopUp, GameSettings.Inst.mainCanvas);
        popup.text = _msg;
        if (!m_currentPopUps.ContainsKey(EPopUpType.Normal))
            m_currentPopUps.Add(EPopUpType.Normal, null);

        m_currentPopUps[EPopUpType.Normal] = popup.GetComponent<RectTransform>();
        m_currentPopUps[EPopUpType.Normal].localPosition = new Vector3(0.0f, -GameSettings.Inst.ScreenHeight * 0.8f, 0.0f);

        float time = 0.5f;
        m_currentPopUps[EPopUpType.Normal].DOMoveY(48.0f, time).SetEase(Ease.OutBack);
        m_currentPopUps[EPopUpType.Normal].DOLocalMoveY(-GameSettings.Inst.ScreenHeight * 0.5f, time).SetDelay(time + _time)
        .OnComplete(() =>
        {
            if (m_currentPopUps[EPopUpType.Normal] != null)
            {
                Destroy(m_currentPopUps[EPopUpType.Normal].gameObject);
            }
            m_isDeletingCurrent = false;
        })
        .OnStart(() => 
        {
            m_isDeletingCurrent = true;
        });
    }

    public void ForceDestroyCurrentPopUp(EPopUpType _type)
    {
        float time = 0.5f;
        if(m_currentPopUps[_type] != null && !m_isDeletingCurrent)
        {
            m_isDeletingCurrent = true;
            m_currentPopUps[_type].DOLocalMoveY(-GameSettings.Inst.ScreenHeight * 0.5f, time).OnComplete(() =>
            {
                m_isDeletingCurrent = false;
                Destroy(m_currentPopUps[_type].gameObject);
            });
        }
    }
}
