using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class RewardAnimation : MonoBehaviour
{
    public RectTransform container;
    public RectTransform rays;
    public RectTransform reward;
    private bool m_doRotation = false;
    public float rotSpeed = 15.0f;
    public float rayRadiateTime = 3.0f;
    public float rewardRotTime = 5.0f;
    private bool m_canDelete = false;

    public AudioClip rewardOpen, rewardClose, rewardGet;
    [HideInInspector]
    public UnityAction onRewardClose;
    private PlayerInput m_input;

    private void Awake()
    {
        m_input = new PlayerInput();
        m_input.Player.LeftClick.performed += _ => CloseReward();
    }

    private void OnEnable()
    {
        m_input.Enable();
    }

    private void Update()
    {
        if (m_doRotation)
            rays.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
    }

    public void SetReward(RectTransform _reward)
    {
        container.localScale = Vector3.zero;
        _reward.parent = reward;
        _reward.localPosition = Vector3.zero;
        _reward.localScale = Vector3.one;

        SoundManager.Inst.PlayPrioritizedSoundEffect(rewardOpen, 2.0f);
        SoundManager.Inst.PlayPrioritizedSoundEffect(rewardGet, 2.0f);
        container.DOScale(1.0f, rewardOpen.length).SetEase(Ease.OutBack).OnComplete(() =>
        {
            m_canDelete = true;
            m_doRotation = true;
            rays.DOScale(1.2f, rayRadiateTime).SetLoops(-1, LoopType.Yoyo);
            reward.DOLocalRotate(new Vector3(0, 0, -2.0f), rewardRotTime).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        });
    }

    private void CloseReward()
    {
        if (!m_canDelete)
            return;

        SoundManager.Inst.PlayPrioritizedSoundEffect(rewardClose, 2.0f);
        onRewardClose?.Invoke();
        m_input.Disable();
        DOTween.Kill(gameObject, false);
        m_doRotation = false;
        container.DOScale(0.0f, rewardClose.length).SetEase(Ease.InBack).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
