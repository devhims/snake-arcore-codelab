using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(VideoPlayer))]
public class HandScanUI : MonoBehaviour
{
    public Text handUIText;
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public Texture rawImageTexture;

    void Start()
    {
        handUIText.gameObject.SetActive(false);
        videoPlayer.prepareCompleted += PrepareCompleted;
        rawImage.texture = rawImageTexture;
    }

    private void PrepareCompleted(VideoPlayer player)
    {
        rawImage.texture = player.texture;
        videoPlayer.enabled = true;
        videoPlayer.Play();
        handUIText.gameObject.SetActive(true);
    }

}