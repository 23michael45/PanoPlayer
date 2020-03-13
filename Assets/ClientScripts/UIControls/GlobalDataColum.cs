using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalDataColum : MonoBehaviour
{
    [HideInInspector]
    public DataTableItem mItem;
    public Text mTitle;
    public Text mValue;
    public Button mBtn;

    private void Start()
    {
        mBtn.onClick.AddListener(OnClick);
    }
    private void OnDestroy()
    {
        mBtn.onClick.RemoveAllListeners();

    }
    public void SetDataItem(DataTableItem item)
    {
        mItem = item;
        mTitle.text = item.mName;
        mValue.text = string.Format("{0:0.00}",item.mValues[0]);
    }
    void OnClick()
    {
        GlobalDataTable.Instance.gameObject.SetActive(false);
        GlobalDataTable.Instance.mChart.SetActive(true);
        GlobalDataTable.Instance.mChart.GetComponent<GraphChartHelper>().FillItem(mItem);
    }
}
