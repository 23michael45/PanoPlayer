#if AVPRO
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
    public GameObject m_BackgroundScene;

    public Canvas m_UICanvas;


    public Transform m_Content;
    public GameObject m_ItemPrefab;

    public Button m_NextBtn;
    public Button m_PreBtn;
    public Button m_StopBtn;
    public Button m_PlayBtn;
    public Button m_PauseBtn;
    public Button m_StartStreamBtn;
    public Button m_QuitBtn;
    public VirtualTextInputBox m_StreamUrl;
    public Text m_FileNameLabel;


    string[] m_FileNames;
    int m_CurrentIndex = 0;

    void PlayIndex(int index)
    {
        string file = m_FileNames[index];
        m_FileNameLabel.text = file.Substring(file.LastIndexOf('/') + 1);

        m_MediaPlayer.PlayVideoByUrl(file);

    }

    public void Start()
    {
        m_NextBtn.onClick.AddListener(OnNext);
        m_PreBtn.onClick.AddListener(OnPre);
        m_StopBtn.onClick.AddListener(OnStop);
        m_PlayBtn.onClick.AddListener(OnPlay);
        m_PauseBtn.onClick.AddListener(OnPause);
        m_StartStreamBtn.onClick.AddListener(OnStartStream);
        m_QuitBtn.onClick.AddListener(OnQuit);
        mInstance = this;
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
        PlayIndex(m_CurrentIndex);
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
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click Big Button");

            if(!IsPointerOverUIElement())
            {
                bool b = m_UICanvas.gameObject.activeInHierarchy;
                m_UICanvas.gameObject.SetActive(!b);

            }
        }
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
#if UMP
        m_MediaPlayer.Play();
#endif
    }
    void OnPause()
    {
#if UMP
        if (m_MediaPlayer.IsPlaying)
        {

            m_MediaPlayer.Pause();
        }
        else
        {

            m_MediaPlayer.Play();
        }
#endif
    }
    void OnStop()
    {
#if UMP
        m_MediaPlayer.Stop();
#endif
    }
    void OnStartStream()
    {
        m_StreamUrl.HideKeyboard();
        string url = m_StreamUrl.gameObject.GetComponent<InputField>().text;
    
        m_FileNameLabel.text = url;
        m_MediaPlayer.PlayVideoByUrl(url);
    }
    private void OnQuit()
    {
        Application.Quit();
    }

    public void SetPlayState(bool bPlaying)
    {
        m_BackgroundScene.SetActive(!bPlaying);
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

