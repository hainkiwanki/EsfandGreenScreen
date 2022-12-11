using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum EVoiceSetting
{
    Default,    // Brian
    Default_Girl, // Amy
    Australian_Guy, // Russell
    Australian_Girl, // Nicole
    Indian_Girl, // Raveena
}

public enum EGameDifficulty
{
    Even_Your_Nan_Can_Do_It,
    Childs_Play,
    Normal,
    MonkaS,
    You_Cannot_Beat_This,
}

public class GameSettings : Singleton<GameSettings>
{
    #region Settings
    private bool m_hasLoadedData = false;

    public EVoiceSetting voiceSetting
    {
        get
        {
            if (!m_hasLoadedData)
                LoadData();
            return m_voiceSetting;
        }
        set { m_voiceSetting = value; }
    }
    private EVoiceSetting m_voiceSetting = EVoiceSetting.Default_Girl;

    public EGameDifficulty gameDifficulty
    {
        get
        {
            if (!m_hasLoadedData)
                LoadData();
            return m_gameDifficulty;
        }
        set { m_gameDifficulty = value; }
    }
    private EGameDifficulty m_gameDifficulty = EGameDifficulty.Normal;

    public float sfxVolume
    {
        get
        {
            if (!m_hasLoadedData) LoadData();
            return m_sfxVolume * 0.5f;
        }
        set { m_sfxVolume = value; }
    }
    private float m_sfxVolume = 0.70f;

    public float musicVolume
    {
        get
        {
            if (!m_hasLoadedData) LoadData();
            return m_musicVolume * 0.5f;
        }
        set { m_musicVolume = value; }
    }
    private float m_musicVolume = 0.35f;

    public float voiceVolume
    {
        get
        {
            if (!m_hasLoadedData) LoadData();
            return m_voiceVolume;
        }
        set { m_voiceVolume = value; }
    }
    private float m_voiceVolume = 1.0f;

    public float mysterySetting
    {
        get
        {
            if (!m_hasLoadedData) LoadData();
            return m_mysterySetting;
        }
        set { m_mysterySetting = value; }
    }
    private float m_mysterySetting = 0.0f;
    #endregion

    #region Player
    public void DisableCharacter()
    {
        if(_characterControl == null)
        {
            _characterControl = FindObjectOfType<CharacterControl>();
        }
        _characterControl.gameObject.SetActive(false);
    }
    public void EnableCharacter()
    {
        if(_characterControl == null)
        {
            _characterControl = FindObjectOfType<CharacterControl>();
        }
        _characterControl.gameObject.SetActive(true);
    }
    private CharacterControl _characterControl;

    public PlayerInput controls
    {
        get
        {
            if(_controls == null)
            {
                _controls = new PlayerInput();
            }

            return _controls;
        }
    }
    private PlayerInput _controls;
    #endregion

    #region CanvasAndScreen
    public RectTransform mainCanvas
    {
        get
        {
            if(_mainCanvas == null)
            {
                var cObj = GameObject.FindGameObjectWithTag("MainCanvas");
                if (cObj == null)
                    Debug.LogError("No main canvas in scene");
                else
                    _mainCanvas = cObj.GetComponent<RectTransform>();
            }

            return _mainCanvas;
        }
    }
    private RectTransform _mainCanvas;

    public ChatBox chatBox
    {
        get
        {
            if(_chatBox == null)
            {
                _chatBox = Instantiate(Resources.Load<ChatBox>("Prefabs/ChatBox"), mainCanvas);
                _chatBox.InstantHide();
            }
            return _chatBox;
        }
    }
    private ChatBox _chatBox;

    public float ScreenHeight => 1080.0f;
    public float ScreenWidth => 1920.0f;
    #endregion

    private void Awake()
    {
        LoadData();
        DontDestroyOnLoad(gameObject);
    }

    private void LoadData()
    {
        string voiceSettingStr = "voice_settings";
        if(PlayerPrefs.HasKey(voiceSettingStr))
            m_voiceSetting = (EVoiceSetting)PlayerPrefs.GetInt(voiceSettingStr);
        string difficultyStr = "game_difficulty";
        if(PlayerPrefs.HasKey(difficultyStr))
            m_gameDifficulty = (EGameDifficulty)PlayerPrefs.GetInt(difficultyStr);
        string sfxVolumeStr = "sfx_volume";
        if(PlayerPrefs.HasKey(sfxVolumeStr))
            m_sfxVolume = PlayerPrefs.GetFloat(sfxVolumeStr);
        string musicVolumeStr = "music_volume";
        if(PlayerPrefs.HasKey(musicVolumeStr))
            m_musicVolume = PlayerPrefs.GetFloat(musicVolumeStr);
        string mysterySettingStr = "mystery_setting";
        if(PlayerPrefs.HasKey(mysterySettingStr))
            m_mysterySetting = PlayerPrefs.GetFloat(mysterySettingStr);
        string voiceVolumeStr = "voice_volume";
        if (PlayerPrefs.HasKey(voiceVolumeStr))
            m_voiceVolume = PlayerPrefs.GetFloat(voiceVolumeStr);

        m_hasLoadedData = true;
    }

    private void SaveData()
    {
        string voiceSettingStr = "voice_settings";
        PlayerPrefs.SetInt(voiceSettingStr, (int)m_voiceSetting);
        string difficultyStr = "game_difficulty";
        PlayerPrefs.SetInt(difficultyStr, (int)m_gameDifficulty);
        string sfxVolumeStr = "sfx_volume";
        PlayerPrefs.SetFloat(sfxVolumeStr, m_sfxVolume);
        string musicVolumeStr = "music_volume";
        PlayerPrefs.SetFloat(musicVolumeStr, m_musicVolume);
        string mysterySettingStr = "mystery_setting";
        PlayerPrefs.SetFloat(mysterySettingStr, m_mysterySetting);
        string voiceVolumeStr = "voice_volume";
        PlayerPrefs.SetFloat(voiceVolumeStr, m_voiceVolume);
    }

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += _ =>
        {
            DOTween.KillAll();
            if(_controls != null)
            {
                _controls.Dispose();
                _controls = null;
            }
        };
        SceneManager.sceneLoaded += (Scene _s, LoadSceneMode _m) =>
        {
            controls.Enable();
        };
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnDestroy()
    {
        SaveData();
    }

    public void ShowMessageForSecond(string _msg, float _duration)
    {
        chatBox.Show();
        chatBox.TurnToText();
        chatBox.message = _msg;
        ExecuteFunctionWithDelay(_duration, () => 
        { 
            chatBox.Hide(); 
        });
    }

    public void ExecuteFunctionWithDelay(float _delay, UnityAction _function)
    {
        StartCoroutine(ExecuteAfterSeconds(_delay, _function));
    }

    IEnumerator ExecuteAfterSeconds(float _t, UnityAction _executeAfter)
    {
        yield return new WaitForSeconds(_t);
        _executeAfter?.Invoke();
    }
}
