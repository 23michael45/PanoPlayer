using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public static Message Instance;

    public GameObject mMessageGo;
    public Text mText;
    private void Awake()
    {
        Instance = this;
        mMessageGo.SetActive(false);
    }
    public void ShowMessage(string s)
    {
        mText.text = s;
        StartCoroutine(AutoHide());
    }

    IEnumerator AutoHide()
    {
        mMessageGo.SetActive(true);
        yield return new WaitForSeconds(3);
        mMessageGo.SetActive(false);

    }
}
