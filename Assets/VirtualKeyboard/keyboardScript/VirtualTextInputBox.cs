using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VirtualTextInputBox : MonoBehaviour
{
    public VirtualKeyboard mKeyboard;
    Button mTriggerBtn;
	AutomateKR		mAutomateKR = new AutomateKR();
    protected UnityEngine.UI.InputField mTextField = null;
    protected string TextField
    {
        set
        {
            if (mTextField != null)
            {
                mTextField.text = value;
            }
        }
        get
        {
            if (mTextField != null)
            {
                return mTextField.text;
            }
            return "";
        }
    }

    void Start () {

        mKeyboard.gameObject.SetActive(false);
        mTextField = GetComponent<UnityEngine.UI.InputField>();
        mTextField.onEndEdit.AddListener(OnEndEdit);

        mTriggerBtn = gameObject.GetComponentInChildren<Button>();
        mTriggerBtn.onClick.AddListener(OnTrigger);

    }
    void OnTrigger()
    {
        mKeyboard.gameObject.SetActive(true);

    }
    void OnEndEdit(string s)
    {

        mKeyboard.gameObject.SetActive(false);
    }
    public void HideKeyboard()
    {
        mKeyboard.gameObject.SetActive(false);
    }
	
	void Update () {

	}

    public void Clear()
    {
        mAutomateKR.Clear();

        TextField = mAutomateKR.completeText + mAutomateKR.ingWord;
    }


    public void KeyDownHangul(char _key)
    {
        mAutomateKR.SetKeyCode(_key);

        TextField = mAutomateKR.completeText + mAutomateKR.ingWord;
    }

    public void KeyDown(char _key)
    {
        mAutomateKR.SetKeyString(_key);

        TextField = mAutomateKR.completeText + mAutomateKR.ingWord;
    }

    public void KeyDown(VirtualKey _key)
    {
        switch(_key.KeyType)
        {
            case VirtualKey.kType.kBackspace:
                {
                    mAutomateKR.SetKeyCode(AutomateKR.KEY_CODE_BACKSPACE);

                }
                break;
            case VirtualKey.kType.kSpace:
                {
                    mAutomateKR.SetKeyCode(AutomateKR.KEY_CODE_SPACE);
                }
                break;
        }

        TextField = mAutomateKR.completeText + mAutomateKR.ingWord;
    }
}
