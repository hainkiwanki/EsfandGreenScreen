using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class ChatNode : BaseNode 
{
    public float delayBefore = 0.2f;
    [VerticalGroup("Text to say"), HideLabel, TextArea]
    [HideIf("hideChatBox")]
    public string text;
    [VerticalGroup("Text to say"), HideLabel]
    public AudioClip clip;
    public float delayAfter;
    [HideIf("@this.clip != null")]
    public float time;
    public bool hideChatBox = false;

    private bool m_executed = false;
    private ChatBox m_box;

    protected override void Init()
    {
        base.Init();
        type = ENodeType.Chat;
    }

    public override void Enter()
    {
        m_box = GameSettings.Inst.chatBox;
        m_box.TurnToText();
    }

    public override void Execute()
    {
        if (m_executed) return;

        m_executed = true;
        m_box.message = text;
        if(!hideChatBox)
            m_box.Show();
        GameSettings.Inst.controls.Disable();
        GameSettings.Inst.StartCoroutine(Chat());
    }

    IEnumerator Chat()
    {
        if (delayBefore > 0)
            yield return new WaitForSeconds(delayBefore);

        if (clip != null)
        {
            SoundManager.Inst.sfxSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
        else
        {
            yield return new WaitForSeconds(time);
        }

        if (delayAfter > 0)
            yield return new WaitForSeconds(delayAfter);

        isdone = true;
    }

    public override void Exit()
    {
        m_executed = false;
        isdone = false;
        m_box.Hide();
        GameSettings.Inst.controls.Enable();
    }
}