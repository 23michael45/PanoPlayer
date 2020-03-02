using ChartAndGraph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphChartHelper : MonoBehaviour
{
    GraphChartBase mGraph;
    DataTableItem[] mDataItems;

    public float mUpdateTime = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        mGraph = GetComponentInChildren<GraphChartBase>();
        mDataItems = GetComponentsInChildren<DataTableItem>(true);
        StartCoroutine(LoopFillItems());

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
            else if (mGraph.DataSource.HasCategory(preItem.GetFullName()))
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


            mGraph.DataSource.ClearCategory(preItem.GetFullName());
            mGraph.DataSource.ClearCategory(curItem.GetFullName());
            for (int i = 0; i < curItem.mValues.Length; i++)
            {
                mGraph.DataSource.AddPointToCategory(curItem.GetFullName(), i, curItem.mValues[i]);
            }
            
            mGraph.DataSource.EndBatch();
        }
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
}
