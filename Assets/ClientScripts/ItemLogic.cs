using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLogic : MonoBehaviour
{
    public Text m_Text;
    public Button m_Btn;

    string m_FileName;
    private void Start()
    {
        m_Btn.onClick.AddListener(OnClick);
    }
    private void OnDestroy()
    {
        m_Btn.onClick.RemoveAllListeners();
    }
    public void SetInfo(string file)
    {
        m_FileName = file;
        m_Text.text = file;
    }
    void OnClick()
    {
        PanoPlayerLogic.mInstance.m_MediaPlayer.OpenVideoFromFile(RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation.AbsolutePathOrURL, m_FileName, true);
    }
}
