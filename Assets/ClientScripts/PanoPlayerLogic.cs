using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanoPlayerLogic : MonoBehaviour
{
    public static PanoPlayerLogic mInstance;

    public MediaPlayer m_MediaPlayer;
    public Canvas m_UICanvas;


    public Transform m_Content;
    public GameObject m_ItemPrefab;

    public Button m_NextBtn;
    public Button m_PreBtn;

    string[] m_FileNames;
    int m_CurrentIndex = 0;

    void PlayIndex(int index)
    {
        m_MediaPlayer.Stop();
        m_MediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, m_FileNames[index], true);
        m_MediaPlayer.Play();
    }

    public void Start()
    {

        m_MediaPlayer.m_VideoLocation = MediaPlayer.FileLocation.AbsolutePathOrURL;
        m_NextBtn.onClick.AddListener(OnNext);
        m_PreBtn.onClick.AddListener(OnPre);
        mInstance = this;
#if UNITY_ANDROID && !UNITY_EDITOR
        m_FileNames = AndroidNative.GetFilesInPath(AndroidNative.GetDCIMPath());

        m_MediaPlayer.m_VideoLocation = MediaPlayer.FileLocation.AbsolutePathOrURL;
        
        FillItems(m_FileNames);
#else

        m_FileNames = new string[5];
        for (int i = 0; i < 5; i++)
        {
            m_FileNames[i] = "D:/DevelopProj/Pano/PiProject/PanoPlayer/PanoPlayer/Assets/2D/Pano8K.mp4";
        }
        //fileNames[0] = "http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4";
        FillItems(m_FileNames);

        m_MediaPlayer.m_VideoPath = m_FileNames[0];

#endif

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

    public void OnSelectBtn()
    {

    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click Big Button");
            bool b = m_UICanvas.gameObject.activeInHierarchy;
            //m_UICanvas.gameObject.SetActive(!b);
        }
    }
    void OnNext()
    {
        m_CurrentIndex++;
        if(m_CurrentIndex >= m_FileNames.Length)
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
}
