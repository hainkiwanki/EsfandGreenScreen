using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Interactable : SerializedMonoBehaviour
{
    private InteractIndicator m_indicator;
    private float m_indicatorTime = 0.2f;
    [SerializeField]
    protected float m_cooldown = 1.0f;
    private float m_timer;
    public EGameProgression minimumProgressRequirement = EGameProgression.Nothing;
    protected bool m_isHidden = true;
    [SerializeField]
    protected int m_amountUsed = 0;

    public bool useForSidequest = false;
    [ShowIfGroup("useForSidequest")]
    public int progress = 0;
    [ShowIfGroup("useForSidequest")]
    public QuestLineGraph sideQuest;

    public void ShowInteractionKey()
    {
        if ((int)ProgressTracker.Inst.currentProgress >= (int)minimumProgressRequirement)
        {            
            m_isHidden = false;
            m_indicator.transform.DOScale(Vector3.one, m_indicatorTime).SetEase(Ease.OutBack);
        }
    }

    public void HideInteractionKey()
    {
        m_isHidden = true;
        m_indicator.transform.DOScale(Vector3.zero, m_indicatorTime).SetEase(Ease.InBack);
    }

    public void Use()
    {
        if (m_timer <= 0.0f && !m_isHidden)
        {
            m_amountUsed += 1;
            OnUse();
            m_indicator.Activate(m_cooldown);
            m_timer = m_cooldown;
        }
    }

    protected virtual void ProgressSideQuest(ref int _progress)
    {
        if (progress >= _progress) return;

        sideQuest.ProgressTo(ref _progress);
        progress = _progress;
    }

    protected virtual void OnUse() { }

    protected virtual void Update()
    {
        if (m_timer > 0.0f)
        {
            m_timer -= Time.deltaTime;
        }
    }

    private void Awake()
    {
        Transform parent = transform.Find("InteractLocation");
        m_indicator = Instantiate(Resources.Load<InteractIndicator>("Prefabs/InteractIndicator"), parent);
        m_indicator.transform.localScale = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            var control = collision.GetComponent<CharacterControl>();
            control.ChangeInteractable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            var control = collision.GetComponent<CharacterControl>();
            control.ChangeInteractable(null);
        }
    }
}
