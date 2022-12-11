using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public struct TextPerClip
{
    public string text;
    public AudioClip clip;

    public void SetText(string _txt)
    {
        text = _txt;
    }
}
[CreateAssetMenu(fileName = "text aduio", menuName = "Dialogue/TextAudio")]
public class TextAudio : SerializedScriptableObject
{
    public float delayBefore;
    public float delayAfter;
    public Dictionary<EVoiceSetting, TextPerClip> clipPerVoice = new Dictionary<EVoiceSetting, TextPerClip>();

    public EVoiceSetting voiceSetting => GameSettings.Inst.voiceSetting;
    public TextPerClip current => clipPerVoice[voiceSetting];

    public float duration
    {
        get
        {
            return current.clip.length;
        }
    }

    public string text
    {
        get
        {
            return current.text;
        }
        set
        {
            current.SetText(value);
        }
    }

    public AudioClip audio
    {
        get
        {
            return current.clip;
        }
    }

    public TextPerClip GetAudioByType(EVoiceSetting _type)
    {
        return clipPerVoice[_type];
    }
}
