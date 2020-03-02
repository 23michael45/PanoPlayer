using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DataTableItem : MonoBehaviour
{
    public bool mIsRandomValue;

    public string mName;

    public float mBaseValue;
    public float mOffsetValue;
    public string mUnitName;


    public float mOrigin = 0f;
    public float mSize = 1f;
    public float mDivisionUnit = 10;


    [HideInInspector]
    public float[] mValues;

    int mHCount = 25;

    public Text mNameText;
    public Text mValueText;
    private void Awake()
    {
        mNameText.text = mName;

        mValues = new float[mHCount];
        for (int i = 0; i < mHCount; i++)
        {
            mValues[i] = mBaseValue + mOffsetValue * UnityEngine.Random.Range(-1f, 1f);
        }

        gameObject.SetActive(false);

    }
    void Start()
    {
        //StartCoroutine(RandomValue());
    }

    IEnumerator RandomValue()
    {
        int index = UnityEngine.Random.Range(0, mValues.Length);
        mValueText.text = mValues[index].ToString();
        yield return new WaitForSeconds(1.0f);
    }
    public string GetFullName()
    {
        return mName + "  " + mUnitName;
    }
}
