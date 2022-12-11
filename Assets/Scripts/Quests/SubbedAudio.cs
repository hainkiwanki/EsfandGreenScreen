using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "text aduio", menuName = "Dialogue/SubbedAudio")]
public class SubbedAudio : SerializedScriptableObject
{
    public bool useTextPerClip = false;
    [HideIf("useTextPerClip")]
    public bool useSingle = true;
    [HideIf("useTextPerClip")]
    public string text;
    [HideIf("useTextPerClip")]
    [ShowIf("useSingle")]
    public AudioClip audioClip;
    [HideIf("useSingle")]
    [HideIf("useTextPerClip")]
    public List<AudioClip> audioClips = new List<AudioClip>();
    [ShowIf("useTextPerClip")]
    public List<TextPerClip> audioClipsSubbed = new List<TextPerClip>();

    public TextPerClip GetTPC()
    {
        if(useSingle && !useTextPerClip)
        {
            return new TextPerClip() { clip = audioClip, text = this.text };
        }
        else if(!useSingle && !useTextPerClip)
        {
            return new TextPerClip() { text = this.text, clip = audioClips.GetRandom() };
        }
        else
        {
            return audioClipsSubbed.GetRandom();
        }
    }
}
