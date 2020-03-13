using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataTable : MonoBehaviour
{
    public static GlobalDataTable Instance;
    public GameObject mChart;
    public List<DataTable> mTables;

    List<DataTableItem> mDataItems = new List<DataTableItem>();

    List<GlobalDataColum> mColums = new List<GlobalDataColum>();

    public GameObject mColumPrefab;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        mChart.SetActive(false);
        foreach (var table in mTables)
        {
            foreach(var items in table.GetComponentsInChildren<DataTableItem>(true))
            {
                mDataItems.Add(items);
            }
        }

        foreach(var item in mDataItems)
        {
            GameObject gonew = Instantiate(mColumPrefab);
            gonew.transform.parent = transform;
            gonew.GetComponent<GlobalDataColum>().SetDataItem(item);
            gonew.SetActive(true);
            gonew.transform.localScale = Vector3.one;
            gonew.transform.localPosition = Vector3.zero;
            gonew.transform.localRotation = Quaternion.identity;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
