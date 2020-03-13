using ChartAndGraph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphChartHelper : MonoBehaviour
{
    GraphChartBase mGraph;
    DataTableItem[] mDataItems;
    Coroutine mCoroutine;
    public float mUpdateTime = 5;

    public GameObject WarningGO;
    public DataTableItem WarningItem;

    DataTableItem mCurrentItem;
    public Button mCloseBtn;

    // Start is called before the first frame update
    void Start()
    {
        mCloseBtn.onClick.AddListener(OnCloseBtn);
    }
    private void OnDestroy()
    {
        mCloseBtn.onClick.RemoveListener(OnCloseBtn);

    }

    private void OnEnable()
    {
        mGraph = GetComponentInChildren<GraphChartBase>();
        mDataItems = GetComponentsInChildren<DataTableItem>(true);
        WarningGO.SetActive(false);
        //mCoroutine = StartCoroutine(LoopFillItems());

    }
    private void OnDisable()
    {
        //StopCoroutine(mCoroutine);
        //mCoroutine = null;
    }

    public void FillItem(DataTableItem item)
    {
        if(mCurrentItem == null)
        {
            FillOneItem(null, item);

        }
        else
        {
            FillOneItem(mCurrentItem, item);
        }
    }

    void FillOneItem(DataTableItem preItem,DataTableItem curItem)
    {
        if (mGraph != null)
        {
            mGraph.DataSource.StartBatch();
            if(mGraph.DataSource.HasCategory("default"))
            {
                mGraph.DataSource.RenameCategory("default", curItem.GetFullName());
            }
            else if (preItem != null && mGraph.DataSource.HasCategory(preItem.GetFullName()))
            {

                mGraph.DataSource.RenameCategory(preItem.GetFullName(), curItem.GetFullName());

            }

            mGraph.DataSource.AutomaticVerticallView = false;
            mGraph.DataSource.AutomaticHorizontalView = false;

            mGraph.DataSource.VerticalViewOrigin = 0;
            mGraph.DataSource.VerticalViewSize = 25;

            mGraph.DataSource.VerticalViewOrigin = curItem.mOrigin;
            mGraph.DataSource.VerticalViewSize = curItem.mSize;

            HorizontalAxis hAxis = mGraph.gameObject.GetComponent<HorizontalAxis>();
            
            hAxis.MainDivisions.Messure = ChartDivisionInfo.DivisionMessure.DataUnits;
            hAxis.MainDivisions.UnitsPerDivision = 1;


            VerticalAxis vAxis = mGraph.gameObject.GetComponent<VerticalAxis>();
            
            vAxis.MainDivisions.Messure = ChartDivisionInfo.DivisionMessure.DataUnits;
            vAxis.MainDivisions.UnitsPerDivision = curItem.mDivisionUnit;

            if(preItem != null)
            {
                mGraph.DataSource.ClearCategory(preItem.GetFullName());

            }
            mGraph.DataSource.ClearCategory(curItem.GetFullName());
            for (int i = 0; i < curItem.mValues.Length; i++)
            {
                mGraph.DataSource.AddPointToCategory(curItem.GetFullName(), i, curItem.mValues[i]);
            }
            
            mGraph.DataSource.EndBatch();

            if(WarningItem == curItem)
            {
                WarningGO.SetActive(true);
            }
            else
            {
                WarningGO.SetActive(false);

            }
        }
        mCurrentItem = curItem;
    }

    IEnumerator LoopFillItems()
    {
        if(mDataItems == null)
        {
            yield break;
        }
        while(true)
        {
            for(int i = 0; i<mDataItems.Length;i++)
            {
                int cur = i;
                int pre = i - 1;
                if(cur == 0)
                {
                    pre = mDataItems.Length - 1;
                }

                FillOneItem(mDataItems[pre], mDataItems[cur]);

                yield return new WaitForSeconds(mUpdateTime);
            }
        }

    }


    void OnCloseBtn()
    {
        GlobalDataTable.Instance.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
