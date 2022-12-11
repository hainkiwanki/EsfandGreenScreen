using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InteractIndicator : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_buttonRenderer;
    [SerializeField]
    private SpriteRenderer m_glowRenderer;
    [SerializeField]
    private SpriteRenderer m_buttonGrowRenderer;
    [SerializeField]
    private Image m_cooldownImage;
    [SerializeField]
    private AudioClip m_popSound;

    private bool m_isAnimating = false;

    private void OnEnable()
    {
        float glowSize = 1.18f;
        m_glowRenderer.transform.DOScale(Vector3.one * glowSize, 1.0f).SetLoops(-1, LoopType.Yoyo);
    }

    public void Activate(float _time = 0.0f)
    {
        if (m_isAnimating)
            return;

        m_isAnimating = true;
        SoundManager.Inst.PlaySoundEffect(m_popSound);
        m_buttonGrowRenderer.gameObject.SetActive(true);
        m_buttonGrowRenderer.transform.DOScale(Vector3.one * 1.5f, 0.2f);
        m_buttonGrowRenderer.DOFade(0.0f, 0.2f);
        m_cooldownImage.fillAmount = 1.0f;
        m_cooldownImage.DOFillAmount(0.0f, _time).OnComplete(() => 
        {
            m_buttonGrowRenderer.gameObject.SetActive(false);
            m_buttonGrowRenderer.transform.DOScale(Vector3.one, 0.0f);
            m_buttonGrowRenderer.DOFade(0.5f, 0.0f);
            m_isAnimating = false;
        });
    }
}
