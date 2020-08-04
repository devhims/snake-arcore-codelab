using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(VideoPlayer))]
public class RawImageVideoPlayer : MonoBehaviour
{
    public Text handUIText;
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public Texture m_RawImageTexture;

    void Start()
    {
        handUIText.gameObject.SetActive(false);
        videoPlayer.prepareCompleted += _PrepareCompleted;
        rawImage.texture = m_RawImageTexture;
    }

    private void _PrepareCompleted(VideoPlayer player)
    {
        rawImage.texture = player.texture;
        videoPlayer.enabled = true;
        videoPlayer.Play();
        handUIText.gameObject.SetActive(true);
    }
}