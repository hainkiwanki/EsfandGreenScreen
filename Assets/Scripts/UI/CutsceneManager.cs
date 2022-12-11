using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutsceneManager : Singleton<CutsceneManager>
{
    private CutscenePlayer m_playerPrefab;
    private CutscenePlayer m_currentPlayer;
    private UnityAction m_onCompleteCallback;
    private SkipIndicator m_skipIndicator;

    private float m_heldSpacebar = 0.0f;
    private bool m_isHoldingSpace = false;
    private float m_skipTime = 1.5f;

    private PlayerInput inputs;

    private void Awake()
    {
        m_playerPrefab = Resources.Load<CutscenePlayer>("Prefabs/CutScenePlayer");
        m_skipIndicator = Instantiate(Resources.Load<SkipIndicator>("Prefabs/SkipIcon"), GameSettings.Inst.mainCanvas);
        inputs = new PlayerInput();
        inputs.Player.SkipCutscene.performed += _ => SpacebarDown();
        inputs.Player.SkipCutscene.canceled += _ => SpacebarUp();
    }

    private void OnEnable()
    {
        inputs.Enable();   
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Update()
    {
        if(m_currentPlayer != null && m_heldSpacebar < m_skipTime)
        {
            if(m_currentPlayer.IsDonePlaying())
            {
                OnCutsceneFinished();
            }
        }

        if (m_currentPlayer == null)
            return;

        if(m_isHoldingSpace)
        {
            m_heldSpacebar += Time.deltaTime;
            m_skipIndicator.SetTime(m_heldSpacebar / m_skipTime);
        }

        if(m_heldSpacebar >= m_skipTime)
        {
            OnCutsceneFinished();
            SpacebarUp();           
        }
    }

    private void OnCutsceneFinished()
    {
        Destroy(m_currentPlayer.gameObject);
        ScreenFader.Inst.FadeIn(3.0f);
        GameSettings.Inst.controls.Enable();
        m_onCompleteCallback?.Invoke();
        Destroy(gameObject);
    }

    private void SpacebarDown()
    {
        m_isHoldingSpace = true;
        m_heldSpacebar = 0.0f;
    }

    private void SpacebarUp()
    {
        m_isHoldingSpace = false;
        m_heldSpacebar = 0.0f;
        m_skipIndicator.Hide();
    }

    public void PlayCutscene(VideoClip _clip, UnityAction _onComplete = null)
    {
        GameSettings.Inst.controls.Disable();
        if (_clip != null)
        {
            m_onCompleteCallback = _onComplete;
            m_currentPlayer = Instantiate(m_playerPrefab, GameSettings.Inst.mainCanvas);
            m_currentPlayer.PlayClip(_clip);
            m_currentPlayer.transform.SetAsLastSibling();
            m_skipIndicator.transform.SetAsLastSibling();
        }
    }
}
