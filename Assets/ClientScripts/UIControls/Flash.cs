using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    Coroutine mCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        mCoroutine = StartCoroutine(Flashing());
    }
    void OnDisable()
    {
        if(mCoroutine != null)
        {
            StopCoroutine(mCoroutine);
            mCoroutine = null;

        }
    }
    IEnumerator Flashing()
    {
        yield return new WaitForEndOfFrame();
        while (mCoroutine != null)
        {
            gameObject.GetComponent<Image>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            gameObject.GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
