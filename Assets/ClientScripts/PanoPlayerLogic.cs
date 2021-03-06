﻿#if AVPRO
using RenderHeads.Media.AVProVideo;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanoPlayerLogic : MonoBehaviour
{
    public static PanoPlayerLogic mInstance;  
    public SvrVideoPlayerController m_MediaPlayer;

    public Canvas m_UICanvas;
    public Camera m_MainCamera;

    public GameObject m_ControlBar;
    public GameObject m_SensorData;


    public Transform m_Content;
    public GameObject m_ItemPrefab;

    public GameObject mFilePlayButtonContainer;
    public Button m_NextBtn;
    public Button m_PreBtn;
    public Button m_StopBtn;
    public Button m_PlayBtn;
    public Button m_PauseBtn;
    public Button m_StartStreamBtn;
    public Button m_StopStreamBtn;
    public Button m_QuitBtn;
    public VirtualTextInputBox m_StreamUrl;
    public Text m_MsgLabel;

    public GameObject m_Pointer;

    string[] m_FileNames;
    int m_CurrentIndex = 0;

    bool m_IsStreaming = false;

    void PlayIndex(int index)
    {
        m_IsStreaming = false;
        string file = m_FileNames[index];

        m_MediaPlayer.PlayVideoByUrl(file);
        SetMsg("Loading");
    }
    public void Start()
    {
        m_NextBtn.onClick.AddListener(OnNext);
        m_PreBtn.onClick.AddListener(OnPre);
        m_StopBtn.onClick.AddListener(OnStop);
        m_PlayBtn.onClick.AddListener(OnPlay);
        m_PauseBtn.onClick.AddListener(OnPause);
        m_StartStreamBtn.onClick.AddListener(OnStartStream);
        m_StopStreamBtn.onClick.AddListener(OnStopStream);
        m_QuitBtn.onClick.AddListener(OnQuit);
        mInstance = this;
        m_SensorData.SetActive(false);
        m_StopStreamBtn.gameObject.SetActive(false);

        string streamUrl = PlayerPrefs.GetString("StreamUrl","");
        if(streamUrl == "")
        {
            m_StreamUrl.gameObject.GetComponent<InputField>().text = "rtmp://39.108.230.83/live/001";
            //m_StreamUrl.gameObject.GetComponent<InputField>().text = "http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4";
        }
        else
        {
            m_StreamUrl.gameObject.GetComponent<InputField>().text = streamUrl;
        }
#if UNITY_ANDROID && !UNITY_EDITOR
        m_FileNames = AndroidNative.GetFilesInPath(AndroidNative.GetDCIMPath());
 
        
#else
        int len = 5;
        m_FileNames = new string[len];
        for (int i = 0; i < len; i++)
        {
            m_FileNames[i] = "D:/DevelopProj/Pano/PiProject/PanoPlayer/PanoPlayer/Assets/2D/Pano8K.mp4";
        }
#endif
        FillItems(m_FileNames);
        SetPlayState(false);


        //test on phone
        //OnStartStream();
    }
    public void FillItems(string[] fileNames)
    {
        foreach (string fileName in fileNames)
        {
            GameObject gonew = GameObject.Instantiate(m_ItemPrefab);
            gonew.SetActive(true);
            gonew.transform.parent = m_Content;
            gonew.transform.localScale = Vector3.one;
            gonew.transform.localPosition = Vector3.zero;
            gonew.GetComponent<ItemLogic>().SetInfo(fileName);
        }
    }
    
    public void Update()
    {
        bool trigger = false;
        if(GvrControllerInput.ClickButtonDown)
        {
            if(!IsPointerOverUIElement())
            {
                bool b = m_ControlBar.activeInHierarchy;
                m_ControlBar.SetActive(!b);

                m_SensorData.SetActive(b);
                if (b == false)
                {
                    float dist = Vector3.Distance(m_MainCamera.transform.position, m_ControlBar.transform.position);
                    Vector3 dir = m_MainCamera.transform.forward;
                    dir.y = 0;
                    dir = dir.normalized;

                    m_ControlBar.transform.position = m_MainCamera.transform.position + dir * dist;
                    m_ControlBar.transform.LookAt(m_ControlBar.transform.position + m_ControlBar.transform.position - m_MainCamera.transform.position);

                }

                //m_Pointer.SetActive(!b);
            }
            
        }

#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(1))
        {
            if (!IsPointerOverUIElement())
            {
                bool b = m_ControlBar.activeInHierarchy;
                m_ControlBar.SetActive(!b);
                if(b == false)
                {
                    float dist = Vector3.Distance(m_MainCamera.transform.position, m_ControlBar.transform.position);
                    Vector3 dir = m_MainCamera.transform.forward;
                    dir.y = 0;
                    dir = dir.normalized;

                    m_ControlBar.transform.position = m_MainCamera.transform.position + dir * dist;
                    m_ControlBar.transform.LookAt(m_ControlBar.transform.position +  m_ControlBar.transform.position - m_MainCamera.transform.position);

                }


                m_SensorData.SetActive(b);

                //m_Pointer.SetActive(!b);
            }
        }
#endif


            if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    void OnNext()
    {
        m_CurrentIndex++;
        if (m_CurrentIndex >= m_FileNames.Length)
        {
            m_CurrentIndex = 0;

        }
        PlayIndex(m_CurrentIndex);
    }
    void OnPre()
    {
        m_CurrentIndex--;
        if (m_CurrentIndex < 0)
        {
            m_CurrentIndex = m_FileNames.Length - 1;

        }
        PlayIndex(m_CurrentIndex);

    }
    void OnPlay()
    {

        PlayIndex(m_CurrentIndex);
        SetPlayState(true);
    }
    void OnPause()
    {
        if (m_MediaPlayer.IsPlaying())
        {

            m_MediaPlayer.Pause();
        }
        else
        {

            m_MediaPlayer.Play();
        }
    }
    void OnStop()
    {
        Debug.Log("OnLogic Stop");
        m_MediaPlayer.Stop();
        SetPlayState(false);
    }
    void OnStartStream()
    {
        m_StreamUrl.HideKeyboard();
        string url = m_StreamUrl.gameObject.GetComponent<InputField>().text;
        SetMsg("Loading");
        m_MediaPlayer.PlayVideoByUrl(url);

        m_IsStreaming = true;

        PlayerPrefs.SetString("StreamUrl", url);
        PlayerPrefs.Save();

        m_StartStreamBtn.gameObject.SetActive(false);
        m_StopStreamBtn.gameObject.SetActive(true);
        mFilePlayButtonContainer.SetActive(false);
        m_StreamUrl.enabled = false;
    }
    void OnStopStream()
    {
        m_MediaPlayer.Stop();

        m_StartStreamBtn.gameObject.SetActive(true);
        m_StopStreamBtn.gameObject.SetActive(false);
        mFilePlayButtonContainer.SetActive(true);
        m_StreamUrl.enabled = true;
    }
    private void OnQuit()
    {
        Application.Quit();
    }

    public void SetPlayState(bool bPlaying)
    {
        Debug.Log("SetPlayState Is Playing : " + bPlaying);
        //m_MediaPlayer.gameObject.SetActive(bPlaying);
        m_MediaPlayer.gameObject.GetComponent<Renderer>().enabled = bPlaying;

        if(bPlaying)
        {
            SetCurrentFileNameMsg();
        }
        else
        {
            SetMsg("");
        }
    }

    void SetCurrentFileNameMsg()
    {
        string file;
        if (m_IsStreaming)
        {
            file = m_StreamUrl.gameObject.GetComponent<InputField>().text;
        }
        else
        {

            file = m_FileNames[m_CurrentIndex];
        }
        string msg = file.Substring(file.LastIndexOf('/') + 1);
        SetMsg(msg);
    }

    void SetMsg(string msg)
    {
        m_MsgLabel.text = msg;
    }











    ///////////////////////////////////////////////////////////////////
    /// ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }
    ///Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}

