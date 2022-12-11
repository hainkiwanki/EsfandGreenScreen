using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;

public class MenuController : SceneController
{
    [SerializeField]
    private List<RectTransform> m_esfandLetters = new List<RectTransform>();
    private List<Vector3> m_letterOgPositions = new List<Vector3>();
    [SerializeField]
    private Image m_letterBorderImage;
    private Camera m_cam;
    [SerializeField]
    private RectTransform m_bg;
    [SerializeField]
    private RectTransform m_greyFlash;
    [SerializeField]
    private RectTransform m_orangeFlash;
    [SerializeField]
    private Image m_logoImage;
    [SerializeField]
    private Animator m_adventuresAnim;
    [SerializeField]
    private RectTransform m_logoTextOnlyImage;
    [SerializeField]
    private RectTransform m_logoGO;
    [SerializeField]
    private TextMeshProUGUI m_continueMessage;
    [SerializeField]
    private RectTransform m_optionsContainer;

    [SerializeField]
    private RectTransform m_buttonTransform;

    private bool m_animationIsFinished = false;
    private bool m_menuShowing = false;

    [SerializeField]
    private GameObject m_particles;

    [SerializeField]
    private List<CustomButton> m_buttons = new List<CustomButton>();

    public OptionController difficulty, voicesetting, sfxvolume, musicvolume, mystery, voicevolume;

    [SerializeField]
    private TextAudio m_voiceChangeAudio;

    [Header("Sounds")]
    public AudioClip m_adventuresSfx;
    public AudioClip m_titleFire;
    public AudioClip m_titleHit;
    public List<AudioClip> m_letterImpact;

    private bool m_notPressedCorrectKey = true;
    private char m_currentDisplayedCharacter;

    protected override void Awake()
    {
        base.Awake();

        PrepareMenu();
        GameSettings.Inst.controls.Player.LeftClick.performed += _ => HasLeftClicked();
        GameSettings.Inst.controls.Player.Any.performed += _ => HasContinued();
        Keyboard.current.onTextInput += PressedAKey;
        HookUpSettings();
    }

    private void OnDisable()
    {
        Keyboard.current.onTextInput -= PressedAKey;
    }

    private void HookUpSettings()
    {
        difficulty.onSlideIntChanged += (int _value) =>
        {
            GameSettings.Inst.gameDifficulty = (EGameDifficulty)_value;
        };
        voicesetting.onSlideIntChanged += (int _value) =>
        {
            GameSettings.Inst.voiceSetting = (EVoiceSetting)_value;
        };
        sfxvolume.onSlideFloatChanged += (float _value) =>
        {
            GameSettings.Inst.sfxVolume = _value;
            SoundManager.Inst.sfxSource.volume = GameSettings.Inst.sfxVolume;
        };
        musicvolume.onSlideFloatChanged += (float _value) =>
        {
            GameSettings.Inst.musicVolume = _value;
            SoundManager.Inst.musicSource.volume = GameSettings.Inst.musicVolume;
        };
        mystery.onSlideFloatChanged += (float _value) =>
        {
            GameSettings.Inst.mysterySetting = _value;
        };
        voicevolume.onSlideFloatChanged += (float _value) =>
        {
            GameSettings.Inst.voiceVolume = _value;
            SoundManager.Inst.voiceSource.volume = GameSettings.Inst.voiceVolume;
        };

        difficulty.SetValue((int)GameSettings.Inst.gameDifficulty);
        voicesetting.SetValue((int)GameSettings.Inst.voiceSetting);
        sfxvolume.SetValue(GameSettings.Inst.sfxVolume * 2.0f);
        musicvolume.SetValue(GameSettings.Inst.musicVolume * 2.0f);
        mystery.SetValue(GameSettings.Inst.mysterySetting);
        voicevolume.SetValue(GameSettings.Inst.voiceVolume);

        if(PlayerPrefs.HasKey("hideIndex"))
        {
            voicesetting.hideIndex = 3;
        }
    }

    private void PrepareMenu()
    {
        for(int i = 0; i < m_esfandLetters.Count; i++)
        {
            m_letterOgPositions.Add(m_esfandLetters[i].position);
            m_esfandLetters[i].position += Vector3.right * (Screen.width * 0.5f);
        }
        m_adventuresAnim.gameObject.SetActive(false);
        m_logoImage.fillAmount = 0.0f;
        m_letterBorderImage.color = new Color(1, 1, 1, 0);
        m_logoTextOnlyImage.gameObject.SetActive(false);
        m_optionsContainer.localScale = Vector3.zero;
        m_optionsContainer.gameObject.SetActive(true);
    }

    public void OnSpeakButton()
    {
        SoundManager.Inst.PlayVoiceLine(m_voiceChangeAudio.audio, true);
        if(GameSettings.Inst.voiceSetting == EVoiceSetting.Indian_Girl)
        {
            voicesetting.hideIndex = 3;
            PlayerPrefs.SetInt("hideIndex", 3);
            GameSettings.Inst.voiceSetting = (EVoiceSetting)3;
        }
    }

