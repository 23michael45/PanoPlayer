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
        m_Text.text = file.Substring(file.LastIndexOf('/') + 1);
    }
    void OnClick()
    {
#if AVPRO
        PanoPlayerLogic.mInstance.m_MediaPlayer.OpenVideoFromFile(RenderHeads.Media.AVProVideo.MediaPlayer.FileLocation.AbsolutePathOrURL, m_FileName, true);
#elif UMP
        PanoPlayerLogic.mInstance.m_MediaPlayer.Path = m_FileName;
#endif
    }
}
