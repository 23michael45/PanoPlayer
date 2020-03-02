using ChartAndGraph;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartTest : MonoBehaviour
{
    public GraphChartBase mGraph;
    // Start is called before the first frame update
    void Start()
    {
        float[] values = new float[24];

        mGraph.DataSource.StartBatch();
        for (int i = 0; i< values.Length;i++)
        {
            mGraph.DataSource.AddPointToCategory("Line1", i,i * i);
        }
        mGraph.DataSource.EndBatch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
