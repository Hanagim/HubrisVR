using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoController : MonoBehaviour
{
    private VideoPlayer player;
    public VideoSlider slider;
    public GameObject playButton;

    private bool wasPlayingBeforeScroll = false; // New variable to track play state

    private void Awake()
    {
        player = GetComponent<VideoPlayer>();
        slider.OnScrollToggled += SliderToggled;
        slider.onValueChanged.AddListener(ScrollTo);
    }

    private void SliderToggled(bool active)
    {
        if (active)
        {
            // Store the play state before pausing
            wasPlayingBeforeScroll = player.isPlaying;
            PauseVideo();
        }
        else
        {
            // Resume playback only if the video was playing before scrolling
            if (wasPlayingBeforeScroll)
            {
                PlayVideo();
            }
        }
    }

    private void Update()
    {
        if (player.isPlaying && !slider.IsBeingUsed)
        {
            float percentage = (float)player.time / (float)player.length;
            slider.SetValueWithoutNotify(percentage);
        }
    }

    public void PlayVideo()
    {
        player.Play();
    }

    public void StopVideo()
    {
        player.Stop();
    }

    public void PauseVideo()
    {
        player.Pause();
    }

    public void ScrollTo(float percentage)
    {
        if (percentage > 0 && percentage < 1)
        {
            float seconds = percentage * (float)player.length;
            player.time = seconds;
        }
    }
}
