using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class KeyboardName2Text : MonoBehaviour
{
    [ExecuteInEditMode]
    void Start()
    {
        Button[] btnArray = gameObject.GetComponentsInChildren<Button>();
        foreach (Button btn in btnArray)
        {
            Text txt = btn.gameObject.GetComponentInChildren<Text>();
            //txt.text = btn.gameObject.name;

            VirtualKey k = btn.gameObject.GetComponentInChildren<VirtualKey>();
            string[] values = txt.text.Split('\n');
            if (values.Length == 2)
            {
                k.KeyValue = values[1][0];
                k.KeyShiftValue = values[0][0];
            }
            else if (values.Length == 1)
            {
                string s = values[0].ToLower();
                if(s.Length == 1)
                {
                    k.KeyValue = (values[0].ToLower())[0];
                    k.KeyShiftValue = values[0][0];

                }

            }
        }
        
    }
}