    private void HasLeftClicked()
    {
        if(!m_animationIsFinished)
        {
            DOTween.KillAll();

            for(int i = 0; i < m_esfandLetters.Count; i++)
            {
                m_esfandLetters[i].position = m_letterOgPositions[i];
            }
            m_letterBorderImage.DOFade(1.0f, 0.0f);
            m_logoImage.fillAmount = 1.0f;
            m_animationIsFinished = true;
            m_adventuresAnim.gameObject.SetActive(true);
            m_adventuresAnim.speed = 100.0f;
            m_logoTextOnlyImage.gameObject.SetActive(true);
            m_continueMessage.DOFade(1.0f, 0.5f).OnComplete(() =>
            {
                StartCoroutine(RandomKey());
                m_continueMessage.DOFade(0.5f, 1.0f).SetLoops(-1, LoopType.Yoyo);
            });
            ScreenFader.Inst.Force(0.0f);
            SoundManager.Inst.PlayMusic(sceneMusic);
        }
    }

    private void PressedAKey(char obj)
    {
        if (m_currentDisplayedCharacter == obj)
        {
            m_notPressedCorrectKey = false;
            HasContinued();
        }
    }

    IEnumerator RandomKey()
    {
        List<char> charList = new List<char>()
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z',
        };
        while (m_notPressedCorrectKey)
        {
            m_currentDisplayedCharacter = charList.GetRandom();
            string c = m_currentDisplayedCharacter.ToString().ToUpper();
            // m_continueMessage.text = "Press the [" + c + "] key to continue";
            // m_continueMessage.text = "Press any key to continue";
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void HasContinued()
    {
        if(m_animationIsFinished && !m_menuShowing)
        {
            float time = 1.5f;
            m_logoGO.DOLocalMoveY(380.0f, time);
            m_logoImage.DOFillAmount(0.0f, time);
            m_menuShowing = true;
            m_continueMessage.gameObject.SetActive(false);
            m_buttonTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutQuint).SetDelay(time * 0.5f);
        }
    }

    private void OnCompleteFade()
    {
        float time = 1.0f;
        for(int i = 0; i < m_esfandLetters.Count; i++)
        {
            if (i == m_esfandLetters.Count - 1)
            {
                m_esfandLetters[i].DOMoveX(m_letterOgPositions[i].x, time, true).SetEase(Ease.InQuart).SetDelay(i * time * 0.5f).OnComplete(OnCompleteLetterAnimation);
            }
            else
            {
                m_esfandLetters[i].DOMoveX(m_letterOgPositions[i].x, time, true).SetEase(Ease.InQuart).SetDelay(i * time * 0.5f).OnComplete(() => 
                {
                    SoundManager.Inst.sfxSource.PlayOneShot(m_letterImpact.GetRandom(), 0.75f);
                    m_bg.DOShakePosition(0.1f, 3, 20);
                });
            }
        }
    }

    private void OnCompleteLetterAnimation()
    {
        SoundManager.Inst.sfxSource.PlayOneShot(m_letterImpact.GetRandom(), 0.75f);
        m_bg.DOShakePosition(0.1f, 6, 40);
        float time = 0.25f;
        m_greyFlash.DOSizeDelta(new Vector2(1200.0f, 926.0f), time, true).SetEase(Ease.InQuart).OnComplete(() =>
        {
            SoundManager.Inst.sfxSource.PlayOneShot(m_titleHit);
            m_greyFlash.DOSizeDelta(Vector2.zero, 0.001f);
            m_bg.DOShakePosition(0.1f, 7, 50);
            m_letterBorderImage.DOFade(1.0f, time).OnComplete(OnCompleteGreyBorder);
        });
    }

    private void OnCompleteGreyBorder()
    {
        SoundManager.Inst.sfxSource.PlayOneShot(m_titleFire);
        float endPos = m_orangeFlash.localPosition.y + 300.0f;
        float flashTime = 0.25f;
        m_orangeFlash.DOLocalMoveY(endPos, flashTime, true).SetEase(Ease.InQuart);
        m_logoImage.DOFillAmount(1.0f, flashTime * 1.0f).SetEase(Ease.InQuart).OnComplete(() => 
        {
            m_adventuresAnim.gameObject.SetActive(true);
            SoundManager.Inst.PlaySoundEffect(m_adventuresSfx, 1.5f);
            m_logoTextOnlyImage.gameObject.SetActive(true);
            StartCoroutine(CompleteAnimationWithDelay(2.0f));
        });
    }

    private IEnumerator CompleteAnimationWithDelay(float _time)
    {
        yield return new WaitForSeconds(_time);
        m_animationIsFinished = true;
        StartCoroutine(RandomKey());
        m_continueMessage.DOFade(1.0f, 0.5f).OnComplete(() =>
        {
            m_continueMessage.DOFade(0.5f, 1.0f).SetLoops(-1, LoopType.Yoyo);
        });
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnNewGameClick()
    {
        m_adventuresAnim.gameObject.SetActive(false);
        m_particles.SetActive(false);
        FadeOutScene("Bathroom");
        PlayerPrefs.DeleteKey("game_progress");
    }

    public void OnOptionsClick()
    {
        m_buttonTransform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InQuint).OnComplete(() => 
        { 
            m_optionsContainer.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuint);
        });
    }

    public void OnBackClick()
    {
        m_optionsContainer.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InQuint).OnComplete(() =>
        {
            m_buttonTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuint);
        });
        if(GameSettings.Inst.voiceSetting == EVoiceSetting.Indian_Girl)
        {
            GameSettings.Inst.voiceSetting = EVoiceSetting.Default;
        }
    }

    protected override void OnFinishedFading()
    {
        if(!m_animationIsFinished)
            OnCompleteFade();
    }
}
