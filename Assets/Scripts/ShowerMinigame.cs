using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using TMPro;

public class ShowerMinigame : MonoBehaviour
{
    [SerializeField]
    private Animator m_anim;
    [SerializeField]
    private GameObject m_esfandShoweringObj;
    [SerializeField]
    private Image m_washProgressImage;
    [SerializeField]
    private RectTransform m_uiContainer;
    [SerializeField]
    private GameObject m_completeIndicator;
    [SerializeField]
    private ActionInteractable m_actionInteractable;
    [SerializeField]
    private TextMeshProUGUI m_amountDone;

    private float m_totalWash = 0.0f;
    private float m_maxWash = 100.0f;
    private float m_washPerTap = 5.0f;
    private float m_washDecrement = 5.0f;

    private bool m_isCompleted = false;
    private bool m_hasStarted = false;

    private int m_timesCleaned = 0;

    [SerializeField]
    private AudioClip m_waterRunning;

    private void Awake()
    {
        m_amountDone.text = m_timesCleaned.ToString();
        m_esfandShoweringObj.SetActive(false);
        GameSettings.Inst.controls.Player.Spacebar.performed += _ => Wash();
        GameSettings.Inst.controls.Player.Esc.performed += _ => End(true);
        GameSettings.Inst.controls.Player.CheatKey.performed += _ => CheatWash();
    }

    private void OnDestroy()
    {
        GameSettings.Inst.controls.Player.Spacebar.performed -= _ => Wash();
        GameSettings.Inst.controls.Player.Esc.performed -= _ => End(true);
        GameSettings.Inst.controls.Player.CheatKey.performed -= _ => CheatWash();
    }

    public void Begin()
    {
        m_hasStarted = true;
        InitInterface();
        GameSettings.Inst.DisableCharacter();
        PopUpManager.Inst.CreatePopUpMessage("Spam space", 5.0f);
        m_isCompleted = false;
        SoundManager.Inst.PlayRequestedSoundStoppable(m_waterRunning);
    }

    public void InitInterface()
    {
        float time = 0.2f;
        m_uiContainer.DOLocalMoveX(0.0f, time).SetEase(Ease.OutBack);
        m_esfandShoweringObj.SetActive(true);
        m_completeIndicator.SetActive(false);
        m_washProgressImage.fillAmount = m_totalWash = 0.0f;
    }

    private void CheatWash()
    {
#if UNITY_EDITOR
        m_totalWash = m_maxWash * 2.0f;
        Wash();
#endif
    }

    public void Wash()
    {
        if (m_isCompleted)
            return;

        m_totalWash += m_washPerTap;

        if (m_totalWash >= m_maxWash)
        {
            m_completeIndicator.SetActive(true);
            m_anim.SetBool("isShowering", false);
            PopUpManager.Inst.CreateCompletionPopUp("Completed", true, () => { End(); });
            m_isCompleted = true;
            m_timesCleaned++;
            m_amountDone.text = m_timesCleaned.ToString();
            m_actionInteractable.ProgressToNext();
        }
        else
        {
            m_anim.SetBool("isShowering", true);
        }
    }

    private void Update()
    {
        if (!m_hasStarted)
            return;

        if (m_totalWash < m_maxWash)
        {
            m_totalWash -= (m_washDecrement * ((m_timesCleaned + 1) * 0.75f)) * Time.deltaTime;
            if (m_totalWash < 0.0f)
            {
                m_totalWash = 0.0f;
                m_anim.SetBool("isShowering", false);
            }
        }
        m_washProgressImage.fillAmount = m_totalWash / m_maxWash;
    }

    public void End(bool _forced = false)
    {
        if (_forced && m_isCompleted)
            return;

        SoundManager.Inst.StopSfx();
        m_hasStarted = false;
        float time = 0.2f;
        m_uiContainer.DOLocalMoveX(300.0f, time).SetEase(Ease.InBack);
        m_esfandShoweringObj.SetActive(false);
        GameSettings.Inst.EnableCharacter();

        if (_forced)
        {
            m_actionInteractable.DecreaseAmount();
            PopUpManager.Inst.CreateCompletionPopUp("Failed", false);
            PopUpManager.Inst.ForceDestroyCurrentPopUp(EPopUpType.Normal);
        }
        else
        {
            PopUpManager.Inst.ForceDestroyCurrentPopUp(EPopUpType.Normal);
        }
    }
}
