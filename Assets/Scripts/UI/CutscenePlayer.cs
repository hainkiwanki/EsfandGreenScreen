using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class CutscenePlayer : MonoBehaviour
{
    private VideoPlayer m_videoPlayer;

    private void Awake()
    {
        m_videoPlayer = GetComponent<VideoPlayer>();
        m_videoPlayer.playOnAwake = true;
    }

    public bool IsDonePlaying()
    {
        return ((int)m_videoPlayer.frame >= (int)m_videoPlayer.frameCount - 2);
    }

    public void PlayClip(VideoClip _clip)
    {
        m_videoPlayer.clip = _clip;
        m_videoPlayer.Play();
    }

    public void Stop()
    {
        m_videoPlayer.Stop();
    }
}
