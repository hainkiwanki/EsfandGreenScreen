using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkipIndicator : MonoBehaviour
{
    public Image backgroundFill, borderFill;

    private bool m_isHidden = true;

    private void Awake()
    {
        backgroundFill.fillAmount = 0.0f;
        borderFill.fillAmount = 0.0f;
        transform.localScale = Vector3.zero;
    }

    public void SetTime(float _t)
    {
        if(m_isHidden)
        {
            m_isHidden = false;
            transform.DOScale(1.0f, 0.1f);
        }

        backgroundFill.fillAmount = _t;
        borderFill.fillAmount = _t;
    }

    public void Hide()
    {
        m_isHidden = true;
        transform.DOScale(0.0f, 0.1f);
        backgroundFill.fillAmount = 0.0f;
        borderFill.fillAmount = 0.0f;
    }
}
