using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoChangeValue : MonoBehaviour
{
    public float m_Min = 0f;
    public float m_Max = 1f;
    public float m_Speed = 0.01f;

    public float m_CurValue = 0;

    bool m_Add = true;

    public Slider m_Slider;
    public string m_ContentTxt = "{}";
    public Text m_Text;
    // Start is called before the first frame update
    void Start()
    {
        if(m_Slider == null)
        {
            m_Slider = gameObject.GetComponent<Slider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Add)
        {
            m_CurValue += m_Speed;

            if(m_CurValue>= m_Max)
            {
                m_Add = false;
            }
        }
        else
        {
            m_CurValue -= m_Speed;

            if (m_CurValue <= m_Min)
            {
                m_Add = true;
            }
        }
        m_Slider.value = (m_CurValue - m_Min) /(m_Max - m_Min) ;

        if(m_Text)
        {
            string s = string.Format(m_ContentTxt, m_CurValue);
            m_Text.text = s;

        }
    }
}
