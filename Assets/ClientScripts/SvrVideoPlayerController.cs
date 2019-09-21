// Copyright 2018 Skyworth VR. All rights reserved.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SvrVideoPlayerController : MonoBehaviour
{
    public PanoPlayerLogic PlayerLogic;
    public SvrVideoPlayer SvrVideoPlayer;
        
    private void Start()
    {

        if (SvrVideoPlayer == null)
            SvrVideoPlayer = GetComponent<SvrVideoPlayer>();

        SvrVideoPlayer.OnEnd += OnEnd;
        SvrVideoPlayer.OnReady += OnReady;
        SvrVideoPlayer.OnVolumeChange += OnVolumeChange;
        SvrVideoPlayer.OnProgressChange += OnProgressChange;
        SvrVideoPlayer.OnVideoError += OnVideoError;
        SvrVideoPlayer.OnVideoPlayerStatusChange += OnVideoPlayerStatusChange;

    }

    public void PlayVideoByUrl(string url)
    {
        if(SvrVideoPlayer.GetPlayerState() == VideoPlayerState.Play)
        {
            SvrVideoPlayer.Stop();
            SvrVideoPlayer.Release();

        }
        // Use CreatVideoPlayer before PreparedPlayVideo.
        SvrVideoPlayer.CreatVideoPlayer();
        // Set video data source.
        SvrVideoPlayer.PreparedPlayVideo(url);
        
        SvrVideoPlayer.SetPlayMode2D();
        
    }

    public void Play()
    {
        SvrVideoPlayer.Play();
    }

    public void Stop()
    {
        SvrVideoPlayer.Stop();
        SvrVideoPlayer.Release();
    }
    public void Pause()
    {
        SvrVideoPlayer.Pause();
    }
    public bool IsPlaying()
    {
        return SvrVideoPlayer.GetPlayerState() == VideoPlayerState.Play;
    }

    private void OnEnd()
    {
        SvrVideoPlayer.Release();
        PlayerLogic.SetPlayState(false);
    }

    private void OnReady()
    {
        PlayerLogic.SetPlayState(true);
        long totalTime = SvrVideoPlayer.GetVideoDuration();


    }

    private void OnVolumeChange(float volumePercent)
    {
    }

    private void OnProgressChange(int time)
    {
    }

    private void OnVideoError(ExceptionEvent errorCode, string errMessage)
    {
        PlayerLogic.SetPlayState(false);
    }

    private void OnVideoPlayerStatusChange(bool status)
    {
    }

    private void OnApplicationQuit()
    {
        SvrVideoPlayer.OnEnd -= OnEnd;
        SvrVideoPlayer.OnReady -= OnReady;
        SvrVideoPlayer.OnVolumeChange -= OnVolumeChange;
        SvrVideoPlayer.OnProgressChange -= OnProgressChange;
        SvrVideoPlayer.OnVideoError -= OnVideoError;
        SvrVideoPlayer.OnVideoPlayerStatusChange -= OnVideoPlayerStatusChange;
    }
}
