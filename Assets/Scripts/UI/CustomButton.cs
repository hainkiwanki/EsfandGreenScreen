using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CustomButton : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> m_onClickSFX;
    private Button m_button;
    [SerializeField]
    private UnityEvent onClick;

    private bool hasClicked = false;
    [SerializeField]
    private float resetClickTimer = 0.5f;

    private void Awake()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(() => 
        {
            if (hasClicked) return;
            hasClicked = true;
            SoundManager.Inst.sfxSource.PlayOneShot(m_onClickSFX.GetRandom());
            onClick?.Invoke();

            GameSettings.Inst.ExecuteFunctionWithDelay(resetClickTimer, () =>
            {
                hasClicked = false;
            });
        });
    }
}
