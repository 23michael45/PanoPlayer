using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VirtualKeyShift : VirtualKey {

    Color mPressColor;
    Color mNormalColor;

    protected virtual void Start()
    {
        base.Start();
        mNormalColor = gameObject.GetComponent<Button>().colors.normalColor;
        mPressColor = gameObject.GetComponent<Button>().colors.pressedColor;
    }

    protected virtual void Update()
    {
        base.Update();
        var block = gameObject.GetComponent<Button>().colors;
        if (_Keybord.IsShiftPress())
        {
            block.normalColor = mPressColor; 
        }
        else
        {
            block.normalColor = mNormalColor;

        }
        gameObject.GetComponent<Button>().colors = block;
    }

}
