using UnityEngine;
using System.Collections.Generic;

public class VirtualKeyboard : MonoBehaviour {

    public VirtualTextInputBox[] TextInputBoxs = null;
    private VirtualTextInputBox TextInputBox = null;
    protected enum kLanguage { kKorean, kEnglish};
    protected bool mPressShift = false;
    protected kLanguage mLanguage = kLanguage.kEnglish;
    protected Dictionary<char, char> CHARACTER_TABLE = new Dictionary<char, char>
    {
        {'1', '!'}, {'2', '@'}, {'3', '#'}, {'4', '$'}, {'5', '%'},{'6', '^'}, {'7', '&'}, {'8', '*'}, {'9', '('},{'0', ')'},
        { '`', '~'},   {'-', '_'}, {'=', '+'}, {'[', '{'}, {']', '}'}, {'\\', '|'}, {',', '<'}, {'.', '>'}, {'/', '?'}
    };

    public bool IsShiftPress(){ return mPressShift; }

    void Awake()
    {
        VirtualKey._Keybord = this;
    }
	// Use this for initialization
	void Start () {
        if (TextInputBoxs != null)
        {
            TextInputBox = TextInputBoxs[0];
        }
    }

    public void changeFocusInputBox(int index)
    {
        Debug.Log("changeFocusInputBox" + index);
        if (index < TextInputBoxs.Length)
        {
            TextInputBox = TextInputBoxs[index];
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Clear()
    {
        if(TextInputBox != null)
        {
            TextInputBox.Clear();
        }
    }

    void OnGUI()
    {
        //Event e = Event.current;
        //if (e.isKey)
        //  Debug.Log("Detected key code: " + e.keyCode);

    }

    public void KeyDown(VirtualKey _key)
    {
        if(TextInputBox != null)
        {
            switch(_key.KeyType)
            {
                case VirtualKey.kType.kShift:
                    {
                        mPressShift = true;
                    }
                    break;
                case VirtualKey.kType.kSpace:
                case VirtualKey.kType.kBackspace:
                    {
                        TextInputBox.KeyDown(_key);
                    }
                    break;
                case VirtualKey.kType.kReturn:
                    {
                        //do somehing
                    }
                    break;
                case VirtualKey.kType.kCharacter:
                    {
                        char keyCharacter = _key.KeyValue;
                        if (mPressShift)
                        {
                            keyCharacter = char.ToUpper(keyCharacter);
                            mPressShift = false;
                        }
                        if (mLanguage == kLanguage.kEnglish)
                        {
                            TextInputBox.KeyDown(keyCharacter);
                        }
                    }
                    break;
                case VirtualKey.kType.kOther:
                    {
                        char keyCharacter = _key.KeyValue;
                        if (mPressShift)
                        {
                            keyCharacter = _key.KeyShiftValue;
                            mPressShift = false;
                        }
                        TextInputBox.KeyDown(keyCharacter);
                    }
                    break;

            }

        }
    }
}
