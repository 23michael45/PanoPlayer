// Copyright 2018 Skyworth VR. All rights reserved.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SvrVideoPlayerController : MonoBehaviour
{
    public PanoPlayerLogic PlayerLogic;
    public SvrVideoPlayer SvrVideoPlayer;

    string mCurrentUrl;

    static bool bFirstTimeFocus = true;
    static bool bFirstTimePause = true;
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if(bFirstTimeFocus)
            {
                bFirstTimeFocus = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(mCurrentUrl))
                {
                    PlayVideoByUrl(mCurrentUrl);

                }


            }
        }
        else
        {

            SvrVideoPlayer.Stop();
            SvrVideoPlayer.Release();
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SvrVideoPlayer.Stop();
            SvrVideoPlayer.Release();
        }
        else
        {
            if (bFirstTimePause)
            {
                bFirstTimePause = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(mCurrentUrl))
                {
                    PlayVideoByUrl(mCurrentUrl);
                }
            }
        }
    }
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
        mCurrentUrl = url;
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
        Message.Instance.ShowMessage("流播放错误!请检查网络或流地址是否正确!");
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
