using UnityEngine;
using System.Collections;

public class VirtualKey : MonoBehaviour {

    static public VirtualKeyboard _Keybord = null;
    public enum kType { kCharacter, kOther, kReturn, kSpace, kBackspace, kShift, kTab, kCapsLock}
    public char KeyShiftValue;
    public char KeyValue;
    public kType KeyType = kType.kCharacter;
    
    private bool mKeepPresed;
    public bool KeepPressed
    {
        set { mKeepPresed = value; }
        get { return mKeepPresed; }
    }

	// Use this for initialization
	protected virtual void Start () {
        UnityEngine.UI.Button _button = gameObject.GetComponent<UnityEngine.UI.Button>();
        if(_button != null)
        {
            _button.onClick.AddListener(onKeyClick);
        }
    }

    void onKeyClick()
    {
        //VirtualKeyboard _keybord = GameObject.FindObjectOfType< VirtualKeyboard>();
        if(_Keybord != null)
        {
            _Keybord.KeyDown(this);
        }
    }

    // Update is called once per frame
    protected virtual void Update () {

	    if(KeepPressed)
        {
            //do something
        }
	}



    
}
