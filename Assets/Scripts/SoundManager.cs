using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Nito.Collections;
using UnityEngine.Events;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource sfxSource
    {
        get
        {
            if (m_sfxAudioSource == null)
                m_sfxAudioSource = gameObject.AddComponent<AudioSource>();
            return m_sfxAudioSource;
        }
    }

    public AudioSource musicSource
    {
        get
        {
            if (m_musicAudioSource == null)
                m_musicAudioSource = gameObject.AddComponent<AudioSource>();
            return m_musicAudioSource;
        }
    }

    public AudioSource voiceSource
    {
        get
        {
            if (m_voiceAudioSource == null)
                m_voiceAudioSource = gameObject.AddComponent<AudioSource>();
            return m_voiceAudioSource;
        }
    }

    private AudioSource m_sfxAudioSource, m_musicAudioSource, m_voiceAudioSource, m_secondSfxAudioSource;
    private string m_currentSongPlaying = "";

    private void Awake()
    {
        m_sfxAudioSource = gameObject.AddComponent<AudioSource>();
        m_musicAudioSource = gameObject.AddComponent<AudioSource>();
        m_secondSfxAudioSource = gameObject.AddComponent<AudioSource>();
        m_voiceAudioSource = gameObject.AddComponent<AudioSource>();

        m_sfxAudioSource.volume = GameSettings.Inst.sfxVolume;
        m_musicAudioSource.volume = GameSettings.Inst.musicVolume;
        m_voiceAudioSource.volume = GameSettings.Inst.voiceVolume;
    }

    public void PlaySoundEffect(AudioClip _clip, float _scale = 1.0f, UnityAction _onComplete = null)
    {
        m_sfxAudioSource.PlayOneShot(_clip, GameSettings.Inst.sfxVolume * _scale);
        if(_onComplete != null)
            GameSettings.Inst.ExecuteFunctionWithDelay(_clip.length, _onComplete);
    }

    public void PlayPrioritizedSoundEffect(AudioClip _clip, float _scale)
    {
        float volumeMusic = m_musicAudioSource.volume;
        m_musicAudioSource.volume = 0.0f;

        m_sfxAudioSource.PlayOneShot(_clip, GameSettings.Inst.sfxVolume * _scale);
        GameSettings.Inst.ExecuteFunctionWithDelay(_clip.length, () =>
        {
            m_musicAudioSource.DOFade(volumeMusic, 1.0f);
        });
    }

    public void PlaySubbedVoiceLine(TextAudio _textAudio, float _scale = 1.0f)
    {
        GameSettings.Inst.ShowMessageForSecond(_textAudio.text, _textAudio.audio.length);
        PlayVoiceLine(_textAudio.audio, false, _scale);
    }

    public void PlaySubbedVoiceLine(SubbedAudio _subAudio, float _scale = 1.0f, float _extraDelay = 0.0f, UnityAction _onComplete = null)
    {
        TextPerClip tpc = _subAudio.GetTPC();
        GameSettings.Inst.ShowMessageForSecond(tpc.text, tpc.clip.length + _extraDelay);
        PlayVoiceLine(tpc.clip, false, _scale);
        if(_onComplete != null)
            GameSettings.Inst.ExecuteFunctionWithDelay(tpc.clip.length + _extraDelay, _onComplete);
    }

    public void PlayVoiceLine(AudioClip _audio, bool _interrupt = false, float _scale = 1.0f)
    {
        if (_interrupt && m_voiceAudioSource.isPlaying)
            m_voiceAudioSource.Stop();
        m_voiceAudioSource.PlayOneShot(_audio, GameSettings.Inst.voiceVolume * _scale);
    }
    
    public void PlayMusic(AudioClip _clip, float _fade = 1.0f, float _volumeScale = 1.0f)
    {
        if (m_currentSongPlaying == _clip.name)
            return;

        m_musicAudioSource.loop = true;
        if (m_currentSongPlaying != "")
        {
            m_musicAudioSource.DOFade(0.0f, _fade).OnComplete(() => 
            {
                m_musicAudioSource.clip = _clip;
                m_musicAudioSource.Play();
                m_musicAudioSource.DOFade(GameSettings.Inst.musicVolume * _volumeScale, _fade);
            });
        }
        else
        {
            m_musicAudioSource.clip = _clip;
            m_musicAudioSource.Play();
            m_musicAudioSource.DOFade(GameSettings.Inst.musicVolume * _volumeScale, _fade);
        }
        m_currentSongPlaying = _clip.name;
    }
    public void PlayRequestedSoundStoppable(AudioClip _clip, float _scale = 1.0f)
    {
        m_secondSfxAudioSource.clip = _clip;
        m_secondSfxAudioSource.Play();
        m_secondSfxAudioSource.volume = GameSettings.Inst.sfxVolume * _scale;
    }
    public void StopSfx()
    {
        if(m_secondSfxAudioSource.isPlaying)
        {
            m_secondSfxAudioSource.Stop();
            m_secondSfxAudioSource.clip = null;
        }
    }
    public void FadeOutMusic(float _time, TweenCallback _onComplete = null)
    {
        m_musicAudioSource.DOFade(0.0f, _time).OnComplete(_onComplete);
    }


    float m_originalVolume = 0.0f;
    public void FadeOutMusic()
    {
        m_originalVolume = m_musicAudioSource.volume;
        m_musicAudioSource.DOFade(0.0f, 0.5f);
    }

    public void FadeInMusic()
    {
        m_musicAudioSource.DOFade(m_originalVolume, 0.5f);
    }

    private AudioClip m_questComplete;
    public void QComplete()
    {
        if (m_questComplete == null)
            m_questComplete = Resources.Load<AudioClip>("Sound/QuestComplete");
        m_sfxAudioSource.PlayOneShot(m_questComplete);
    }
}
